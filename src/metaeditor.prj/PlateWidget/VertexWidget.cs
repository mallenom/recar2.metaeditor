using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	public sealed class VertexMovedEventArgs : EventArgs
	{
		public Point Offset { get; }

		public VertexMovedEventArgs(Point offset)
		{
			Offset = offset;
		}
	}

	[DebuggerDisplay("{" + nameof(Position) + "}")]
	public sealed class VertexWidget : Widget, IDisposable
	{
		public event EventHandler<VertexMovedEventArgs> Moved;

		private readonly Pen _pen;

		private readonly SolidBrush _brush;

		private bool _isSelected;

		private Point _mouseDownLocation;

		public Color Color
		{
			get => _pen.Color;
			set
			{
				_pen.Color = value;
				_brush.Color = value;
			}
		}

		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				if(_isSelected != value)
				{
					_isSelected = value;
					Invalidate();
				}
			}
		}

		/// <summary>Устанавливает и возвращает координаты центра точки.</summary>
		public Point Position
		{
			get
			{
				var loc = Location;
				loc.Offset(Size.Width / 2, Size.Height / 2);
				return loc;
			}
			set
			{
				var loc = value;
				loc.Offset(-Size.Width / 2, -Size.Height / 2);
				Location = loc;
			}
		}

		public VertexWidget(int size) : base(WidgetFlags.DisableParentScrollOptimization)
		{
			_pen = new Pen(Color.Coral, 2);
			_brush = new SolidBrush(Color);
			Size = new Size(size, size);
		}

		public void Dispose()
		{
			_pen?.Dispose();
			_brush.Dispose();
		}

		public override bool HitTest(int x, int y)
		{
			var dx = x - Size.Width / 2;
			var dy = y - Size.Height / 2;
			return base.HitTest(x, y) && dx * dx + dy * dy < Size.Width * Size.Height / 4;
		}

		[CanBeNull]
		public VertexWidget GetPrev()
		{
			var prev = default(VertexWidget);
			var found = false;
			var last = default(VertexWidget);
			foreach(var widget in Parent.Children)
			{
				if(widget is VertexWidget vw)
				{
					if(vw != this)
					{
						if(!found) prev = vw;
					}
					else
					{
						found = true;
						if(prev != null) break;
					}
					last = vw;
				}
			}
			return prev ?? last;
		}

		[CanBeNull]
		public VertexWidget GetNext()
		{
			var next = default(VertexWidget);
			var found = false;
			var first = default(VertexWidget);
			foreach(var widget in Parent.Children)
			{
				if(widget is VertexWidget vw)
				{
					if(first == null) first = vw;
					if(vw == this)
					{
						found = true;
						continue;
					}
					if(found)
					{
						next = vw;
						break;
					}
				}
			}
			return next ?? first;
		}

		protected override void OnIsMouseOverChanged(EventArgs args)
		{
			base.OnIsMouseOverChanged(args);
			Invalidate();
		}

		protected override void OnIsMouseCapturedChanged(EventArgs e)
		{
			base.OnIsMouseCapturedChanged(e);
			Invalidate();
		}

		protected override void OnMouseDown(WidgetMouseEventArgs args)
		{
			_mouseDownLocation = args.Location;
			base.OnMouseDown(args);
		}

		protected override void OnMouseMove(WidgetMouseEventArgs args)
		{
			base.OnMouseMove(args);
			if(IsMouseCaptured && args.Buttons == MouseButtons.Left)
			{
				var offset = new Point(args.X - _mouseDownLocation.X, args.Y - _mouseDownLocation.Y);
				if(!offset.IsEmpty)
				{
					Moved?.Invoke(this, new VertexMovedEventArgs(offset));
				}
			}
		}

		protected override void OnRender(Graphics graphics, Point location, Rectangle clipRectangle)
		{
			if(!IsEnabled) return;

			var smoothingMode = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.HighQuality;

			var rect = new Rectangle(location, Size);
			rect.Inflate(-2, -2);
			if(IsSelected || IsMouseOver || IsMouseCaptured)
			{
				graphics.FillEllipse(_brush, rect);
			}
			graphics.DrawEllipse(_pen, rect);

			graphics.SmoothingMode = smoothingMode;
		}
	}
}
