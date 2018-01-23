using System;
using System.Drawing;
using System.Linq;

using Mallenom;
using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	class LabelRegionController : IDisposable
	{
		#region Data

		private readonly Widget _rootWidget;

		private bool _isUpdated;

		#endregion

		#region Create & destroy

		public LabelRegionController(Widget rootWidget)
		{
			Verify.Argument.IsNotNull(rootWidget, nameof(rootWidget));

			_rootWidget = rootWidget;
			_rootWidget.WidgetAdded += RootWidgetOnPointWidgetAdded;
			_rootWidget.WidgetRemoved += RootWidgetOnPointWidgetRemoved;
		}

		public void Dispose()
		{
			_rootWidget.WidgetAdded -= RootWidgetOnPointWidgetAdded;
			_rootWidget.WidgetRemoved -= RootWidgetOnPointWidgetRemoved;
		}

		#endregion

		#region Private methods

		private void RootWidgetOnPointWidgetAdded(object sender, WidgetEventArgs args)
		{
			if(args.Widget is VertexWidget widget)
			{
				var labelWidget = new LabelVertexWidget(widget)
				{
					Offset = new Point(10, 10),
				};
				labelWidget.LocationChanged += LabelWidgetOnLocationChanged;
				_rootWidget.Children.Add(labelWidget);
			}
		}

		private void RootWidgetOnPointWidgetRemoved(object sender, WidgetEventArgs args)
		{
			if(args.Widget is VertexWidget widget)
			{
				var labelWidget = _rootWidget.Children.OfType<LabelVertexWidget>().FirstOrDefault(w => w.VertexWidget == widget);
				if(labelWidget != null)
				{
					labelWidget.LocationChanged -= LabelWidgetOnLocationChanged;
					_rootWidget.Children.Remove(labelWidget);
					labelWidget.Dispose();
				}
			}
		}

		private void LabelWidgetOnLocationChanged(object sender, EventArgs args)
		{
			if(_isUpdated) return;

			_isUpdated = true;

			var widget = (LabelVertexWidget)sender;

			var prev = widget.VertexWidget.GetPrev();
			var next = widget.VertexWidget.GetNext();
			var v1 = new Point((int)(prev.Position.X - widget.Position.X), (int)(prev.Position.Y - widget.Position.Y));
			var v2 = new Point((int)(next.Position.X - widget.Position.X), (int)(next.Position.Y - widget.Position.Y));
			var bisector = GetBisector(v1, v2);
			widget.Offset = new Point((int)(-ArrowLenght * bisector.X), (int)(-ArrowLenght * bisector.Y));
			if(!_rootWidget.Bounds.Contains(new Rectangle(widget.Location, widget.Size)))
			{
				widget.Offset = new Point((int)(ArrowLenght * bisector.X), (int)(ArrowLenght * bisector.Y));
			}
			if(!_rootWidget.Bounds.Contains(new Rectangle(widget.Location, widget.Size)))
			{
				widget.Offset = new Point((int)(ArrowLenght * bisector.Y), (int)(ArrowLenght * bisector.X));
			}
			if(!_rootWidget.Bounds.Contains(new Rectangle(widget.Location, widget.Size)))
			{
				widget.Offset = new Point((int)(-ArrowLenght * bisector.Y), (int)(-ArrowLenght * bisector.X));
			}

			widget.Text = GetLabelText(widget.VertexWidget);

			_isUpdated = false;
		}

		private const int ArrowLenght = 5;

		protected virtual string GetLabelText(VertexWidget widget) => $"{widget.Position.X}, {widget.Position.Y}";

		/// <summary>Возвращает биссектрису между двумя отрезками, заданными векторами.</summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		private static PointF GetBisector(Point v1, Point v2)
		{
			var ort1 = GetOrt(v1);
			var ort2 = GetOrt(v2);
			var sum = new PointF(ort1.X + ort2.X, ort1.Y + ort2.Y);
			if(sum.IsEmpty)
			{
				return ort1.X == 0.0f ? new PointF(1.0f, 0.0f) : new PointF(0.0f, 1.0f);
			}
			return GetOrt(sum);
		}

		private static PointF GetOrt(PointF vector)
		{
			var lenght = GetVectorLenght(vector);
			return lenght == 0.0 ? new PointF(0.0f, 0.0f) : new PointF((float)(vector.X / lenght), (float)(vector.Y / lenght));
		}

		private static double GetVectorLenght(PointF vector)
		{
			return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
		}

		#endregion
	}
}
