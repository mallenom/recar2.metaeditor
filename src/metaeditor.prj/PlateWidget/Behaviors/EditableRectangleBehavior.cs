using System;
using System.Drawing;

using Mallenom;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Поведение редактируемого прямоугольника.</summary>
	public class EditableRectangleBehavior : VertexEditorBehavior
	{
		/// <summary>Возвращает и устанавливает минимальный размер прямоугольника.</summary>
		/// <value>Минимальный размер прямоугольника.</value>
		public Size MinimumSize { get; set; }

		private static bool IsHorizontal(Point p1, VertexWidget w2)
		{
			return Math.Abs(p1.X - w2.Position.X) > Math.Abs(p1.Y - w2.Position.Y);
		}

		protected override void OnVertexWidgetMoved(VertexWidget widget, Point offset)
		{
			Assert.IsNotNull(widget);

			var regionBounds = GetRegionBounds();
			if(regionBounds.IsEmpty)
			{
				return;
			}
			var wloc = widget.Position;
			wloc.Offset(offset);
			wloc = ClampVertexPosition(wloc, regionBounds);

			var invalidRect = Widget.GetInvalidRect();
			var prevWidget = widget.GetPrev();
			var isHorizontal = IsHorizontal(widget.Position, prevWidget);
			{
				if(isHorizontal)
				{
					if(MinimumSize.Width != 0)
					{
						var diffX = wloc.X - prevWidget.Position.X;
						if(Math.Abs(diffX) < MinimumSize.Width)
						{
							wloc = new Point(prevWidget.Position.X + (diffX < 0 ? -1 : 1) * MinimumSize.Width, wloc.Y);
						}
					}
				}
				else
				{
					if(MinimumSize.Height != 0)
					{
						var diffY = wloc.Y - prevWidget.Position.Y;
						if(Math.Abs(diffY) < MinimumSize.Height)
						{
							wloc = new Point(wloc.X, prevWidget.Position.Y + (diffY < 0 ? -1 : 1) * MinimumSize.Height);
						}
					}
				}
			}

			var nextWidget = widget.GetNext();
			{
				if(!isHorizontal)
				{
					if(MinimumSize.Width != 0)
					{
						var diffX = wloc.X - nextWidget.Position.X;
						if(Math.Abs(diffX) < MinimumSize.Width)
						{
							wloc = new Point(nextWidget.Position.X + (diffX < 0 ? -1 : 1) * MinimumSize.Width, wloc.Y);
						}
					}
				}
				else
				{
					if(MinimumSize.Height != 0)
					{
						var diffY = wloc.Y - nextWidget.Position.Y;
						if(Math.Abs(diffY) < MinimumSize.Height)
						{
							wloc = new Point(wloc.X, nextWidget.Position.Y + (diffY < 0 ? -1 : 1) * MinimumSize.Height);
						}
					}
				}
			}

			if(prevWidget != null)
			{
				prevWidget.Position = isHorizontal
					? new Point(prevWidget.Position.X, wloc.Y)
					: new Point(wloc.X, prevWidget.Position.Y);
			}

			if(nextWidget != null)
			{
				nextWidget.Position = !isHorizontal
					? new Point(nextWidget.Position.X, wloc.Y)
					: new Point(wloc.X, nextWidget.Position.Y);
			}

			widget.Position = wloc;

			invalidRect = Rectangle.Union(invalidRect, Widget.GetInvalidRect());
			Widget.Invalidate(invalidRect);
			OnVertexMoved(EventArgs.Empty);
		}
	}
}
