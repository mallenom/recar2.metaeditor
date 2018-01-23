using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

using Mallenom;
using Mallenom.Imaging;
using Mallenom.Localization;

using Recar2.Algorithms.Widgets;

using Image = Mallenom.Imaging.Image;
using NumericUpDown = System.Windows.Forms.NumericUpDown;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace Recar2.Algorithms.UI
{
	/// <summary></summary>
	sealed class PlateRegionController : IDisposable
	{
		sealed class PlateSizeAsRegionWidgetState : IRegionWidgetState
		{
			public PlateSizeAsRegionWidgetState(RegionController controller, PlateSize plateSize)
			{
				Verify.Argument.IsNotNull(controller, nameof(controller));
				Verify.Argument.IsNotNull(plateSize, nameof(plateSize));

				Controller = controller;
				PlateSize = plateSize;
			}

			[NotNull]
			public RegionController Controller { get; }

			[NotNull]
			public PlateSize PlateSize { get; }

			public bool TryRestore()
			{
				if(!(Controller.Widget.Host?.Control is Image image))
				{
					return false;
				}
				else
				{
					var rect = PlateSize.Position;
					switch(PlateSize.Mode)
					{
						case PlateSizeMode.Pixel:
							var converter = image.CoordConverter;
							if(converter == null)
							{
								return false;
							}
							var location = converter.ConvertMatrixToScreen(new MatrixPoint((int)PlateSize.Position.X, (int)PlateSize.Position.Y));
							var size = converter.ConvertScreenToMatrix(Point.Empty, new Point((int)PlateSize.Position.Width, (int)PlateSize.Position.Height));
							Controller.Vertices = ToVertices(new Rectangle(location, Size.Truncate(size)));
							return true;
						case PlateSizeMode.Relative:
							var viewRect = image.ViewRect;
							if(viewRect.Width <= 0 || viewRect.Height <= 0)
							{
								return false;
							}
							var displayRect = new Rectangle(
								viewRect.X + (int)(viewRect.Width * PlateSize.Position.X),
								viewRect.Y + (int)(viewRect.Height * PlateSize.Position.Y),
								(int)(viewRect.Width * PlateSize.Position.Width),
								(int)(viewRect.Height * PlateSize.Position.Height));
							Controller.Vertices = ToVertices(displayRect);
							return true;
						default:
							throw new ArgumentOutOfRangeException();
					}
				}
			}
		}

		/// <summary>Изменена позиция и/или размер рамки.</summary>
		public event EventHandler<RegionChangedEventArgs> PlateChanged;

		//private static LocalizationScope LocalizationScope { get; } = new LocalizationScope($"Recar2.Algorithms.{nameof(ConvenientAlgorithmSetupControl)}");

		private readonly RegionController _regionController;

		private readonly Label _pixelsInfoLabel;

		private readonly UpdateTracker _updateTracker;

		public Image Image { get; set; }

		public RegionWidget Widget => _regionController.Widget;

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

		private Image ZoneImage { get; }

		private readonly EditablePlateBehavior _editable;
		private readonly MovableRegionBehavior _movable;
		private readonly MaintainRegionWidgetStateBehavior _state;

		public PlateRegionController(
			UpdateTracker updateTracker,
			Image zoneImage,
			Label pixelsInfoLabel)
		{
			Verify.Argument.IsNotNull(updateTracker, nameof(updateTracker));

			_updateTracker = updateTracker;

			_regionController = new RegionController();

			_movable  = new MovableRegionBehavior();
			_editable = new EditablePlateBehavior();
			_state    = new MaintainRegionWidgetStateBehavior();

			_movable.RegionMoved  += OnRegionMoved;
			_editable.VertexMoved += OnVertexMoved;

			_regionController.Widget.Behaviors.Add(new SyncBoundsToImageBehavior());
			_regionController.Widget.Behaviors.Add(_state);
			_regionController.Widget.Behaviors.Add(_movable);
			_regionController.Widget.Behaviors.Add(_editable);

			ZoneImage = zoneImage;
			_widthUpDown = widthUpDown;
			_heightUpDown = heightUpDown;
			_pixelsInfoLabel = pixelsInfoLabel;

			_widthUpDown.ValueChanged  += WidthUpDownOnValueChanged;
			_heightUpDown.ValueChanged += HeightUpDownOnValueChanged;
		}

		public void Dispose()
		{
			_regionController.Dispose();

			_movable.RegionMoved  -= OnRegionMoved;
			_editable.VertexMoved -= OnVertexMoved;

			_widthUpDown.ValueChanged  -= WidthUpDownOnValueChanged;
			_heightUpDown.ValueChanged -= HeightUpDownOnValueChanged;
		}

		public void SetPlate(PlateSize plate)
		{
			using(_updateTracker.BeginUpdate())
			{
				if(plate != null)
				{
					_state.State = new PlateSizeAsRegionWidgetState(_regionController, plate);
					_state.TryRestoreState();
				}
				else
				{
					_state.InvalidateState();
					_regionController.Vertices = null;
				}
				CopyFrameSizeToProposed(plate.Mode == PlateSizeMode.Relative ? plate : GetPlate(PlateSizeMode.Relative));
				UpdatePixelsInfoLabel();
			}
		}

		[CanBeNull]
		private PlateSize TryGetPlateSizeFromState(IRegionWidgetState regionWidgetState, PlateSizeMode mode)
		{
			if(regionWidgetState == null)
			{
				return default;
			}
			if(regionWidgetState is PlateSizeAsRegionWidgetState originalState)
			{
				if(originalState.PlateSize.Mode == mode)
				{
					return originalState.PlateSize;
				}
				var matrix = Image?.Matrix;
				if(Utility.MatrixIsConsideredEmpty(matrix))
				{
					return default;
				}
				switch(mode)
				{
					case PlateSizeMode.Relative:
						{
							var position = originalState.PlateSize.Position;
							return new PlateSize(PlateSizeMode.Pixel, new Mallenom.Primitives.RectangleF(
								position.X / matrix.Width, position.Y / matrix.Height, position.Width / matrix.Width, position.Height / matrix.Height));
						}
					case PlateSizeMode.Pixel:
						{
							var position = originalState.PlateSize.Position;
							return new PlateSize(PlateSizeMode.Pixel, new Mallenom.Primitives.RectangleF(
								position.X * matrix.Width, position.Y * matrix.Height, position.Width * matrix.Width, position.Height * matrix.Height));
						}
					default:
						throw new ArgumentOutOfRangeException(nameof(mode), mode, @"Unknown mode");
				}
			}
			if(regionWidgetState is RegionWidgetState savedState)
			{
				if(!savedState.Vertices.Any()) return default(PlateSize);
				var bounds = savedState.Vertices.Select(v => v.Position).ToArray().ToBoundedRectangle();
				if(bounds.Width <= 0 || bounds.Height <= 0)
				{
					return default;
				}
				switch(mode)
				{
					case PlateSizeMode.Pixel:
						{
							var matrix = Image?.Matrix;
							if(Utility.MatrixIsConsideredEmpty(matrix))
							{
								return default(PlateSize);
							}
							var targetBounds = new RectangleF(0, 0, matrix.Width, matrix.Height);
							var location = savedState.ConvertPosition(bounds.Location, targetBounds);
							var size     = savedState.ConvertSize(bounds.Size, targetBounds.Size);
							return new PlateSize(PlateSizeMode.Pixel, new Mallenom.Primitives.RectangleF(
								location.X, location.Y, size.Width, size.Height));
						}
					case PlateSizeMode.Relative:
						{
							var targetBounds = new RectangleF(0, 0, 1, 1);
							var location = savedState.ConvertPosition(bounds.Location, targetBounds);
							var size     = savedState.ConvertSize(bounds.Size, targetBounds.Size);
							return new PlateSize(PlateSizeMode.Relative, new Mallenom.Primitives.RectangleF(
								location.X, location.Y, size.Width, size.Height));
						}
					default:
						throw new ArgumentOutOfRangeException(nameof(mode), mode, @"Unknown mode");
				}
			}
			return default;
		}

		[CanBeNull]
		private PlateSize TryGetPlateSizeFromGeometry(PlateSizeMode mode)
		{
			if(Image == null)
			{
				return default(PlateSize);
			}
			var vertices = _regionController.Vertices;
			if(vertices == null || vertices.Length < 4)
			{
				return default(PlateSize);
			}
			var displayRect = _regionController.Vertices.ToBoundedRectangle();
			if(displayRect.Width <= 0 || displayRect.Height <= 0)
			{
				return default(PlateSize);
			}
			switch(mode)
			{
				case PlateSizeMode.Pixel:
					var converter = Image.CoordConverter;
					if(converter == null)
					{
						return default(PlateSize);
					}
					var location   = converter.ConvertScreenToMatrix(displayRect.Location);
					var size       = converter.ConvertScreenToMatrix(Point.Empty, new Point(displayRect.Width, displayRect.Height));
					var matrixRect = new Mallenom.Primitives.RectangleF(location.X, location.Y, size.Width, size.Height);
					return new PlateSize(PlateSizeMode.Pixel, matrixRect);
				case PlateSizeMode.Relative:
					var viewRect = Image.ViewRect;
					if(viewRect.Width <= 0 || viewRect.Height <= 0)
					{
						return default(PlateSize);
					}
					var x = (displayRect.X - viewRect.X) / (float)viewRect.Width;
					var y = (displayRect.Y - viewRect.Y) / (float)viewRect.Height;
					var w = displayRect.Width  / (float)viewRect.Width;
					var h = displayRect.Height / (float)viewRect.Height;
					return new PlateSize(PlateSizeMode.Relative, new Mallenom.Primitives.RectangleF(x, y, w, h));
				default:
					throw new ArgumentOutOfRangeException(nameof(mode), mode, @"Unknown mode");
			}
		}

		[CanBeNull]
		public PlateSize GetPlate(PlateSizeMode mode)
		{
			return 
				TryGetPlateSizeFromState(_state.State, mode) ??
				TryGetPlateSizeFromGeometry(mode);
		}

		public void NotifyMatrixUpdated(bool sizeChanged)
		{
			CopyToPlateImage();
			if(sizeChanged)
			{
				UpdatePixelsInfoLabel();
			}
		}

		private void CopyToPlateImage()
		{
			if(ZoneImage?.Matrix == null) return;

			var matrix = Image?.Matrix;
			if(Utility.MatrixIsConsideredEmpty(matrix))
			{
				ZoneImage.Matrix.Clear();
			}
			else
			{
				var plateRect = GetPlate(PlateSizeMode.Pixel);
				if(plateRect == null)
				{
					ZoneImage.Matrix.Clear();
				}
				else
				{
					var matrixRect = new Rectangle(Point.Empty, matrix.Size);
					var zoneRect = Rectangle.Intersect(matrixRect, Rectangle.Truncate(plateRect.Position.ToRect()));
					if(zoneRect.Width > 0 && zoneRect.Height > 0)
					{
						matrix.CopyTo(ZoneImage.Matrix, zoneRect);
					}
					else
					{
						ZoneImage.Matrix.Clear();
					}
				}
			}
			ZoneImage.InvalidateCache();
		}

		private void CopyFrameSizeToProposed(PlateSize plate)
		{
			if(plate != null)
			{
				switch(plate.Mode)
				{
					case PlateSizeMode.Relative:
						if(_widthUpDown != null)  _widthUpDown.Value  = plate.Position.Width.ToPercent();
						if(_heightUpDown != null) _heightUpDown.Value = plate.Position.Height.ToPercent();
						break;
					case PlateSizeMode.Pixel:
						if(_widthUpDown != null)  _widthUpDown.Value  = (decimal)plate.Position.Width;
						if(_heightUpDown != null) _heightUpDown.Value = (decimal)plate.Position.Height;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			UpdatePixelsInfoLabel();
		}

		private void CopyCurrentFrameSizeToProposed()
		{
			CopyFrameSizeToProposed(GetPlate(PlateSizeMode.Relative));
		}

		private void UpdatePixelsInfoLabel()
		{
			if(_pixelsInfoLabel != null)
			{
				if(Utility.MatrixIsConsideredEmpty(Image?.Matrix))
				{
					_pixelsInfoLabel.Text = string.Empty;
					return;
				}
				var plateInPixels = GetPlate(PlateSizeMode.Pixel);
				if(plateInPixels != null)
				{
					_pixelsInfoLabel.Text = string.Format(Locale.Current.Culture,
						Locale.Current.GetString(LocalizationScope + "pixString"),
						(int)plateInPixels.Position.Width, (int)plateInPixels.Position.Height);
				}
				else
				{
					_pixelsInfoLabel.Text = string.Empty;
				}
			}
		}

		private void WidthUpDownOnValueChanged(object sender, EventArgs args)
		{
			if(_updateTracker.IsUpdating) return;

			var matrix = Image?.Matrix;
			if(Utility.MatrixIsConsideredEmpty(matrix))
			{
				return;
			}

			var minrwidth = (ConvenientAlgorithmParameters.Minimum.PlateSize.Width * 100) / (double)matrix.Width;
			var rwidth    = Convert.ToDouble(_widthUpDown.Value);
			if(rwidth < minrwidth)
			{
				_widthUpDown.Value = Convert.ToDecimal(minrwidth);
				return;
			}
			using(_updateTracker.BeginUpdate())
			{
				var viewRect    = Image.ViewRect;
				var displayRect = _regionController.Vertices.ToBoundedRectangle();
				var width       = (int)(viewRect.Width * rwidth / 100.0);

				displayRect.Width = width;
				var dx = displayRect.Right - viewRect.Right;
				if(dx > 0)
				{
					displayRect.X -= dx;
				}

				_regionController.Vertices = ToVertices(displayRect);
				UpdatePixelsInfoLabel();
			}
			PlateChanged?.Invoke(this, RegionChangedEventArgs.ForShapeChanged);
		}

		private void HeightUpDownOnValueChanged(object sender, EventArgs args)
		{
			if(_updateTracker.IsUpdating) return;

			var matrix = Image?.Matrix;
			if(Utility.MatrixIsConsideredEmpty(matrix))
			{
				return;
			}

			var minrheight = (ConvenientAlgorithmParameters.Minimum.PlateSize.Height * 100) / (double)matrix.Height;
			var rheight = Convert.ToDouble(_heightUpDown.Value);
			if(rheight < minrheight)
			{
				_heightUpDown.Value = Convert.ToDecimal(minrheight);
				return;
			}
			using(_updateTracker.BeginUpdate())
			{
				var viewRect    = Image.ViewRect;
				var displayRect = _regionController.Vertices.ToBoundedRectangle();
				var height      = (int)(viewRect.Height * rheight / 100.0);

				displayRect.Height = height;
				var dy = displayRect.Bottom - viewRect.Bottom;
				if(dy > 0)
				{
					displayRect.Y -= dy;
				}

				_regionController.Vertices = ToVertices(displayRect);
				UpdatePixelsInfoLabel();
			}
			PlateChanged?.Invoke(this, RegionChangedEventArgs.ForShapeChanged);
		}

		private void OnRegionMoved(object sender, EventArgs e)
		{
			CopyToPlateImage();
			PlateChanged?.Invoke(this, RegionChangedEventArgs.ForLocationChanged);
		}

		private void OnVertexMoved(object sender, EventArgs e)
		{
			if(_updateTracker.IsUpdating) return;

			CopyToPlateImage();
			using(_updateTracker.BeginUpdate())
			{
				CopyCurrentFrameSizeToProposed();
			}
			PlateChanged?.Invoke(this, RegionChangedEventArgs.ForShapeChanged);
		}

		private static Point[] ToVertices(Rectangle displayRect)
		{
			return new[]
			{
				new Point(displayRect.X,     displayRect.Y),      new Point(displayRect.Right, displayRect.Y),
				new Point(displayRect.Right, displayRect.Bottom), new Point(displayRect.X,     displayRect.Bottom),
			};
		}
	}
}
