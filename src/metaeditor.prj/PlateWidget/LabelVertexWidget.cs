using System;
using System.Drawing;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	sealed class LabelVertexWidget : Widget, IDisposable
	{
		private readonly Brush _backBrush = new SolidBrush(Color.FromArgb(100, Color.Black));

		private readonly Brush _textBrush = new SolidBrush(Color.FromArgb(100, Color.White));

		private readonly Pen _textPen = new Pen(Color.FromArgb(100, Color.White), 1.5f);

		private readonly Color _textColor = Color.FromArgb(200, Color.White);

		private readonly Font _textFont;

		private string _text;

		private Point _offset;

		/// <summary>Возвращает виджет к которому прикреплен данный виджет.</summary>
		public VertexWidget VertexWidget { get; }

		public string Text
		{
			get => _text;
			set
			{
				if(value != _text)
				{
					_text = value;
					var size = TextRenderer.MeasureText(_text, _textFont);
					Size = new Size(size.Width + 3, size.Height + 3);
					Invalidate();
				}
			}
		}

		public PointF Position => VertexWidget.Position;

		public Point Offset
		{
			get => _offset;
			set
			{
				if(value != _offset)
				{
					_offset = value;
					UpdateLocation();
				}
			}
		}

		public LabelVertexWidget(VertexWidget widget, WidgetFlags flags = WidgetFlags.None) : base(flags)
		{
			Verify.Argument.IsNotNull(widget, nameof(widget));

			HandlesMouseEvents = false;
			_textFont = new Font(SystemFonts.MessageBoxFont.Name, 7.0f);

			VertexWidget = widget;
			VertexWidget.LocationChanged += WidgetOnLocationChanged;
		
			UpdateLocation();
		}

		public void Dispose()
		{
			_backBrush?.Dispose();
			_textBrush?.Dispose();
			_textFont?.Dispose();
		}

		protected override void OnRender(Graphics graphics, Point location, Rectangle clipRectangle)
		{
			var textRect = new Rectangle(location.X, location.Y, Size.Width - 1, Size.Height - 1);
			graphics.FillRectangle(_backBrush, textRect);
			graphics.DrawRectangle(_textPen, textRect);
			TextRenderer.DrawText(graphics, Text, _textFont, textRect, _textColor);
		}

		private void WidgetOnLocationChanged(object sender, EventArgs args)
		{
			UpdateLocation();
		}

		private void UpdateLocation()
		{
			var location = new Point(VertexWidget.Position.X, VertexWidget.Position.Y);
			location.Offset(_offset);
			if(_offset.X < 0)
			{
				location.Offset(-Size.Width, 0);
			}
			if(_offset.Y < 0)
			{
				location.Offset(0, -Size.Height);
			}
			Location = location;
		}
	}
}
