using System;
using System.Drawing;

using Mallenom;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Поведение редактируемого многоугольника.</summary>
	public class EditablePolygonBehavior : VertexEditorBehavior
	{
		protected override void OnVertexWidgetMoved(VertexWidget widget, Point offset)
		{
			Assert.IsNotNull(widget);

			var wloc = widget.Position;
			wloc.Offset(offset);

			var regionBounds = GetRegionBounds();
			if(!regionBounds.IsEmpty)
			{
				wloc = ClampVertexPosition(wloc, regionBounds);
			}
			widget.Position = wloc;
			Widget.Invalidate();
			OnVertexMoved(EventArgs.Empty);
		}
	}
}
