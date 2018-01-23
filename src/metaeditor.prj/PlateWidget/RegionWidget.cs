using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Флаги виджета.</summary>
	[Flags]
	public enum RegionWidgetFlags
	{
		None = 0,
		OutsideShading = 1 << 0,
	}

	/// <summary>Виджет для отображения рамки зоны обработки.</summary>
	public sealed class RegionWidget : Widget, IDisposable
	{
		private sealed class TextSizeCache
		{
			private string _text;
			private WeakReference<Font> _font;
			private Size _proposedSize;
			private Size _size;

			public Size Measure(string text, Font font, Size proposedSize)
			{
				if(_text != text || _font == null || !_font.TryGetTarget(out var cachedFont) || cachedFont != font || _proposedSize != proposedSize)
				{
					_size = TextRenderer.MeasureText(text, font, proposedSize);
					_text = text;
					_font = new WeakReference<Font>(font);
					_proposedSize = proposedSize;
				}
				return _size;
			}
		}

		private static readonly object RegionBoundsChangingEvent = new object();

		public event EventHandler RegionBoundsChanging
		{
			add => Events.AddHandler(RegionBoundsChangingEvent, value);
			remove => Events.RemoveHandler(RegionBoundsChangingEvent, value);
		}

		private void OnRegionBoundsChanging(EventArgs e) => Events.Raise(this, RegionBoundsChangingEvent, e);

		private static readonly object RegionBoundsChangedEvent = new object();

		public event EventHandler RegionBoundsChanged
		{
			add => Events.AddHandler(RegionBoundsChangedEvent, value);
			remove => Events.RemoveHandler(RegionBoundsChangedEvent, value);
		}

		private void OnRegionBoundsChanged(EventArgs e) => Events.Raise(this, RegionBoundsChangedEvent, e);

		private static readonly object ImageBoundsChangingEvent = new object();

		public event EventHandler ImageBoundsChanging
		{
			add => Events.AddHandler(ImageBoundsChangingEvent, value);
			remove => Events.RemoveHandler(ImageBoundsChangingEvent, value);
		}

		private void OnImageBoundsChanging(EventArgs e) => Events.Raise(this, ImageBoundsChangingEvent, e);

		private static readonly object ImageBoundsChangedEvent = new object();

		public event EventHandler ImageBoundsChanged
		{
			add => Events.AddHandler(ImageBoundsChangedEvent, value);
			remove => Events.RemoveHandler(ImageBoundsChangedEvent, value);
		}

		private void OnImageBoundsChanged(EventArgs e) => Events.Raise(this, ImageBoundsChangedEvent, e);

		private static readonly object ShapeChangedEvent = new object();

		public event EventHandler ShapeChanged
		{
			add => Events.AddHandler(ShapeChangedEvent, value);
			remove => Events.RemoveHandler(ShapeChangedEvent, value);
		}

		private void OnShapeChanged(EventArgs e) => Events.Raise(this, ShapeChangedEvent, e);

		private readonly RegionWidgetFlags _flags;

		private readonly Pen _pen;

		private readonly Font _textFont;

		private bool _isSelected;

		private readonly TextSizeCache _cachedTextSize = new TextSizeCache();

		private Rectangle _regionBounds;

		private Rectangle _imageBounds;

		public Color Color { get; set; }

		public string Text { get; set; }

		public Rectangle RegionBounds
		{
			get => _regionBounds;
			set
			{
				if(_regionBounds != value)
				{
					OnRegionBoundsChanging(EventArgs.Empty);
					_regionBounds = value;
					OnRegionBoundsChanged(EventArgs.Empty);
				}
			}
		}

		public Rectangle ImageBounds
		{
			get => _imageBounds;
			set
			{
				if(_imageBounds != value)
				{
					OnImageBoundsChanging(EventArgs.Empty);
					_imageBounds = value;
					OnImageBoundsChanged(EventArgs.Empty);
				}
			}
		}

		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				if(value == _isSelected) return;
				_isSelected = value;
				foreach(var widget in Children)
				{
					if(widget is VertexWidget vw)
					{
						vw.IsSelected = _isSelected;
					}
				}
			}
		}

		public int PointSize { get; set; } = 12;

		/// <summary>Возвращает коллекцию подключенных поведений виджета.</summary>
		[NotNull]
		public WidgetBehaviorsCollection Behaviors { get; }

		public RegionWidget(RegionWidgetFlags flags = RegionWidgetFlags.None)
			: base(WidgetFlags.DisableChildrenScrollOptimization | WidgetFlags.DisableParentScrollOptimization)
		{
			_flags = flags;
			Behaviors = new WidgetBehaviorsCollection(this);
			HandlesMouseWheelEvents = false;
			Color = Color.DarkOrange;
			_pen = new Pen(Color, 2);
			_textFont = new Font(SystemFonts.MessageBoxFont.Name, 8.0f);
		}

		protected override void OnWidgetAdded(WidgetEventArgs e)
		{
			if(e.Widget is VertexWidget vw)
			{
				vw.LocationChanged += OnVertexWidgetMoved;
				OnShapeChanged(default);
			}
			base.OnWidgetAdded(e);
		}

		protected override void OnWidgetRemoved(WidgetEventArgs e)
		{
			if(e.Widget is VertexWidget vw)
			{
				vw.LocationChanged -= OnVertexWidgetMoved;
				OnShapeChanged(default);
			}
			base.OnWidgetAdded(e);
		}

		private void OnVertexWidgetMoved(object sender, EventArgs e)
		{
			OnShapeChanged(e);
		}

		public void Dispose()
		{
			_pen?.Dispose();
			_textFont?.Dispose();
		}

		public Rectangle GetInvalidRect()
		{
			var rect = GetRegionRect();
			if(rect.IsEmpty) return Rectangle.Empty;
			var extraRect = Rectangle.FromLTRB(rect.Left - PointSize / 2, rect.Top - _textFont.Height, rect.Right + PointSize / 2, rect.Bottom + PointSize / 2);
			var textRect = GetTextRect();
			return Rectangle.Union(extraRect, textRect);
		}

		public override bool HitTest(int x, int y)
		{
			var rect = GetRegionRect();
			if(rect.IsEmpty) return false;
			rect.Inflate(2, 2);
			if(rect.Contains(x, y))
			{
				return true;
			}
			rect.Inflate(PointSize / 2, PointSize / 2);
			if(rect.Contains(x, y))
			{
				return VertexHitTest(x, y);
			}
			return false;
		}

		public bool VertexHitTest(int x, int y)
		{
			foreach(var widget in Children)
			{
				if(widget is VertexWidget pw)
				{
					if(pw.HitTest(x - pw.Location.X, y - pw.Location.Y)) return true;
				}
			}
			return false;
		}

		protected override void OnRender(Graphics graphics, Point location, Rectangle clipRectangle)
		{
			var smoothingMode = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;

			_pen.Color = Color;
			var first = default(VertexWidget);
			var prev  = default(VertexWidget);
			foreach(var widget in Children)
			{
				if(widget is VertexWidget vertex)
				{
					if(first == null)
					{
						first = prev = vertex;
					}
					else
					{
						DrawEdge(graphics, prev, vertex, clipRectangle);
						prev = vertex;
					}
				}
			}

			if(first != null) DrawEdge(graphics, prev, first, clipRectangle);

			if((_flags & RegionWidgetFlags.OutsideShading) == RegionWidgetFlags.OutsideShading)
			{
				using(var gp = new GraphicsPath())
				{
					gp.AddRectangle(new Rectangle(location, Size));
					Point? previousPoint = null;
					foreach(var widget in Children)
					{
						if(widget is VertexWidget pw)
						{
							if(previousPoint.HasValue)
							{
								gp.AddLine(previousPoint.Value, pw.Position);
							}
							previousPoint = pw.Position;
						}
					}
					using(var brush = new SolidBrush(Color.FromArgb(60, 0, 0, 0)))
					{
						graphics.FillPath(brush, gp);
					}
				}
			}

			if(first != null && !string.IsNullOrWhiteSpace(Text))
			{
				var textBounds = GetTextRect();
				var textClip = Rectangle.Intersect(clipRectangle, textBounds);
				if(textClip.Width > 0 && textClip.Height > 0)
				{
					TextRenderer.DrawText(graphics, Text, _textFont, textBounds, Color);
				}
			}
			graphics.SmoothingMode = smoothingMode;
		}

		private Rectangle GetTextRect()
		{
			if(string.IsNullOrWhiteSpace(Text)) return Rectangle.Empty;

			var rect = GetRegionRect();
			var measuredSize = _cachedTextSize.Measure(Text, _textFont, rect.Size);
			var widthExtra = (measuredSize.Width > rect.Width) ? (measuredSize.Width - rect.Width) / 2 : 0;
			return Rectangle.FromLTRB(rect.Left - widthExtra, (int)(rect.Top - _textFont.GetHeight() - 4), rect.Right + widthExtra, rect.Top);
		}

		private void DrawEdge(Graphics g, VertexWidget v1, VertexWidget v2, Rectangle clipRectangle)
		{
			var p1 = v1.Position;
			var p2 = v2.Position;
			if(IsEnabled && (v1.IsVisible || v2.IsVisible))
			{
				var dx = p2.X - p1.X;
				var dy = p2.Y - p1.Y;
				var length     = Math.Sqrt(dx * dx + dy * dy);
				var proportion = (PointSize - 2) / 2.0 / length;
				if(v1.IsVisible)
				{
					var cutX = (int)Math.Round(proportion * dx);
					p1.X += cutX;
					p2.X -= cutX;
				}
				if(v2.IsVisible)
				{
					var cutY = (int)Math.Round(proportion * dy);
					p1.Y += cutY;
					p2.Y -= cutY;
				}
			}
			var rc = Rectangle.FromLTRB(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y));
			rc.Intersect(clipRectangle);
			if(!rc.IsEmpty)
			{
				g.DrawLine(_pen, p1, p2);
			}
		}

		private Rectangle GetRegionRect()
		{
			var left = Bounds.Right;
			var top = Bounds.Bottom;
			var right = Bounds.X;
			var bottom = Bounds.Y;
			var isPresentPoint = false;
			foreach(var widget in Children)
			{
				if(widget is VertexWidget pw)
				{
					isPresentPoint = true;
					var pos = pw.Position;
					if(pos.X < left) left = pos.X;
					if(pos.Y < top) top = pos.Y;
					if(pos.X > right) right = pos.X;
					if(pos.Y > bottom) bottom = pos.Y;
				}
			}
			return isPresentPoint ? Rectangle.FromLTRB(left, top, right, bottom) : default;
		}
	}
}
