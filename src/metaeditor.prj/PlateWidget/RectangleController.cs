using System;
using System.Drawing;
using System.Linq;

using Mallenom;
using Mallenom.Imaging;

using Recar2.Algorithms.Widgets;

using Image = Mallenom.Imaging.Image;

namespace Recar2.MetaEditor.PlateWidget
{
	sealed class RectangleController : IDisposable
	{
		sealed class RegionWidgetState : IRegionWidgetState
		{
			public RegionWidgetState(RegionController controller, Rectangle rectangle)
			{
				Verify.Argument.IsNotNull(controller, nameof(controller));

				Controller = controller;
				Rectangle = rectangle;
			}

			private RegionController Controller { get; }

			public Rectangle Rectangle { get; }

			public bool TryRestore()
			{
				if(Controller.Widget.Host?.Control is Image image)
				{
					var converter = image.CoordConverter;
					if(converter == null) return false;
					Controller.Vertices = Rectangle.ToPoints().Select(p => converter.ConvertMatrixToScreen(new MatrixPoint(p.X, p.Y))).ToArray();
					return true;
				}
				return false;
			}
		}

		#region Events

		public event EventHandler RegionChanged;
		private void OnRegionChanged(EventArgs args) => RegionChanged?.Invoke(this, args);
		private void OnVertexMoved(object sender, EventArgs args) => OnRegionChanged(args);

		#endregion

		private readonly RegionController _regionController;

		private readonly MaintainRegionWidgetStateBehavior _state;

		public Image Image { get; set; }

		public RegionWidget Widget => _regionController.Widget;

		public Rectangle Rect
		{
			get
			{
				if(_state.State is RegionWidgetState state)
				{
					return state.Rectangle;
				}

				if(Image == null || _regionController.Vertices.Length == 0) return default;

				var converter = Image.CoordConverter;
				return _regionController.Vertices
					.Select(p => converter.ConvertScreenToMatrix(p)).Select(p => new Point(p.X, p.Y))
					.ToArray()
					.ToBoundedRectangle();
			}
			set
			{
				_state.State = new RegionWidgetState(_regionController, value);
				_state.TryRestoreState();
			}
		}

		public bool IsVisible
		{
			get => _regionController.IsVisible;
			set => _regionController.IsVisible = value;
		}

		public Color Color
		{
			get => _regionController.Color;
			set => _regionController.Color = value;
		}

		public string Text
		{
			get => _regionController.Text;
			set => _regionController.Text = value;
		}

		public bool ShowVertices
		{
			get => _regionController.ShowVertices;
			set => _regionController.ShowVertices = value;
		}

		public RectangleController()
		{
			var editable = new EditableRectangleBehavior();
			var movable = new MovableRegionBehavior();
			_state = new MaintainRegionWidgetStateBehavior();

			editable.VertexMoved += OnVertexMoved;
			movable.RegionMoved += OnVertexMoved;

			_regionController = new RegionController(RegionWidgetFlags.OutsideShading);
			_regionController.Widget.Behaviors.Add(new SyncBoundsToImageBehavior());
			_regionController.Widget.Behaviors.Add(_state);
			//_regionController.Widget.Behaviors.Add(movable);
			_regionController.Widget.Behaviors.Add(editable);
		}

		public void Dispose()
		{
			_regionController.Dispose();
		}
	}
}
