using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	public class RegionController : IDisposable
	{
		#region Data

		private bool _showVertices = true;

		#endregion

		#region Properties

		public bool ShowVertices
		{
			get => _showVertices;
			set
			{
				if(value == _showVertices) return;
				_showVertices = value;
				foreach(var widget in Widget.Children.OfType<LabelVertexWidget>())
				{
					widget.IsVisible = _showVertices;
				}
			}
		}

		public bool IsVisible
		{
			get => Widget.IsVisible;
			set => Widget.IsVisible = value;
		}

		public Color Color
		{
			get => Widget.Color;
			set
			{
				Widget.Color = value;
				Widget.Invalidate();
			}
		}

		public string Text
		{
			get => Widget.Text;
			set => Widget.Text = value;
		}

		/// <summary>Возвращает и устанавливает отображаемый регион, заданный набором вершин.</summary>
		public Point[] Vertices
		{
			get => Widget.Children.OfType<VertexWidget>().Select(w => w.Position).ToArray();
			set => RelocateWidgets(value);
		}

		public RegionWidget Widget { get; }

		#endregion

		#region Create & destroy

		public RegionController(RegionWidgetFlags flags = RegionWidgetFlags.None)
		{
			Widget = new RegionWidget(flags) 
			{
				Layout = new WidgetLayout() { Dock = DockStyle.Fill }
			};
		}

		public void Dispose()
		{
			Widget.Dispose();
		}

		#endregion

		#region Private methods

		private void RelocateWidgets(Point[] points)
		{
			var invalidRect = Widget.GetInvalidRect();
			if(points == null || points.Length != Widget.Children.OfType<VertexWidget>().Count())
			{
				RebuildWidgets(points);
			}
			if(points != null)
			{
				int index = 0;
				foreach(var widget in Widget.Children.OfType<VertexWidget>())
				{
					widget.Position = points[index];
					++index;
				}
			}
			invalidRect = Rectangle.Union(invalidRect, Widget.GetInvalidRect());
			Widget.Invalidate(invalidRect);
		}

		private void RebuildWidgets(Point[] points)
		{
			DestroyWidgets();
			if(points != null && points.Length != 0)
			{
				foreach(var point in points)
				{
					var vertexWidget = new VertexWidget(Widget.PointSize)
					{
						Color     = Color,
						IsVisible = ShowVertices,
					};
					Widget.Children.Add(vertexWidget);
				}
			}
		}

		private void DestroyWidgets()
		{
			foreach(var widget in Widget.Children.OfType<VertexWidget>().ToArray())
			{
				Widget.Children.Remove(widget);
				widget.Dispose();
			}
		}

		#endregion
	}
}
