using System;
using System.Drawing;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Widgets;

using Recar2.Algorithms.UI;
using Recar2.Algorithms.Widgets;
using Recar2.MetaEditor.PlateWidget;

using Image = Mallenom.Imaging.Image;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace Recar2.MetaEditor
{
	sealed class ImageWidget : Widget, IDisposable
	{
		public event EventHandler FrameChanged;

		public event EventHandler PlateChanged;

		private readonly RectangleController _frameController;

		private readonly FreePlateRegionController _plateController;

		private readonly SymbolsRegionController _symbolsController;

		private bool _frameUpdated;

		private Point _frameOffset;

		/// <summary>Устанавливает и возвращает рамку области номера.</summary>
		public Rectangle Frame
		{
			get => _frameController.Rect;
			set
			{
				_frameController.Rect = value;
				RecalcFrameOffset();
			}
		}

		[CanBeNull]
		public Point[] PlateCoords
		{
			get => _plateController.Points;
			set
			{
				_plateController.Points = value;
				RecalcFrameOffset();
			}
		}

		[CanBeNull]
		public string Text
		{
			get => _plateController.Text;
			set => _plateController.Text = value;
		}

		public bool ShowVertices
		{
			get => _plateController.ShowVertices;
			set => _plateController.ShowVertices = value;
		}

		public bool IsFrameVisible
		{
			get => _frameController.IsVisible;
			set => _frameController.IsVisible = value;
		}

		public WidgetSymbol[] Symbols
		{
			get => _symbolsController.Symbols;
			set => _symbolsController.Symbols = value;
		}

		public ImageWidget(Image image)
		{
			_frameController = new RectangleController() {Image = image, IsVisible = true, Color = Color.Chartreuse};
			_plateController = new FreePlateRegionController() {Image = image, IsVisible = true};
			_symbolsController = new SymbolsRegionController() {Image = image, Color1 = Color.ForestGreen, Color2 = Color.DodgerBlue};

			_frameController.RegionChanged += FrameControllerOnRegionChanged;

			_plateController.RegionMoved += PlateControllerOnRegionMoved;
			_plateController.RegionChanged += PlateControllerOnRegionChanged;

			Layout = new WidgetLayout()
			{
				Dock = DockStyle.Fill,
			};

			_symbolsController.SymbolAdded += (sender, args) => Children.Add(args.Widget);
			_symbolsController.SymbolRemoved += (sender, args) => Children.Remove(args.Widget);

			Children.Add(_frameController.Widget);
			Children.Add(_plateController.Widget);
		}

		public void Dispose()
		{
			_plateController.RegionChanged -= PlateControllerOnRegionChanged;
			_plateController.RegionMoved -= PlateControllerOnRegionMoved;
			_frameController.RegionChanged -= FrameControllerOnRegionChanged;
			_symbolsController.Dispose();
			_plateController.Dispose();
			_frameController.Dispose();
		}

		private void FrameControllerOnRegionChanged(object sender, EventArgs args)
		{
			if(_frameUpdated) return;
			RecalcFrameOffset();
			FrameChanged?.Invoke(this, args);
		}

		private void PlateControllerOnRegionChanged(object sender, EventArgs args)
		{
			RecalcFrameOffset();
			PlateChanged?.Invoke(this, args);
		}

		private void PlateControllerOnRegionMoved(object sender, EventArgs args)
		{
			_frameUpdated = true;
			var plateLocation = _plateController.Points.ToBoundedRectangle().Location;
			var rect = _frameController.Rect;
			rect.Location = new Point(plateLocation.X + _frameOffset.X, plateLocation.Y + _frameOffset.Y);
			_frameController.Rect = rect;
			_frameUpdated = false;
			PlateChanged?.Invoke(this, args);
			FrameChanged?.Invoke(this, args);
		}

		private void RecalcFrameOffset()
		{
			var points = _plateController.Points;
			if(points != null && points.Length != 0)
			{
				var plateLocation = points.ToBoundedRectangle().Location;
				_frameOffset = new Point(_frameController.Rect.Location.X - plateLocation.X, _frameController.Rect.Location.Y - plateLocation.Y);
			}
			else _frameOffset = default;
		}
	}
}
