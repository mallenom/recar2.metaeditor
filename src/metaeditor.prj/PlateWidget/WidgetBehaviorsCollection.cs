using System.Collections.ObjectModel;

using Mallenom;
using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Коллекция поведений виджетов.</summary>
	public sealed class WidgetBehaviorsCollection : Collection<IWidgetBehavior>
	{
		internal WidgetBehaviorsCollection(Widget owner)
		{
			Verify.Argument.IsNotNull(owner, nameof(owner));

			Owner = owner;
		}

		/// <summary>Возвращает владельца коллекции.</summary>
		[NotNull]
		private Widget Owner { get; }

		protected override void InsertItem(int index, IWidgetBehavior item)
		{
			Verify.Argument.IsNotNull(item, nameof(item));
			Verify.Argument.IsTrue(item.Widget == null, "Specified behavior is already attached to a widget.");

			base.InsertItem(index, item);
			item.Widget = Owner;
		}

		protected override void SetItem(int index, IWidgetBehavior item)
		{
			Verify.Argument.IsNotNull(item, nameof(item));
			Verify.Argument.IsTrue(item.Widget == null, "Specified behavior is already attached to a widget.");

			var old = Items[index];
			old.Widget = null;
			base.SetItem(index, item);
			item.Widget = Owner;
		}

		protected override void RemoveItem(int index)
		{
			var old = Items[index];
			old.Widget = null;
			base.RemoveItem(index);
		}

		protected override void ClearItems()
		{
			foreach(var item in Items)
			{
				item.Widget = null;
			}
			base.ClearItems();
		}
	}
}
