using System;
using System.Drawing;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Поведение перемещаемого региона.</summary>
	public class MovableRegionBehavior : WidgetBehavior<RegionWidget>
	{
		private Point _lastLocation;

		/// <summary>Регион был перемещен.</summary>
		public event EventHandler RegionMoved;

		/// <summary>Вызывается при перемещении региона.</summary>
		/// <param name="e">Аргументы события.</param>
		private void OnRegionMoved(EventArgs e) => RegionMoved?.Invoke(this, e);

		/// <summary>Возвращает флаг перемещения виджета.</summary>
		/// <value><c>true</c>, если виджет перемещается, иначе - <c>false</c>.</value>
		public bool IsMoving { get; private set; }

		protected override void Attach(RegionWidget widget)
		{
			Assert.IsNotNull(widget);

			widget.MouseDown  += OnWidgetMouseDown;
			widget.MouseMove  += OnWidgetMouseMove;
			widget.MouseUp    += OnWidgetMouseUp;
			widget.MouseEnter += OnWidgetMouseEnter;
			widget.MouseLeave += OnWidgetMouseLeave;

			base.Attach(widget);
		}

		protected override void Detach(RegionWidget widget)
		{
			Assert.IsNotNull(widget);

			widget.MouseDown  -= OnWidgetMouseDown;
			widget.MouseMove  -= OnWidgetMouseMove;
			widget.MouseUp    -= OnWidgetMouseUp;
			widget.MouseEnter -= OnWidgetMouseEnter;
			widget.MouseLeave -= OnWidgetMouseLeave;
			IsMoving = false;

			base.Detach(widget);
		}

		private void OnWidgetMouseEnter(object sender, EventArgs e)
		{
			Widget.IsSelected = Widget.IsMouseDirectlyOver || IsMoving;
		}

		private void OnWidgetMouseLeave(object sender, EventArgs e)
		{
			Widget.IsSelected = Widget.IsMouseDirectlyOver || IsMoving;
		}

		private void OnWidgetMouseDown(object sender, WidgetMouseEventArgs e)
		{
			if(Widget.IsEnabled && e.Buttons == MouseButtons.Left)
			{
				_lastLocation = e.Location;
				IsMoving = true;
			}
		}

		private void OnWidgetMouseMove(object sender, WidgetMouseEventArgs e)
		{
			if(IsMoving)
			{
				var offsetX = e.Location.X - _lastLocation.X;
				var offsetY = e.Location.Y - _lastLocation.Y;
				if(offsetX != 0 || offsetY != 0)
				{
					var offset = Move(new Point(offsetX, offsetY));
					_lastLocation.Offset(offset);
					OnRegionMoved(RegionChangedEventArgs.ForLocationChanged);
				}
			}
		}

		private void OnWidgetMouseUp(object sender, WidgetMouseEventArgs e)
		{
			IsMoving = false;
		}

		private Point ClampOffset(Point offset)
		{
			var regionBounds = Widget.RegionBounds;
			var offsetX = offset.X;
			var offsetY = offset.Y;
			foreach(var w in Widget.Children)
			{
				if(w is VertexWidget vw)
				{
					if(vw.Position.X + offsetX < regionBounds.Left) offsetX = regionBounds.Left - vw.Position.X;
					else if(vw.Position.X + offsetX > regionBounds.Right) offsetX = regionBounds.Right - vw.Position.X;
					if(vw.Position.Y + offsetY < regionBounds.Top) offsetY = regionBounds.Top - vw.Position.Y;
					else if(vw.Position.Y + offsetY > regionBounds.Bottom) offsetY = regionBounds.Bottom - vw.Position.Y;
				}
			}
			return new Point(offsetX, offsetY);
		}

		private Point Move(Point offset)
		{
			var actualOffset = ClampOffset(offset);
			if(!actualOffset.IsEmpty)
			{
				var invalidRect = Widget.GetInvalidRect();
				foreach(var w in Widget.Children)
				{
					if(w is VertexWidget vw)
					{
						var wloc = vw.Position;
						wloc.Offset(actualOffset);
						vw.Position = wloc;
					}
				}
				invalidRect = Rectangle.Union(invalidRect, Widget.GetInvalidRect());
				Widget.Invalidate(invalidRect);
				OnRegionMoved(EventArgs.Empty);
			}
			return actualOffset;
		}
	}
}
