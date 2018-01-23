using System;
using System.Drawing;
using System.Drawing.Imaging;

using Mallenom.Widgets;

namespace Recar2.MetaEditor.UI
{
	public sealed class ButtonRenderer : IWidgetRenderer
	{
		private static readonly ImageAttributes Attr = Colorize(Color.White);

		private static ImageAttributes Colorize(Color color)
		{
			var imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(new ColorMatrix(new[]
			{
						new float[] {0, 0, 0, 0, 0},
						new float[] {0, 0, 0, 0, 0},
						new float[] {0, 0, 0, 0, 0},
						new float[] {0, 0, 0, 1, 0},
						new float[] {color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, 0, 1}}));
			return imageAttributes;
		}

		public RendererInvalidateFlags InvalidateFlags => RendererInvalidateFlags.IsMouseOver | RendererInvalidateFlags.IsPressed;

		private static Color GetBackColor(Widget widget)
		{
			if(widget.IsPressed)
			{
				return Color.FromArgb(0, 122, 204);
			}
			if(widget.IsMouseOver)
			{
				return Color.FromArgb(62, 62, 64);
			}
			return Color.FromArgb(125, 45, 45, 48);
		}

		public void Render(Widget widget, Graphics graphics, Point location, Rectangle clipRectangle)
		{
			var buttonWidget = (ButtonWidget)widget;

			using(var brush = new SolidBrush(GetBackColor(widget)))
			{
				graphics.FillRectangle(brush, new Rectangle(location, widget.Size));
			}

			var imageMask = buttonWidget.ImageMask;
			if(imageMask != null)
			{
				var imageOffset = (widget.Height - imageMask.Height) / 2;
				graphics.DrawImage(imageMask,
					new Rectangle(new Point(location.X + imageOffset, location.Y + imageOffset), imageMask.Size),
					0, 0, imageMask.Width, imageMask.Height, GraphicsUnit.Pixel, Attr);
			}
		}
	}
}
