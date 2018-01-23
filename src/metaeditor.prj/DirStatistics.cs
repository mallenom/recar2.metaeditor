using System;
using System.Collections.Generic;

namespace Recar2.MetaEditor
{
	sealed class DirStatistics
	{
		public class StencilCount
		{
			public int TotalPlateCount { set; get; }
			public int MarkupPlateCount { set; get; }
		}

		public int TotalImageCount { get; set; }

		public int MarkupImageCount { get; set; }

		public int RecogImageCount { get; set; }

		public Dictionary<string, StencilCount> Stencils { get; } =new Dictionary<string, StencilCount>();

		public void Clear()
		{
			TotalImageCount = 0;
			MarkupImageCount = 0;
			RecogImageCount = 0;
			Stencils.Clear();
		}
	}
}
