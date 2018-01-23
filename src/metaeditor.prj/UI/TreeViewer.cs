using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Diagnostics.Logs;

using Recar2.Algorithms;
using Recar2.Harvest;
using Recar2.ImageMetadatas;
using Recar2.MetaEditor.Properties;

namespace Recar2.MetaEditor.UI
{
	sealed class DirectoryEventArgs : EventArgs
	{
		public DirectoryItem Item { get; }

		public DirectoryEventArgs(DirectoryItem item)
		{
			Item = item;
		}
	}

	sealed class ImageEventArgs : EventArgs
	{
		public ImageItem Item { get; }

		public ImageEventArgs(ImageItem item)
		{
			Item = item;
		}
	}

	sealed class ImageDataEventArgs : EventArgs
	{
		public string Path { get; }

		[CanBeNull]
		public ImageMetadata Metadata { get; set; }

		[CanBeNull]
		public IntermediateMetadata AlgorithmsMetadata { get; set; }

		public ImageDataEventArgs(string path)
		{
			Verify.Argument.IsNeitherNullNorEmpty(path, nameof(path));

			Path = path;
		}
	}

	sealed class TreeViewer : IDisposable
	{
		#region Events

		public event EventHandler<ImageDataEventArgs> ImagedataRequred;

		public event EventHandler<DirectoryEventArgs> DirectorySelected;

		public event EventHandler<ImageEventArgs> ImageSelected;

		#endregion

		#region Data

		private static readonly ILog Log = LogManager.GetCurrentClassLog();

		private readonly TreeView _view;

		private readonly ImageList _imageList = new ImageList()
		{
			Images =
			{
				Resources.Important,
				Resources.UnImportant,
				Resources.Folder,
			},
			ImageSize = new Size(14, 14),
		};

		#endregion

		#region Create

		public TreeViewer(TreeView view)
		{
			Verify.Argument.IsNotNull(view, nameof(view));

			_view = view;

			_view.ImageList = _imageList;
			_view.ImageIndex = 0;

			_view.BeforeExpand += ViewOnBeforeExpand;
			_view.AfterSelect += ViewOnAfterSelect;
			_view.NodeMouseClick += ViewOnNodeMouseClick;
			_view.DoubleClick += ViewOnDoubleClick;
			_view.KeyUp += ViewOnKeyUp;
		}

		[CanBeNull]
		public ImageItem SelectedImageItem => _view.SelectedNode?.Tag as ImageItem;

		public void Dispose()
		{
			_view.BeforeExpand -= ViewOnBeforeExpand;
			_view.AfterSelect -= ViewOnAfterSelect;
		}

		#endregion

		#region Methods

		public void PrevImage()
		{
			if(_view.SelectedNode == null) return;

			_view.SelectedNode = _view.SelectedNode.PrevNode;
			_view.Focus();

			if(_view.SelectedNode.Nodes.Count != 0) return;

			OnSelectedTreeNode(_view.SelectedNode);
		}

		public void NextImage()
		{
			if(_view.SelectedNode == null) return;

			_view.SelectedNode = _view.SelectedNode.NextNode;
			_view.Focus();

			if(_view.SelectedNode.Nodes.Count != 0) return;

			OnSelectedTreeNode(_view.SelectedNode);
		}

		public void ReloadTree(string imageDirectory)
		{
			_view.Cursor = Cursors.WaitCursor;
			try
			{
				if(Directory.Exists(imageDirectory))
				{
					_view.Nodes.Clear();
					var node = BuildTree(imageDirectory, loadingContent: false);
					if(node != null)
					{
						_view.Nodes.Add(node);
						_view.SelectedNode = node;
					}
				}
				else
				{
					_view.Nodes.Clear();
				}
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Updated tree fail.", exc);
			}
			finally
			{
				_view.Cursor = Cursors.Default;
			}
		}

		public void ReloadSelectedNode()
		{
			var node = _view.SelectedNode;
			switch(_view.SelectedNode.Tag)
			{
				case DirectoryItem _:
					UpdateDirectoryNode(node);
					break;
				case ImageItem _:
					ReloadImageNode(node);
					break;
			}
			if(node != null)
			{
				_view.SelectedNode = node;
			}
			else
			{
				_view.SelectedNode.Remove();
			}
		}

