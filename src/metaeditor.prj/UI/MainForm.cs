using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Diagnostics.Logs;
using Mallenom.Imaging;
using Mallenom.IPL;
using Mallenom.Storage;
using Mallenom.Widgets;

using Recar2.Algorithms;
using Recar2.Algorithms.UI;
using Recar2.Algorithms.Widgets;
using Recar2.Harvest;
using Recar2.ImageMetadatas;
using Recar2.MetaEditor.Properties;
using Recar2.MetaEditor.UI;

namespace Recar2.MetaEditor
{
	sealed partial class MainForm : Form
	{
		enum ShowMode
		{
			Markup,
			Result
		}

		private const string SelectDirectoryTemplate = "Select directory...";

		#region Data

		private static readonly ILog Log = LogManager.GetCurrentClassLog();

		private ShowMode _showMode = ShowMode.Markup;

		private readonly Core _core;

		private readonly TreeViewer _treeViewer;

		/// <summary>Флаг, указывающий, что на форме изменены данные и они не были сохранены.</summary>
		private bool _isModified;

		private string _imageDirectory;

		private string _currentImageFilename;

		private readonly ControlWidgetAdapter _widgetPlateAdapter;

		private readonly ImageWidget _imagePlateWidget;

		private ImageBrightness _brightness;

		private readonly StencilsProvider _stencilsProvider;

		private int _lbxPlatesIndex = -1;

		private bool _cutImage;

		private TreeNode _cutNode;

		private int _suspendUpdateCounter;

		#endregion

		#region Create & destroy

		public MainForm(Core core)
		{
			_core = core;

			InitializeComponent();
			Icon = Resources.Icon;
			Font = SystemFonts.DefaultFont;

			Text = $@"{Application.ProductName} v.{Application.ProductVersion}.";

			_treeViewer = new TreeViewer(_trvImages);

			_treeViewer.ImagedataRequred += TreeViewerOnImagedataRequred;
			_treeViewer.DirectorySelected += TreeViewerOnDirectorySelected;
			_treeViewer.ImageSelected += OnSelectedImageNode;
			
			_widgetPlateAdapter = new ControlWidgetAdapter(_imgPlate)
			{
				RootWidget = _imagePlateWidget = new ImageWidget(_imgPlate)
				{
					ShowVertices = false,
					IsEnabled = false,
					HandlesMouseEvents = false,
				},
			};

			_stencilsProvider = new StencilsProvider();

			_image.PlateChanged += OnImageWidgetPlateChanged;
			_image.PrevButtonClick += OnPrevButtonClick;
			_image.NextButtonClick += OnNextButtonClick;

			_imgPlate.Smooth = true;

			_cmbCountries.Sorted = true;
			_cmbStencils.Sorted = true;
		}

