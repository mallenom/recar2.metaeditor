using System;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	public abstract class HostedWidgetBehavior<TWidget, TControl> : WidgetBehavior<TWidget>
		where TWidget : Widget
		where TControl : Control
	{
		private TControl _control;

		protected HostedWidgetBehavior()
		{
		}

		[CanBeNull]
		protected TControl Control
		{
			get => _control;
			set
			{
				if(_control != value)
				{
					if(_control != null)
					{
						Detach(_control);
					}
					_control = value;
					if(_control != null)
					{
						Attach(_control);
					}
				}
			}
		}

		private void OnWidgetHostChanged(object sender, EventArgs e)
		{
			Control = Widget.Host?.Control as TControl;
		}

		protected override void Attach(TWidget widget)
		{
			Assert.IsNotNull(widget);

			widget.HostChanged += OnWidgetHostChanged;
			Control = widget.Host?.Control as TControl;

			base.Attach(widget);
		}

		protected override void Detach(TWidget widget)
		{
			Assert.IsNotNull(widget);

			widget.HostChanged -= OnWidgetHostChanged;
			Control = null;

			base.Detach(widget);
		}

		protected virtual void Attach(TControl control)
		{
		}

		protected virtual void Detach(TControl control)
		{
		}
	}
}
