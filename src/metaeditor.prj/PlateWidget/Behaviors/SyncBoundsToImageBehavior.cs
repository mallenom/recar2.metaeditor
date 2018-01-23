using System;

using Mallenom;
using Image = Mallenom.Imaging.Image;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Синхронизирует RegionBounds и ImageBounds виджета в соответствии с ViewRect/MatrixRect контрола Image.</summary>
	public class SyncBoundsToImageBehavior : HostedWidgetBehavior<RegionWidget, Image>
	{
		protected override void Attach(Image control)
		{
			Assert.IsNotNull(control);

			control.ViewRectChanged   += OnImageViewRectChanged;
			control.MatrixRectChanged += OnImageMatrixRectChanged;

			Widget.ImageBounds  = control.MatrixRect;
			Widget.RegionBounds = control.ViewRect;

			base.Attach(control);
		}

		protected override void Detach(Image control)
		{
			Assert.IsNotNull(control);

			control.ViewRectChanged   -= OnImageViewRectChanged;
			control.MatrixRectChanged -= OnImageMatrixRectChanged;

			base.Detach(control);
		}

		private void OnImageMatrixRectChanged(object sender, EventArgs e)
		{
			Widget.ImageBounds = Control.MatrixRect;
		}

		private void OnImageViewRectChanged(object sender, EventArgs e)
		{
			Widget.RegionBounds = Control.ViewRect;
		}
	}
}
