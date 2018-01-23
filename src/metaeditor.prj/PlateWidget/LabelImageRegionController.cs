using System;
using System.Drawing;

using Mallenom;
using Mallenom.Widgets;

using Image = Mallenom.Imaging.Image;

namespace Recar2.Algorithms.Widgets
{
	sealed class LabelImageRegionController : LabelRegionController
	{
		private readonly Image _image;

		public LabelImageRegionController(Widget rootWidget, Image image) 
			: base(rootWidget)
		{
			Verify.Argument.IsNotNull(image, nameof(image));

			_image = image;
		}

		protected override string GetLabelText(VertexWidget widget)
		{
			var p = GetRelationPos(widget.Position);
			return $"{p.X.ToPercent():0.0}, {p.Y.ToPercent():0.0}";
		}

		private PointF GetRelationPos(Point point)
		{
			var converter = _image.CoordConverter;
			var matrixPoint = converter.ConvertScreenToMatrix(point);
			var matrix = _image.Matrix;
			if(matrix == null || matrix.IsEmpty)
			{
				return PointF.Empty;
			}
			var x = matrixPoint.X / (float)matrix.Width;
			var y = matrixPoint.Y / (float)matrix.Height;
			return new PointF(x, y);
		}
	}
}