		protected override void Dispose(bool disposing)
		{
			_treeViewer.ImageSelected -= OnSelectedImageNode;

			_image.PrevButtonClick -= OnPrevButtonClick;
			_image.NextButtonClick -= OnNextButtonClick;
			_image.PlateChanged -= OnImageWidgetPlateChanged;

			_treeViewer.ImagedataRequred -= TreeViewerOnImagedataRequred;
			_treeViewer.DirectorySelected -= TreeViewerOnDirectorySelected;
			_treeViewer.ImageSelected -= OnSelectedImageNode;

			_treeViewer.Dispose();

			_widgetPlateAdapter.Dispose();
			_imagePlateWidget.Dispose();

			if(disposing)
			{
				components?.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Methods

		private void SelectImageDirectory(string directory)
		{
			var index = _cmbImagesDirectory.Items.IndexOf(directory);
			if(index == -1)
			{
				index = 0;
				_cmbImagesDirectory.Items.Insert(index , directory);
			}
			_cmbImagesDirectory.SelectedIndex = index;

			_imageDirectory = directory;
		}

		private void ReloadTree()
		{
			_treeViewer.ReloadTree(_imageDirectory);
		}

		private void LoadCountries()
		{
			_cmbCountries.Items.Clear();
			foreach(var modelDescription in _stencilsProvider.GetModels())
			{
				_cmbCountries.Items.Add(new CountryItem(modelDescription));
			}
			_cmbCountries.Items.Add(CountryItem.Unregistered);
		}

		private void LoadedStencils()
		{
			try
			{
				_cmbStencils.Items.Clear();
				if(_cmbCountries.SelectedItem is CountryItem country && country != CountryItem.Unregistered)
				{
					foreach(var stencil in _stencilsProvider.GetStencils(country.ModelId))
					{
						_cmbStencils.Items.Add(new StencilItem(stencil));
					}
				}
				_cmbStencils.Items.Add(StencilItem.Unregistered);
				if(_cmbStencils.Items.Count > 0) _cmbStencils.SelectedIndex = 0;
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Load stencils fail.", exc);
			}
		}

		/// <summary>Проверяет, если не сохраненные данные на форме.</summary>
		private void CheckUnsaved()
		{
			try
			{
				if(_isModified)
				{
					var result = MessageBox.Show("Data not saved.\nSave?", "Saving data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if(result == DialogResult.Yes)
					{
						SaveMetadata();
					}
					else
					{
						_isModified = false;
						_btnSaveMetadata.Enabled = _isModified;
						_btnSaveMetadataAndNext.Enabled = _isModified;
					}
				}
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Save fail.", exc);
			}
		}

		/// <summary>Сохраняет текущие метаданные в файл.</summary>
		private void SaveMetadata()
		{
			if(_lbxPlates.Items.Count == 0) throw new InvalidOperationException("You didn't enter a car number.");

			if(_lbxPlates.SelectedItem is PlateMetadata plate)
			{
				ApplyPlate(plate);
			}

			var metadata = new ImageMetadata
			{
				Brightness = _rdiLight.Checked ? ImageBrightness.Light : ImageBrightness.Dark,
				Description = _txtDescription.Text,
				Plates = _lbxPlates.Items.Cast<PlateMetadata>().ToArray(),
				HumanChecked = true,
				Important = _trvImages.SelectedNode.ImageIndex == 0,
			};

			_core.SaveMetadata(_currentImageFilename, metadata);

			_isModified = false;
			_btnSaveMetadata.Enabled = _isModified;
			_btnSaveMetadataAndNext.Enabled = _isModified;

			_treeViewer.ReloadSelectedNode();
		}

		private void ApplyPlate(PlateMetadata plate)
		{
			plate.Country = (_cmbCountries.SelectedItem as CountryItem)?.ModelId;
			plate.Stencil = (_cmbStencils.SelectedItem as StencilItem)?.StencilId;
			plate.Quality = Core.Quality(_rdiHigh.Checked, _rdiNormal.Checked);
			_image.PlateCoordinates?.CopyTo(plate.Coordinates, 0);
		}

		/// <summary>Добавление нового номера.</summary>
		private void AddNewPlate()
		{
			if(string.IsNullOrWhiteSpace(_txtPlate.Text)) return;

			try
			{
				var newPlate = Core.SanitizePlate(_txtPlate.Text);

				if(_lbxPlates.FindString(newPlate) != -1)
				{
					MessageBox.Show("This number is already there!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				SearchTemplateEnteredNumber(newPlate);

				var plate = new PlateMetadata
				{
					Country = (_cmbCountries.SelectedItem as CountryItem)?.ModelId,
					Stencil = (_cmbStencils.SelectedItem as StencilItem)?.StencilId,
					Number = newPlate,
				};

				_lbxPlates.Items.Add(plate);
				_lbxPlates.SelectedItem = plate;

				_image.IsPlateVisible = true;
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Add plate fail.", exc);
			}
		}

		private void SearchTemplateEnteredNumber(string number)
		{
			var country = _cmbCountries.SelectedItem as CountryItem;
			var stencil = Core.FormatNumberToStencils(number, country?.ModelId);
			var searchResult = string.Empty;

			foreach(var item in _cmbStencils.Items.Cast<StencilItem>())
			{
				if(string.IsNullOrEmpty(item.StencilId)) continue;

				var array = item.StencilId.Split('_');
				if(array.Length == 4)
				{
					var stroke = array[2] + "_" + array[3];

					if(!stroke.Equals(stencil)) continue;
					searchResult = item.StencilId;
					break;
				}
				if(!array[2].Equals(stencil)) continue;
				searchResult = item.StencilId;
			}
			UpdateStencil(searchResult);
		}

		private void RemoveSelectedPlate()
		{
			try
			{
				if(_lbxPlates.Items.Count == 0) return;

				if(MessageBox.Show(this, @"Delete number?", @"Delete a number", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

				_lbxPlatesIndex = -1;
				_lbxPlates.Items.RemoveAt(_lbxPlates.SelectedIndex);

				if(_lbxPlates.Items.Count > 0) _lbxPlates.SelectedIndex = _lbxPlates.Items.Count - 1;

				_isModified = true;
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Remove plate fail.", exc);
			}
		}

		private void ChangePlate()
		{
			try
			{
				if(_lbxPlates.Items.Count == 0) return;

				if(_lbxPlates.SelectedItem is PlateMetadata plate)
				{
					SuspendUpdate();
					Core.ChangeNumber(_txtPlate.Text, plate);
					_txtPlate.Text = plate.Number;
					var index = _lbxPlates.SelectedIndex;
					_lbxPlates.SelectedIndexChanged -= _lbxPlates_SelectedIndexChanged;
					_lbxPlates.Items.RemoveAt(index);
					_lbxPlates.Items.Insert(index, plate);
					_lbxPlates.SelectedIndex = index;
					_lbxPlates.SelectedIndexChanged += _lbxPlates_SelectedIndexChanged;
				}
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error(exc.Message, exc);
			}
			finally
			{
				ResumeUpdate();
			}
		}

		private void SetModify()
		{
			if(IsSuspendUpdate) return;

			_isModified = true;
			_btnSaveMetadata.Enabled = _isModified;
			_btnSaveMetadataAndNext.Enabled = _isModified;
		}

		private void UpdatePlateImage([CanBeNull]PlateMetadata plate = null, [CanBeNull]Decision decision = null)
		{
			try
			{
				var coordinates = _image.PlateCoordinates;
				if(coordinates != null && coordinates.Any())
				{
					if(decision != null)
					{
						var rect = coordinates.ToBoundedRectangle();
						_imgPlate.FooterText = $"{decision.Plate.Number} [{decision.Plate.Stencil}], {rect}";
						rect.Inflate(15, 6);
						rect.Intersect(new Rectangle(Point.Empty, _image.ImageSize));
						_image.Matrix.CopyTo(_imgPlate.Matrix, rect);
						if(_imgPlate.Matrix is Matrix m)
						{
							Enhancement.ContrastStretch(m);
						}
						_imgPlate.Invalidate();

						var offsetX = -rect.X;
						var offsetY = -rect.Y;
						_imagePlateWidget.PlateCoords = coordinates.Select(c =>
						{
							c.Offset(offsetX, offsetY);
							return c;
						}).ToArray();

						var symbols = new List<WidgetSymbol>();
						foreach(var symbol in decision.Symbols)
						{
							var ws = new WidgetSymbol()
							{
								Char = symbol.Char.ToString(),
								Bounds = symbol.Bounds.Offset(offsetX, offsetY).ToRect(),
							};
							symbols.Add(ws);
						}
						_imagePlateWidget.Symbols = symbols.ToArray();
						_imagePlateWidget.IsVisible = true;
					}
					else
					{
						var rect = coordinates.ToBoundedRectangle();
						if(plate != null) _imgPlate.FooterText = $"{plate.Number} [{plate.Stencil}], {rect}";
						rect.Inflate(15, 6);
						rect.Intersect(new Rectangle(Point.Empty, _image.ImageSize));
						_image.Matrix.CopyTo(_imgPlate.Matrix, rect);
						if(_imgPlate.Matrix is Matrix m)
						{
							Enhancement.ContrastStretch(m);
						}
						_imgPlate.Invalidate();

						var offsetX = -rect.X;
						var offsetY = -rect.Y;
						_imagePlateWidget.PlateCoords = coordinates.Select(c =>
						{
							c.Offset(offsetX, offsetY);
							return c;
						}).ToArray();
						_imagePlateWidget.Symbols = default;
						_imagePlateWidget.IsVisible = true;
					}
				}
				else
				{
					_imgPlate.FooterText = string.Empty;
				}
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error(exc.Message, exc);
			}
		}

		#endregion

		#region Update UI

		private bool IsSuspendUpdate => _suspendUpdateCounter != 0;

		private void SuspendUpdate() => ++_suspendUpdateCounter;

		private void ResumeUpdate()
		{
			if(_suspendUpdateCounter == 0) throw new InvalidOperationException($"Call {nameof(ResumeUpdate)}() without {nameof(SuspendUpdate)}.");
			--_suspendUpdateCounter;
		}

		/// <summary>Обновляет номер на UI из указанных метаданных.</summary>
		/// <param name="plate"></param>
		private void UpdateUIPlate(PlateMetadata plate)
		{
			Assert.IsNotNull(plate);

			SuspendUpdate();

			_txtPlate.Text = plate.Number;

			var countryId = plate.Country;
			if(string.IsNullOrWhiteSpace(countryId)) // Если страна не указана
			{
				countryId = _stencilsProvider.GetModel(plate.Stencil)?.Id;
			}
			UpdateCountry(countryId);
			UpdateStencil(plate.Stencil);
			UpdatePlateQuality(plate);
			UpdateImage(plate);

			ResumeUpdate();
		}

		private void UpdateUIPlate(Decision decision)
		{
			Assert.IsNotNull(decision);

			SuspendUpdate();

			_txtPlate.Text = decision.Plate.Number;

			var countryId = _stencilsProvider.GetModel(decision.Plate.Stencil)?.Id;
			UpdateCountry(countryId);
			UpdateStencil(decision.Plate.Stencil);
			UpdateImages(decision);

			ResumeUpdate();
		}

		private void UpdateCountry(string countryId)
		{
			_cmbCountries.SelectedItem = _cmbCountries.Items.Cast<CountryItem>().FirstOrDefault(it => it.ModelId.Equals(countryId, StringComparison.OrdinalIgnoreCase))
			                             ?? _cmbCountries.Items.Cast<CountryItem>().First(it => it == CountryItem.Unregistered);
		}

		private void UpdateStencil(string stencilId)
		{
			_cmbStencils.SelectedItem = _cmbStencils.Items.Cast<StencilItem>().FirstOrDefault(it => it.StencilId.Equals(stencilId, StringComparison.OrdinalIgnoreCase))
			                            ?? _cmbStencils.Items.Cast<StencilItem>().First(it => it == StencilItem.Unregistered);
		}

		private void UpdateImage(PlateMetadata plate)
		{
			_image.UpdatePlate(plate);
			UpdatePlateImage(plate);
			_image.IsPlateVisible = true;
		}

		private void UpdateImages(Decision decision)
		{
			_image.UpdateDecision(decision);
			UpdatePlateImage(decision: decision);
			_image.IsPlateVisible = true;
		}

		private void UpdateBrightness(ImageMetadata metadata)
		{
			if(metadata.Brightness == _brightness) return;

			switch(metadata.Brightness)
			{
				case ImageBrightness.Light:
					_rdiLight.Checked = true;
					break;
				case ImageBrightness.Dark:
					_rdiDark.Checked = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			_brightness = metadata.Brightness;
		}

		/// <summary>Изменяет качество номера в зависимости от выбранного.</summary>
		private void UpdatePlateQuality(PlateMetadata plate)
		{
			if(plate == null) return;

			switch(plate.Quality)
			{
				case PlateQuality.High:
					_rdiHigh.Checked = true;
					break;
				case PlateQuality.Normal:
					_rdiNormal.Checked = true;
					break;
				case PlateQuality.Low:
					_rdiLow.Checked = true;
					break;
			}
		}

		#endregion

		#region Load & Save configuration

		public void LoadConfiguration(IObjectStorageReader reader)
		{
			SuspendUpdate();

			var recentReader = reader.TryGetChildStorage("RecentImageDirectories");
			if(recentReader != null)
			{
				var items = recentReader.GetChildStorages().Select(sr => sr.ReadAttribute<string>("Value")).ToArray();
				_cmbImagesDirectory.Items.Clear();
				_cmbImagesDirectory.Items.AddRange(items);
				_cmbImagesDirectory.Items.Add(SelectDirectoryTemplate);
			}

			var imageDirectory = reader.TryReadParameter<string>("ImagesDirectory", () => string.Empty);
			if(!string.IsNullOrEmpty(imageDirectory))
			{
				SelectImageDirectory(imageDirectory);
			}

			ResumeUpdate();

			//_selectedDirectoryNode = reader.TryReadParameter<string>("SelectedNode", () => string.Empty);

			var layout = reader.TryGetChildStorage("Layout");
			if(layout != null)
			{
				var x = layout.TryReadParameter("X", Location.X);
				var y = layout.TryReadParameter("Y", Location.Y);
				var width = layout.TryReadParameter("Width", Size.Width);
				var height = layout.TryReadParameter("Height", Size.Height);
				SetBounds(x, y, width, height);
				_sptImage.SplitterDistance = layout.TryReadParameter("ImageSpliter", _sptImage.SplitterDistance);
				_sptTree.SplitterDistance = layout.TryReadParameter("TreeSpliter", _sptTree.SplitterDistance);
				_showMode = layout.TryReadParameter("ShowMode", _showMode);
			}
		}

		public void SaveConfiguration(IObjectStorageWriter writer)
		{
			writer.WriteCollection("RecentImageDirectories", "Item",
				_cmbImagesDirectory.Items.Cast<string>()
					.Where(s => !s.Equals(SelectDirectoryTemplate, StringComparison.OrdinalIgnoreCase))
					.Take(10),
				(sw, s) => sw.WriteAttribute("Value", s));
			writer.WriteParameter("ImagesDirectory", _imageDirectory);

			var node = _trvImages.SelectedNode;
			if(node != null)
			{
				writer.WriteParameter("SelectedNode", node.FullPath);
			}

			var layout = writer.BeginChildObject("Layout");
			layout.WriteParameter("X", Location.X);
			layout.WriteParameter("Y", Location.Y);
			layout.WriteParameter("Width", Size.Width);
			layout.WriteParameter("Height", Size.Height);
			layout.WriteParameter("ImageSpliter", _sptImage.SplitterDistance);
			layout.WriteParameter("TreeSpliter", _sptTree.SplitterDistance);
			layout.WriteParameter("ShowMode", _showMode);
			writer.EndChildObject(layout);
		}

		#endregion

		#region Event handlers

		private void TreeViewerOnImagedataRequred(object sender, ImageDataEventArgs args)
		{
			var metadata = _core.LoadMetadata(args.Path) ?? new ImageMetadata();
			//var metadata = await _core.LoadMetadataAsync(args.Path, CancellationToken.None) ?? new ImageMetadata();
			var criterion = new FilterCriterion()
			{
				Pattern = _txtTreeFilter.Text,
				OnlyImportant = _chkImportant.Checked,
			};
			if(criterion.IsAllow(metadata))
			{
				args.Metadata = metadata;
			}
			var resultFilename = Path.ChangeExtension(args.Path, "result.xml");
			if(File.Exists(resultFilename))
			{
				try
				{
					args.AlgorithmsMetadata = XmlStorage.LoadResult(resultFilename);
				}
				catch(Exception exc) when(!exc.IsCritical())
				{
					Log.Warn($"Load result file fail: '{resultFilename}'.");
				}
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				SuspendUpdate();

				if(_cmbImagesDirectory.Items.Count == 0)
				{
					_cmbImagesDirectory.Items.Add(SelectDirectoryTemplate);
					_cmbImagesDirectory.SelectedIndex = 0;
				}

				KeyPreview = true;

				switch(_showMode)
				{
					case ShowMode.Markup:
						_rdiMarkup.Checked = true;
						break;
					case ShowMode.Result:
						_rdiAlgorithmResult.Checked = true;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				_btnSaveMetadata.Enabled = _isModified;
				_btnSaveMetadataAndNext.Enabled = _isModified;

				LoadCountries();
				_cmbCountries.SelectedIndex = 0;

				SelectImageDirectory(_imageDirectory);

				_treeViewer.ReloadTree(_imageDirectory);
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Load stencils or images error.", exc);
			}
			finally
			{
				ResumeUpdate();
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			CheckUnsaved();
		}

		private void _cmbImagesDirectory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(IsSuspendUpdate) return;

			CheckUnsaved();

			if(_cmbImagesDirectory.SelectedItem is string directory)
			{
				if(directory.Equals(SelectDirectoryTemplate, StringComparison.OrdinalIgnoreCase))
				{
					using(var form = new FolderBrowserDialog()
					{
						Description = "Select images directory",
						SelectedPath = _imageDirectory
					})
					{
						if(form.ShowDialog(this) != DialogResult.OK) return;
						directory = form.SelectedPath;
					}
				}
				SelectImageDirectory(directory);
				ReloadTree();
			}
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs args)
		{
			if(args.KeyCode == Keys.Escape)
			{
				if(MessageBox.Show(this, "Close application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					Close();
				}
			}

			if(args.Control)
				switch(args.KeyCode)
				{
					case Keys.T:
						_trvImages.Focus();
						break;
					case Keys.N:
						_lbxPlates.Focus();
						break;
					case Keys.L:
						_rdiLight.Focus();
						break;
					case Keys.S:
						SaveMetadata();
						break;
					case Keys.M:
						_cmbStencils.Focus();
						break;
					case Keys.Q:
						_rdiNormal.Focus();
						break;
					case Keys.A:
						AddNewPlate();
						break;
					case Keys.D:
						RemoveSelectedPlate();
						break;
					case Keys.W:
						_txtPlate.Focus();
						break;
					case Keys.R:
						_cmbCountries.Focus();
						break;
				}
		}

		private void TreeViewerOnDirectorySelected(object sender, DirectoryEventArgs args)
		{
			var item = args.Item;
			var builder = new StringBuilder();
			builder.AppendLine(item.Path);
			var statistics = item.Statistics;
			builder.AppendLine($"Recog: {statistics.RecogImageCount} / markup: {statistics.MarkupImageCount} / total: {statistics.TotalImageCount}.");
			foreach(var stencil in statistics.Stencils.OrderBy(p => p.Key))
			{
				builder.AppendLine($"{stencil.Key}: markup: {stencil.Value.MarkupPlateCount}, total: {stencil.Value.TotalPlateCount}");
			}
			_txtDirectoryInfo.Text = builder.ToString();
		}

		private void OnSelectedImageNode(object sender, ImageEventArgs args)
		{
			CheckUnsaved();

			try
			{
				SuspendUpdate();
				var item = args.Item;
				_image.LoadImage(item.Path);
				UpdateImageData(item);
				Log.Info($"Load image: '{item.Path}'.");
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Load image or metadata fail.", exc);
			}
			finally
			{
				ResumeUpdate();
				_isModified = false;
			}
		}

		private void UpdateImageData(ImageItem item)
		{
			Verify.Argument.IsNotNull(item, nameof(item));

			_imgPlate.Clear();
			_imgPlate.FooterText = string.Empty;
			_imagePlateWidget.IsVisible = false;
			_currentImageFilename = item.Path;

			var metadata = item.Metadata;
			if(metadata != null)
			{
				UpdateBrightness(metadata);
				_txtDescription.Text = metadata.Description;
				_lbxPlates.Items.Clear();
				_lbxPlatesIndex = -1;
				_txtPlate.Text = string.Empty;
				_image.IsPlateVisible = false;
				_trvImages.SelectedNode.ImageIndex = metadata.Important ? 0 : 1;
				_trvImages.SelectedNode.SelectedImageIndex = metadata.Important ? 0 : 1;
			}

			switch(_showMode)
			{
				case ShowMode.Markup:
					if(metadata?.Plates != null && metadata.Plates.Any())
					{
						_lbxPlates.Items.AddRange(metadata.Plates);
					}
					break;
				case ShowMode.Result:
					if(item.AlgorithmMetadata != null)
					{
						foreach(var decision in item.AlgorithmMetadata.Intermediated.TryGet<Decision[]>())
						{
							_lbxPlates.Items.Add(decision);
						}
					}
					break;
			}
			if(_lbxPlates.Items.Count != 0) _lbxPlates.SelectedIndex = 0;

			_txtImageInfo.Text = $"{item.Path}";
			_txtImageInfo.BackColor = (metadata != null) ? metadata.HumanChecked ? Color.LightGreen : Color.LightSalmon : Color.Gray;
		}

		private void OnParameterChanged(object sender, EventArgs e)
		{
			SetModify();
		}

		private void _btnSaveMetadata_Click(object sender, EventArgs e)
		{
			ChangePlate();
			try
			{
				SaveMetadata();
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Save data fail.", exc);
			}
		}

		private void _btnSaveMetadataAndNext_Click(object sender, EventArgs e)
		{
			ChangePlate();
			SaveAndNextImage();
		}

		private void _btnAddPlate_Click(object sender, EventArgs e)
		{
			AddNewPlate();
		}

		private void _btnRemovePlate_Click(object sender, EventArgs e)
		{
			RemoveSelectedPlate();
		}

		private void OnPrevButtonClick(object sender, EventArgs e)
		{
			_treeViewer.PrevImage();
		}

		private void OnNextButtonClick(object sender, EventArgs e)
		{
			_treeViewer.NextImage();
		}

		private void _cmbCountries_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetModify();
			LoadedStencils();
		}

		private void _cmbStencil_SelectedIndexChanged(object sender, EventArgs args)
		{
			try
			{
				if(_cmbStencils.SelectedItem is StencilItem stencil && stencil != StencilItem.Unregistered)
				{
					var modelId = _stencilsProvider.GetModel(stencil.StencilId);
					var item = _cmbCountries.Items.Cast<CountryItem>().FirstOrDefault(it => it.ModelId.Equals(modelId.Id));
					_cmbCountries.SelectedItem = item;
					_ctrStencil.StencilId = stencil.StencilId;
					_ctrStencil.Text = _txtPlate.Text;
				}
				else
				{
					_ctrStencil.StencilId = null;
				}

				SetModify();
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Stencil image not found.", exc);
			}
		}

		private void _txtPlate_TextChanged(object sender, EventArgs e)
		{
			var plate = _txtPlate.Text;
			_ctrStencil.Text = plate;

			SetModify();
		}

		private void _txtPlate_KeyDown(object sender, KeyEventArgs args)
		{
			switch(args.KeyCode)
			{
				case Keys.Enter:
					ChangePlate();
					break;
			}
		}

		private void _lbxPlates_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(_lbxPlatesIndex != -1)
			{
				if(_lbxPlates.Items[_lbxPlatesIndex] is PlateMetadata p)
				{
					ApplyPlate(p);
				}
			}
			if(_lbxPlates.SelectedItem is PlateMetadata plate)
			{
				UpdateUIPlate(plate);
			}
			if(_lbxPlates.SelectedItem is Decision decision)
			{
				UpdateUIPlate(decision);
			}
			_lbxPlatesIndex = _lbxPlates.SelectedIndex;
		}

		private void _rdiHigh_CheckedChanged(object sender, EventArgs e)
		{
			if(IsSuspendUpdate) return;

			if(_lbxPlates.SelectedItem is PlateMetadata plate)
			{
				plate.Quality = PlateQuality.High;
				SetModify();
			}
		}

		private void _rdiNormal_CheckedChanged(object sender, EventArgs e)
		{
			if(IsSuspendUpdate) return;

			if(_lbxPlates.SelectedItem is PlateMetadata plate)
			{
				plate.Quality = PlateQuality.Normal;
				SetModify();
			}
		}

		private void _rdiLow_CheckedChanged(object sender, EventArgs e)
		{
			if(IsSuspendUpdate) return;

			if(_lbxPlates.SelectedItem is PlateMetadata plate)
			{
				plate.Quality = PlateQuality.Low;
				SetModify();
			}
		}

		private void _btnStatistics_Click(object sender, EventArgs e)
		{
		}

		private void _txtDescription_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				_txtPlate.Focus();
			}
		}

		private void _btnApplyFilter_Click(object sender, EventArgs e)
		{
			ReloadTree();
		}

		private void OnImageWidgetPlateChanged(object sender, EventArgs e)
		{
			UpdatePlateImage();
			SetModify();
		}

		private void _chkImportant_CheckedChanged(object sender, EventArgs e)
		{
			ReloadTree();
		}

		private void _copy_Click(object sender, EventArgs e)
		{
			var node = _trvImages.SelectedNode;
			var treeItem = (DirectoryItem)node?.Tag;
			Clipboard.SetText(treeItem.Path);
		}

		private void _cut_Click(object sender, EventArgs e)
		{
			try
			{
				var node = _trvImages.SelectedNode;
				node.ForeColor = Color.BurlyWood;
				_cutImage = true;
				_cutNode = node;
				Clipboard.SetText(node?.Tag.ToString());
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error(exc.Message, exc);
			}
		}

		private void _paste_Click(object sender, EventArgs e)
		{
			try
			{
				if(_cutImage)
				{
					var pastDirectory = Path.GetDirectoryName(_trvImages.SelectedNode?.Tag.ToString());
					var cutFile = Clipboard.GetText();
					if(Path.GetDirectoryName(cutFile).Equals(pastDirectory))
					{
						MessageBox.Show(@"Вы указали ту же директорию. Выберите другую.", "Ошибка");
						_cutImage = true;
						return;
					}

					File.Copy(cutFile, Path.Combine(pastDirectory, Path.GetFileName(cutFile)));
					_image.Clear();
					_imgPlate.Clear();
					_cutNode.Remove();
					Clipboard.Clear();
					File.Delete(cutFile);
					_cutImage = false;
				}
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error(exc.Message, exc);
			}
		}

		private void _newVersionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://dev.mallenom.ru:8111/viewType.html?buildTypeId=automarshal_utility_AlgorithmsMetaeditor");
		}

		private void _redmineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://dev.mallenom.ru:3000/projects/automarshal-utility/issues?query_id=187");
		}

		private void _statisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(_image.ImageSize.Width == 0)
			{
				MessageBox.Show("No image selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			var form = new Statistics(_imageDirectory, _image.ImageSize.Width);
			form.Show();
		}

		private void _lnkRefreshInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_treeViewer.ReloadSelectedNode();
		}

		private void OnShowModeChanged(object sender, EventArgs e)
		{
			if(_showMode == ShowMode.Markup)
			{
				CheckUnsaved();
			}

			_showMode = _rdiMarkup.Checked ? ShowMode.Markup : ShowMode.Result;

			if(_treeViewer.SelectedImageItem != null)
			{
				SuspendUpdate();
				UpdateImageData(_treeViewer.SelectedImageItem);
				ResumeUpdate();
			}

			switch(_showMode)
			{
				case ShowMode.Markup:
					_txtPlate.Enabled = true;
					_txtDescription.Enabled = true;
					_btnRemovePlate.Enabled = true;
					_btnAddPlate.Enabled = true;
					_btnSaveMetadata.Enabled = true;
					_btnSaveMetadataAndNext.Enabled = true;
					_cmbCountries.Enabled = true;
					_cmbStencils.Enabled = true;
					break;
				case ShowMode.Result:
					_txtPlate.Enabled = false;
					_txtDescription.Enabled = false;
					_btnRemovePlate.Enabled = false;
					_btnAddPlate.Enabled = false;
					_btnSaveMetadata.Enabled = false;
					_btnSaveMetadataAndNext.Enabled = false;
					_cmbCountries.Enabled = false;
					_cmbStencils.Enabled = false;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>Сохраняет и переходит к следующему изображению </summary>
		private void SaveAndNextImage()
		{
			try
			{
				SaveMetadata();
				_treeViewer.NextImage();
			}
			catch(Exception exc) when(!exc.IsCritical())
			{
				Log.Error("Save or go to the next fail.", exc);
			}
		}

		private void _trvImages_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if(MouseButtons.Right == e.Button)
			{
				_cntMenuStrip.Items["paste"].Enabled = false;
				_cntMenuStrip.Items["cut"].Enabled = false;
				//if(Clipboard.ContainsText()) _cntMenuStrip.Items["paste"].Enabled = true;
				//else _cntMenuStrip.Items["paste"].Enabled = false;
				e.Node.ContextMenuStrip = _cntMenuStrip;
				_cntMenuStrip.Show();
			}
		}

		#endregion
	}
}