		#endregion

		#region Private methods

		private TreeNode CreateTreeNode(ImageItem item)
		{
			Verify.Argument.IsNotNull(item, nameof(item));

			return new TreeNode
			{
				Tag = item,
			};
		}

		/// <summary>Создает дерево файлов с выбранной фильтрацией.</summary>
		/// <param name="directoryName">Путь к выбранной директории.</param>
		/// <param name="loadingContent"></param>
		[CanBeNull]
		private TreeNode BuildTree(string directoryName, bool loadingContent = true)
		{
			if(directoryName.Equals("Select directory", StringComparison.CurrentCulture)) return default;
			if(!Directory.Exists(directoryName)) throw new DirectoryNotFoundException("Directory not found.");

			var node = CreateDirectoryNode(directoryName);
			RebuildTree(node, loadingContent);
			return node;
		}

		private void RebuildTree(TreeNode node, bool loadingContent = true)
		{
			Verify.Argument.IsNotNull(node, nameof(node));
			Verify.Argument.IsOfExactType(node.Tag, typeof(DirectoryItem), nameof(node));

			node.Nodes.Clear();
			var item = node.Tag as DirectoryItem;
			foreach(var directory in Directory.EnumerateDirectories(item.Path))
			{
				var n = BuildTree(directory, loadingContent: false);
				if(n != null)
				{
					node.Nodes.Add(n);
				}
			}
			if(loadingContent)
			{
				var statistics = item.Statistics;
				statistics.Clear();
				foreach(var path in Directory.EnumerateFiles(item.Path).Where(p => p.IsImage()))
				{
					var n = CreateImageNode(path);
					if(n != null)
					{
						node.Nodes.Add(n);
						CollectStatistics(statistics, n.Tag as ImageItem);
					}
				}
			}
			if(node.Nodes.Count == 0) node.Nodes.Add(""); // Добавляем пустой узел, что бы появился значок Expand
		}

		private TreeNode CreateDirectoryNode(string path)
		{
			Verify.Argument.IsNeitherNullNorEmpty(path, nameof(path));

			var item = new DirectoryItem(path);
			var name = Path.GetFileName(item.Path) ?? "<none>";
			return new TreeNode
			{
				Name = name,
				Text = name,
				Tag = item,
				ImageIndex = 2,
				SelectedImageIndex = 2,
			};
		}

		private void UpdateDirectoryNode(TreeNode node)
		{
			Verify.Argument.IsNotNull(node, nameof(node));

			var item = node.Tag as DirectoryItem;
			var name = Path.GetFileName(item.Path) ?? "<none>";
			node.Name = name;
			node.Text = name;
			node.Tag = item;
			node.ImageIndex = 2;
			node.SelectedImageIndex = 2;
		}

		[CanBeNull]
		private TreeNode CreateImageNode(string path)
		{
			var args = new ImageDataEventArgs(path);
			ImagedataRequred?.Invoke(this, args);

			if(args.Metadata != null)
			{
				var item = new ImageItem(path, args.Metadata, args.AlgorithmsMetadata);
				var node = CreateTreeNode(item);
				UpdateImageNode(node);
				return node;
			}
			return default;
		}

		private void ReloadImageNode(TreeNode node)
		{
			Verify.Argument.IsNotNull(node, nameof(node));
			Verify.Argument.IsOfExactType<ImageItem>(node.Tag, nameof(node));

			var item = node.Tag as ImageItem;
			var args = new ImageDataEventArgs(item.Path);
			ImagedataRequred?.Invoke(this, args);

			node.Tag = new ImageItem(item.Path, args.Metadata, args.AlgorithmsMetadata);
			UpdateImageNode(node);
		}

