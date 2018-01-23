using System;
using System.Drawing;
using System.Linq;

using Mallenom;
using Mallenom.Imaging;

using Recar2.Algorithms.Widgets;

using Image = Mallenom.Imaging.Image;

namespace Recar2.Algorithms.UI
{
	sealed class FreePlateRegionController : IDisposable
	{
		sealed class PointsRegionWidgetState : IRegionWidgetState
		{
			[NotNull]
			private readonly RegionController _controller;

			public Point[] Points { get; }

			public PointsRegionWidgetState(RegionController controller, Point[] points)
			{
				Verify.Argument.IsNotNull(controller, nameof(controller));

				_controller = controller;
				Points = points;
			}

			public bool TryRestore()
			{
				if(_controller.Widget.Host?.Control is Image image)
				{
					var viewRect = image.ViewRect;
					if(viewRect.Width <= 0 || viewRect.Height <= 0)
					{
						return false;
					}
					var converter =  image.CoordConverter;
					_controller.Vertices = Points?.Select(p => converter.ConvertMatrixToScreen(new MatrixPoint(p.X, p.Y))).ToArray();
					return true;
				}
				return false;
			}
		}

		public event EventHandler RegionChanged;

		private void OnRegionChanged(EventArgs args) => RegionChanged?.Invoke(this, args);

		private void OnVertexMoved(object sender, EventArgs args)
		{
			_state.State = new PointsRegionWidgetState(_regionController, Points);
			OnRegionChanged(args);
		}

		public event EventHandler RegionMoved;

		private void OnRegionMoved(EventArgs args) => RegionMoved?.Invoke(this, args);

		private void MovableOnRegionMoved(object sender, EventArgs args)
		{
			_state.State = new PointsRegionWidgetState(_regionController, Points);
			OnRegionMoved(args);
		}

		private readonly RegionController _regionController;

		private readonly MaintainRegionWidgetStateBehavior _state;

		public RegionWidget Widget => _regionController.Widget;

		[CanBeNull]
		public Point[] Points
		{
			get
			{
				if(_state.State is PointsRegionWidgetState state)
				{
					return state.Points;
				}
				if(Image == null)
				{
					return default;
				}
				var converter = Image.CoordConverter;
				return _regionController.Vertices.Select(p => converter.ConvertScreenToMatrix(p)).Select(p => new Point(p.X, p.Y)).ToArray();
			}
			set
			{
				_state.State = new PointsRegionWidgetState(_regionController, value);
				_state.TryRestoreState();
			}
		}

		public Image Image { get; set; }

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

		public FreePlateRegionController()
		{
			var editable = new EditablePolygonBehavior();
			var movable = new MovableRegionBehavior();
			_state = new MaintainRegionWidgetStateBehavior();

			editable.VertexMoved += OnVertexMoved;
			movable.RegionMoved += MovableOnRegionMoved;

			_regionController = new RegionController(RegionWidgetFlags.OutsideShading);
			_regionController.Widget.Behaviors.Add(new SyncBoundsToImageBehavior());
			_regionController.Widget.Behaviors.Add(_state);
			_regionController.Widget.Behaviors.Add(movable);
			_regionController.Widget.Behaviors.Add(editable);
		}

		public void Dispose()
		{
			_regionController.Dispose();
		}
	}
}
