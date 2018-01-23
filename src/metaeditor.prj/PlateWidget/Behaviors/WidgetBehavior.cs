using Mallenom;
using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	public abstract class WidgetBehavior : IWidgetBehavior
	{
		private Widget _widget;

		protected WidgetBehavior()
		{
		}

		[CanBeNull]
		public Widget Widget
		{
			get => _widget;
			set
			{
				if(_widget != value)
				{
					if(_widget != null)
					{
						Detach(_widget);
					}
					_widget = value;
					if(_widget != null)
					{
						Attach(_widget);
					}
				}
			}
		}

		protected virtual void Detach(Widget widget)
		{
		}

		protected virtual void Attach(Widget widget)
		{
		}
	}

	public abstract class WidgetBehavior<T> : IWidgetBehavior
		where T : Widget
	{
		private Widget _attached;
		private T _widget;

		protected WidgetBehavior()
		{
		}

		[CanBeNull]
		Widget IWidgetBehavior.Widget
		{
			get => _attached;
			set
			{
				if(_attached != value)
				{
					_attached = value;
					Widget = value as T;
				}
			}
		}

		[CanBeNull]
		public T Widget
		{
			get => _widget;
			private set
			{
				if(_widget != value)
				{
					if(_widget != null)
					{
						Detach(_widget);
					}
					_widget = value;
					if(_widget != null)
					{
						Attach(_widget);
					}
				}
			}
		}

		protected virtual void Detach(T widget)
		{
		}

		protected virtual void Attach(T widget)
		{
		}
	}
}
