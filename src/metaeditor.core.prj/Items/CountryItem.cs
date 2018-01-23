using System;

using Recar2.Models;

namespace Recar2.MetaEditor
{
	/// <summary>Вспомогательный класс для отображения стран в ComboBox.</summary>
	public sealed class CountryItem
	{
		public static readonly CountryItem Unregistered = new CountryItem(null);

		private readonly ModelDescription _modelDescription;

		public string ModelId => _modelDescription != null ? _modelDescription.Id : string.Empty;

		public CountryItem(ModelDescription modelDescription)
		{
			_modelDescription = modelDescription;
		}

		public override string ToString() =>
			(_modelDescription != null)
				? $"{_modelDescription.Name} ({_modelDescription.Id})"
				: "!Незарегистрированная страна";
	}
}
