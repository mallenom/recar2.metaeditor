using System;
using System.Drawing;
using System.Linq;

using Mallenom;
using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Базовое поведение для обработки изменения позиции вершин фигуры.</summary>
	public abstract class VertexEditorBehavior : WidgetBehavior<RegionWidget>
	{
		public event EventHandler VertexMoved;

		protected virtual void OnVertexMoved(EventArgs e)
		{
			VertexMoved?.Invoke(this, e);
		}

		protected override void Attach(RegionWidget widget)
		{
			Assert.IsNotNull(widget);

			widget.WidgetAdded += OnChildWidgetAdded;
			widget.WidgetRemoved += OnChildWidgetRemoved;
			if(widget.HasChildren)
			{
				foreach(var vertex in widget.Children.OfType<VertexWidget>())
				{
					vertex.Moved += OnVertexWidgetMoved;
				}
			}

			base.Attach(widget);
		}

		protected override void Detach(RegionWidget widget)
		{
			Assert.IsNotNull(widget);

			widget.WidgetAdded -= OnChildWidgetAdded;
			widget.WidgetRemoved -= OnChildWidgetRemoved;
			if(widget.HasChildren)
			{
				foreach(var vertex in widget.Children.OfType<VertexWidget>())
				{
					vertex.Moved -= OnVertexWidgetMoved;
				}
			}

			base.Detach(widget);
		}

		private void OnChildWidgetAdded(object sender, WidgetEventArgs e)
		{
			if(e.Widget is VertexWidget vw)
			{
				vw.Moved += OnVertexWidgetMoved;
			}
		}

		private void OnChildWidgetRemoved(object sender, WidgetEventArgs e)
		{
			if(e.Widget is VertexWidget vw)
			{
				vw.Moved -= OnVertexWidgetMoved;
			}
		}

		protected Rectangle GetRegionBounds()
		{
			return Widget?.RegionBounds ?? Rectangle.Empty;
		}

		private void OnVertexWidgetMoved(object sender, VertexMovedEventArgs e)
		{
			OnVertexWidgetMoved((VertexWidget)sender, e.Offset);
		}

		protected static Point ClampVertexPosition(Point position, Rectangle regionBounds)
		{
			if(position.X < regionBounds.X) position.X = regionBounds.X;
			if(position.Y < regionBounds.Y) position.Y = regionBounds.Y;
			if(position.X > regionBounds.Right) position.X = regionBounds.Right;
			if(position.Y > regionBounds.Bottom) position.Y = regionBounds.Bottom;
			return position;
		}

		protected abstract void OnVertexWidgetMoved(VertexWidget widget, Point offset);
	}
}