		private void UpdateImageNode(TreeNode node)
		{
			Verify.Argument.IsNotNull(node, nameof(node));
			Verify.Argument.IsOfExactType<ImageItem>(node.Tag, nameof(node));

			var item = node.Tag as ImageItem;
				var path = ((ImageItem)node.Tag).Path;
				var color = Color.DarkGray;
				node.Name = Path.GetFileName(path) ?? "<none>";
				var text = node.Name;
				var important = false;
				var metadata = item.Metadata;
				if(metadata != null)
				{
					var plates = string.Join(", ", metadata.Plates?.Select(p => p.Number).ToArray() ?? new string[0]);
					text += $" [{plates}]";
					color = metadata.HumanChecked ? Color.DarkGreen : Color.Red;
					if(metadata.Plates != null && metadata.Plates.Any(p => !string.IsNullOrEmpty(p.Number) && string.IsNullOrEmpty(p.Stencil)))
					{
						text += "(no_stencil)";
						color = Color.MediumVioletRed;
					}
					if(item.AlgorithmMetadata != null)
					{
						var decisions = item.AlgorithmMetadata.Intermediated.Get<Decision[]>();
						if(decisions.Any())
						{
							text += $" /'{decisions[0].Plate.Number}'";
						}
						else
						{
							text += " /[unrecog]";
						}
					}
					important = metadata.Important;
				}
				node.Text = text;
				node.ForeColor = color;
				node.ImageIndex = important ? 0 : 1;
				node.SelectedImageIndex = important ? 0 : 1;
			
		}

		private static void CollectStatistics(DirStatistics statistics, ImageItem item)
		{
			statistics.TotalImageCount++;

			if(item.Metadata != null && item.Metadata.HumanChecked) statistics.MarkupImageCount++;

			if(item.AlgorithmMetadata != null && item.AlgorithmMetadata.Intermediated.Get<Decision[]>().Any()) statistics.RecogImageCount++;

			if(item.Metadata?.Plates != null)
			{
				foreach(var plate in item.Metadata.Plates)
				{
					var stencil = plate.Stencil;
					if(!statistics.Stencils.TryGetValue(stencil, out var _))
					{
						statistics.Stencils[stencil] = new DirStatistics.StencilCount();
					}
					statistics.Stencils[stencil].TotalPlateCount++;
					if(item.Metadata.HumanChecked) statistics.Stencils[stencil].MarkupPlateCount++;
				}
			}
		}

		#endregion

		#region Event handlers

		private void OnSelectedTreeNode(TreeNode node)
		{
			Verify.Argument.IsNotNull(node, nameof(node));

			switch(node.Tag)
			{
				case DirectoryItem item:
					DirectorySelected?.Invoke(this, new DirectoryEventArgs(item));
					break;
				case ImageItem item:
					ImageSelected?.Invoke(this, new ImageEventArgs(item));
					break;
			}
		}

		private void ViewOnBeforeExpand(object sender, TreeViewCancelEventArgs args)
		{
			if(args.Node.Tag is DirectoryItem)
			{
				RebuildTree(args.Node);
			}
		}

		private void ViewOnAfterSelect(object sender, TreeViewEventArgs args)
		{
			OnSelectedTreeNode(args.Node);
		}

		private void ViewOnKeyUp(object sender, KeyEventArgs args)
		{
			if(args.KeyCode != Keys.Down && args.KeyCode != Keys.Up) return;
			if(_view.SelectedNode.Nodes.Count != 0) return;
			OnSelectedTreeNode(_view.SelectedNode);
		}

		private void ViewOnDoubleClick(object sender, EventArgs eventArgs)
		{
			try
			{
				switch(_view.SelectedNode.ImageIndex)
				{
					case 2:
						return;
					case 1:
						_view.SelectedNode.ImageIndex = 0;
						_view.SelectedNode.SelectedImageIndex = 0;
						break;
					default:
						_view.SelectedNode.ImageIndex = 1;
						_view.SelectedNode.SelectedImageIndex = 1;
						break;
				}
				//if(!_isUiUpdated) SetModify();
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Fail add important.", exc);
			}
		}

		private void ViewOnNodeMouseClick(object sender, TreeNodeMouseClickEventArgs treeNodeMouseClickEventArgs)
		{
			//throw new NotImplementedException();
		}

		#endregion
	}
}
