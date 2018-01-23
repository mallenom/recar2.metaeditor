using System;

using Mallenom;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Сохраняет относительные пропорции региона при изменении размера области.</summary>
	class MaintainRegionWidgetStateBehavior : WidgetBehavior<RegionWidget>
	{
		private bool _isRestoring;

		public MaintainRegionWidgetStateBehavior()
		{
		}

		protected override void Attach(RegionWidget widget)
		{
			Assert.IsNotNull(widget);

			widget.RegionBoundsChanging += OnWidgetBoundsChanging;
			widget.RegionBoundsChanged  += OnWidgetBoundsChanged;
			widget.ImageBoundsChanging  += OnWidgetBoundsChanging;
			widget.ImageBoundsChanged   += OnWidgetBoundsChanged;
			widget.ShapeChanged         += OnWidgetShapeChanged;

			base.Attach(widget);
		}

		protected override void Detach(RegionWidget widget)
		{
			Assert.IsNotNull(widget);

			widget.RegionBoundsChanging -= OnWidgetBoundsChanging;
			widget.RegionBoundsChanged  -= OnWidgetBoundsChanged;
			widget.ImageBoundsChanging  -= OnWidgetBoundsChanging;
			widget.ImageBoundsChanged   -= OnWidgetBoundsChanged;
			widget.ShapeChanged         -= OnWidgetShapeChanged;

			base.Detach(widget);
		}

		[CanBeNull]
		public IRegionWidgetState State { get; set; }

		public bool TryRestoreState()
		{
			if(State != null)
			{
				_isRestoring = true;
				var result = State.TryRestore();
				_isRestoring = false;
				return result;
			}
			return false;
		}

		public void InvalidateState()
		{
			State = null;
		}

		private void OnWidgetShapeChanged(object sender, EventArgs e)
		{
			if(!_isRestoring)
			{
				InvalidateState();
			}
		}

		private void OnWidgetBoundsChanging(object sender, EventArgs e)
		{
			if(State == null)
			{
				State = RegionWidgetState.TryCapture(Widget);
			}
		}

		private void OnWidgetBoundsChanged(object sender, EventArgs e)
		{
			TryRestoreState();
		}
	}
}
