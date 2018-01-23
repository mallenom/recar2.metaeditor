using Mallenom.Widgets;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Поведение виджета.</summary>
	public interface IWidgetBehavior
	{
		/// <summary>Возвращает и устанавливает виджет, для которого актуально данное поведение.</summary>
		Widget Widget { get; set; }
	}
}
