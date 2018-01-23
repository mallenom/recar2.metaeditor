using System;

using Recar2.Models;

namespace Recar2.MetaEditor
{
	/// <summary>Вспомогательный класс для отображения шаблонов номеров в ComboBox.</summary>
	public sealed class StencilItem : IComparable<StencilItem>
	{
		public static readonly StencilItem Unregistered = new StencilItem(null);

		private readonly StencilDescription _stencilDescription;

		public string StencilId => _stencilDescription?.Id ?? string.Empty;

		public StencilItem(StencilDescription stencilDescription)
		{
			_stencilDescription = stencilDescription;
		}

		public override string ToString() =>
			(_stencilDescription != null)
				? $"{_stencilDescription.Id} ({_stencilDescription.Name})"
				: "!Незарегистрированный шаблон";

		public int CompareTo(StencilItem other)
		{
			return StencilId.CompareTo(other.StencilId);
		}
	}
}
