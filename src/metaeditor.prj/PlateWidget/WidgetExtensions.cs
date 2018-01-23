using System;

using Mallenom;

namespace Recar2.Algorithms.Widgets
{
	static class WidgetExtensions
	{
		public static decimal ToPercent(this double value)
		{
			Verify.Argument.IsInRange(0.0, value, 1.0, nameof(value));

			return (decimal)(100.0 * value);
		}

		public static decimal ToPercent(this float value)
		{
			Verify.Argument.IsInRange(0.0, value, 1.0, nameof(value));

			return (decimal)(100.0 * value);
		}
	}
}
