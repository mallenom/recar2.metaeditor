using System;

namespace Recar2.Algorithms.Widgets
{
	/// <summary>Аргументы события измеения региона.</summary>
	public class RegionChangedEventArgs : EventArgs
	{
		/// <summary>Аргументы для события изменения положения региона.</summary>
		public static RegionChangedEventArgs ForLocationChanged { get; } = new RegionChangedEventArgs(locationChanged: true);

		/// <summary>Аргументы для события изменения формы региона.</summary>
		public static RegionChangedEventArgs ForShapeChanged { get; } = new RegionChangedEventArgs(shapeChanged: true);

		/// <summary>Создание <see cref="RegionChangedEventArgs"/>.</summary>
		/// <param name="locationChanged">Флаг изменения положения региона.</param>
		/// <param name="shapeChanged">Флаг изменения формы региона.</param>
		public RegionChangedEventArgs(bool locationChanged = false, bool shapeChanged = false)
		{
			LocationChanged = locationChanged;
			ShapeChanged = shapeChanged;
		}

		/// <summary>Возвращает флаг измеения положения региона.</summary>
		/// <value>Флаг изменения положения региона.</value>
		public bool LocationChanged { get; }

		/// <summary>Возвращает флаг измеения формы региона.</summary>
		/// <value>Флаг изменения формы региона.</value>
		public bool ShapeChanged { get; }
	}
}
