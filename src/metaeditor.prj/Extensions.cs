using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Mallenom;

namespace Recar2.MetaEditor
{
	static class Extensions
	{
		public static bool IsImage(this string path)
		{
			switch(Path.GetExtension(path)?.ToLower())
			{
				case ".jpg":
				case ".jpeg":
				case ".png":
				case ".bmp": return true;
				default: return false;
			}
		}

		public static Rectangle ToBoundedRectangle(this Point[] points)
		{
			Verify.Argument.IsNotNull(points, nameof(points));
			Verify.Argument.IsFalse(points.Length == 0, nameof(points), "Points array is empty.");

			var left = points[0].X;
			var top = points[0].Y;
			var right = points[0].X;
			var bottom = points[0].Y;
			for(var i = 1; i < points.Length; ++i)
			{
				var point = points[i];
				if(point.X < left) left = point.X;
				if(point.Y < top) top = point.Y;
				if(point.X > right) right = point.X;
				if(point.Y > bottom) bottom = point.Y;
			}
			return Rectangle.FromLTRB(left, top, right, bottom);
		}

		public static Point[] ToPoints(this Rectangle rect)
		{
			return rect.IsEmpty ? new Point[0] : new[] { rect.Location, new Point( rect.Right, rect.Y), new Point(rect.Right, rect.Bottom), new Point(rect.X, rect.Bottom) };
		}

		public static Point[] ToPoints(this Mallenom.Primitives.Point[] points)
		{
			return Array.ConvertAll(points, p => new Point(p.X, p.Y));
		}
	}
}
