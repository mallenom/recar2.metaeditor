using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Mallenom.Imaging;
using Mallenom.Widgets;

using Recar2.Algorithms;
using Recar2.ImageMetadatas;

using Image = System.Drawing.Image;

namespace Recar2.MetaEditor.UI
{
	partial class ImageControl : UserControl
	{
		enum ShowMode { Image, Plate }

		public event EventHandler PlateChanged;

		public event EventHandler PrevButtonClick
		{
			add => _buttonsWidget.PrevButtonClick += value;
			remove => _buttonsWidget.PrevButtonClick -= value;
		}

		public event EventHandler NextButtonClick
		{
			add => _buttonsWidget.NextButtonClick += value;
			remove => _buttonsWidget.NextButtonClick -= value;
		}

		#region Data

		private readonly ControlWidgetAdapter _widgetAdapter;

		private readonly ImageWidget _imageWidget;

		private readonly ButtonsWidget _buttonsWidget;

		private ShowMode _mode = ShowMode.Image;

		#endregion

		#region Properties

		public bool IsPlateVisible { get; set; }

		public Size ImageSize => _image.Matrix.Size;

		public IImageMatrix Matrix => _image.Matrix;

		public Point[] PlateCoordinates => _imageWidget.PlateCoords;

		#endregion

		public ImageControl()
		{
			InitializeComponent();

			var root = new Widget
			{
				Layout = new WidgetLayout
				{
					Dock = DockStyle.Fill,
				},
			};

			_imageWidget   = new ImageWidget(_image);
			_buttonsWidget = new ButtonsWidget();

			root.Children.Add(_imageWidget);
			root.Children.Add(_buttonsWidget);
			_widgetAdapter = new ControlWidgetAdapter(_image)
			{
				RootWidget = root,
			};

			_image.DoubleClick += ImageOnDoubleClick;

			_imageWidget.PlateChanged += ImageWidgetOnPlateChanged;

			UpdateImageRect();
		}

		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				_imageWidget.PlateChanged -= ImageWidgetOnPlateChanged;

				_widgetAdapter.Dispose();
				_imageWidget.Dispose();

				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public void Clear()
		{
			_image.Clear();
			UpdatePlate(null);
		}

		public void LoadImage(string path)
		{
			Clear();
			_image.Matrix.Load((Bitmap)Image.FromFile(path));
			_image.FooterText = $@"{_image.Matrix.Width} x {_image.Matrix.Height}";
			UpdateImageRect();
		}

		public void UpdatePlate(PlateMetadata plate)
		{
			if(plate != null)
			{
				var coordinates = plate.Coordinates;
				if(coordinates.All(p => p.IsEmpty))
				{
					var matrixSize = ImageSize;
					var rect = new Rectangle((int)(0.4 * matrixSize.Width), (int)(0.45 * matrixSize.Height), (int)(0.2 * matrixSize.Width), (int)(0.1 * matrixSize.Height));
					coordinates = new[]
					{
						new Point(rect.Left, rect.Top),
						new Point(rect.Right, rect.Top),
						new Point(rect.Right, rect.Bottom),
						new Point(rect.Left, rect.Bottom)
					};
				}
				_imageWidget.PlateCoords = coordinates;
				_image.FooterText = $@"{plate.Number} [{plate.Stencil}], {_image.Matrix.Width} x {_image.Matrix.Height}";
			}
			else
			{
				_imageWidget.PlateCoords = default;
			}
			_imageWidget.ShowVertices = true;
			_imageWidget.Frame = default;
			UpdateImageRect();
			UpdatePlateFrame();
		}


		public void UpdateDecision(Decision decision)
		{
			if(decision != null)
			{
				_imageWidget.PlateCoords = decision.Plate.Region.ToPoints();
				_image.FooterText = $@"{decision.Plate.Number} [{decision.Plate.Stencil}], {_image.Matrix.Width} x {_image.Matrix.Height}";
			}
			else
			{
				_imageWidget.Symbols = default;
				_imageWidget.PlateCoords = default;
			}
			_imageWidget.IsFrameVisible = false;
			_imageWidget.ShowVertices = false;
			_imageWidget.Frame = default;
			UpdateImageRect();
			UpdatePlateFrame();
		}

		private void ImageWidgetOnPlateChanged(object sender, EventArgs args)
		{
			UpdatePlateFrame();
			PlateChanged?.Invoke(this, args);
		}

		private void UpdatePlateFrame()
		{
			if(_imageWidget.PlateCoords == null || _imageWidget.PlateCoords.Length == 0)
			{
				_imageWidget.Text = null;
			}
			else
			{
				var plateRect = _imageWidget.PlateCoords.ToBoundedRectangle();
				var widthPercent = Math.Round(((float)plateRect.Width / _image.Matrix.Width * 100), 1);
				var heightPercent = Math.Round(((float)plateRect.Height / _image.Matrix.Height * 100), 1);
				_imageWidget.Text = $"{plateRect.X}, {plateRect.Y} - {plateRect.Width} x {plateRect.Height}  {widthPercent}% x {heightPercent}%";
			}
		}

		private void ImageOnDoubleClick(object sender, EventArgs eventArgs)
		{
			_mode = (_mode == ShowMode.Image) ? ShowMode.Plate : ShowMode.Image;
			UpdateImageRect();
		}

		private void UpdateImageRect()
		{
			if(_imageWidget.Frame.IsEmpty)
			{
				CreateFrame(_imageWidget.PlateCoords);
			}
			switch(_mode)
			{
				case ShowMode.Image:
					_image.MatrixRect = new Rectangle(0, 0, _image.Matrix.Width, _image.Matrix.Height);
					_imageWidget.IsFrameVisible = true;
					break;
				case ShowMode.Plate:
					if(!_imageWidget.Frame.IsEmpty) _image.MatrixRect = _imageWidget.Frame;
					_imageWidget.IsFrameVisible = false;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			_image.Invalidate();
		}

		private void CreateFrame(Point[] plate)
		{
			var rect = (plate == null || plate.Length == 0) ? default : plate.ToBoundedRectangle();
			if(!rect.IsEmpty)
			{
				rect.Inflate(30, 20);
				rect.Intersect(new Rectangle(Point.Empty, ImageSize));
				_imageWidget.Frame = rect;
			}
			else
			{
				_imageWidget.Frame = default;
			}
		}
	}
}
