using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Mallenom;

namespace Recar2.Algorithms.Widgets
{
	public class RegionWidgetState : IRegionWidgetState
	{
		public static RegionWidgetState TryCapture(RegionWidget widget)
		{
			return widget != null && !widget.RegionBounds.IsEmpty ? new RegionWidgetState(widget) : null;
		}

		public class VertexState
		{
			internal VertexState(VertexWidget widget)
			{
				Verify.Argument.IsNotNull(widget, nameof(widget));

				Widget   = widget;
				Position = widget.Position;
			}

			public VertexWidget Widget { get; }

			public Point Position { get; }
		}

		private RegionWidgetState(RegionWidget widget)
		{
			Verify.Argument.IsNotNull(widget, nameof(widget));

			Widget = widget;
			RegionBounds = widget.RegionBounds;
			var vertices = new List<VertexState>();
			foreach(var vertex in widget.Children.OfType<VertexWidget>())
			{
				vertices.Add(new VertexState(vertex));
			}
			Vertices = vertices;
		}

		public RegionWidget Widget { get; }

		public Rectangle RegionBounds { get; }

		public IReadOnlyList<VertexState> Vertices { get; }

		public bool TryRestore()
		{
			var regionBounds = Widget.RegionBounds;
			if(regionBounds.Width <= 0 || regionBounds.Height <= 0)
			{
				return false;
			}
			foreach(var vertex in Vertices)
			{
				vertex.Widget.Position = ConvertPosition(vertex.Position, regionBounds);
			}
			return true;
		}

		public Size ConvertSize(Size size, Size targetSize)
		{
			return new Size(size.Width * targetSize.Width / RegionBounds.Width, size.Height * targetSize.Height / RegionBounds.Height);
		}

		public SizeF ConvertSize(SizeF size, SizeF targetSize)
		{
			return new SizeF(size.Width * targetSize.Width / RegionBounds.Width, size.Height * targetSize.Height / RegionBounds.Height);
		}

		public Point ConvertPosition(Point position, Rectangle targetBounds)
		{
			var x = position.X - RegionBounds.X;
			var y = position.Y - RegionBounds.Y;
			x = targetBounds.X + x * targetBounds.Width  / RegionBounds.Width;
			y = targetBounds.Y + y * targetBounds.Height / RegionBounds.Height;
			return new Point(x, y);
		}

		public PointF ConvertPosition(PointF position, RectangleF targetBounds)
		{
			var x = position.X - RegionBounds.X;
			var y = position.Y - RegionBounds.Y;
			x = targetBounds.X + x * targetBounds.Width  / RegionBounds.Width;
			y = targetBounds.Y + y * targetBounds.Height / RegionBounds.Height;
			return new PointF(x, y);
		}
	}
}
