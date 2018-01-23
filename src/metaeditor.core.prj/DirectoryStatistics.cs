using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Recar2.ImageMetadatas;

namespace Recar2.MetaEditor
{
	public sealed class DirectoryStatistics
	{
		#region Data

		public int Marked { get; set; }

		public int Unplaced { get; set; }

		public int CountImages { get; set; }

		public int CountLightImages { get; set; }

		public int CountDarkImages { get; set; }

		public int CountHighQuality { get; set; }

		public int CountMediumQuality { get; set; }

		public int CountLowQuality { get; set; }

		public int CountOneNumber { get; set; }

		public int CountTwoNumber { get; set; }

		public int CountMoreTwoNumber { get; set; }

		public int CountSmallNumber { get; set; }

		public int CountMediumNumber { get; set; }

		public int CountLargeNumber { get; set; }

		public string DirectoryName { get; set; }

		public string PathDirectory { get; set; }

		#endregion

		public void Counting(string fileName, Dictionary<string, int> stencils, int widthImage)
		{
			var metaData = new MetadataStorage();
			var imageMetaData = new ImageMetadata();

			CountImages++;

			var xmlFile = Path.Combine(Path.ChangeExtension(fileName, "xml"));
			if(File.Exists(xmlFile))
			{
				imageMetaData = metaData.LoadMetadata(xmlFile);

				if(imageMetaData.HumanChecked) Marked++;
				else Unplaced++;

				if(imageMetaData.Brightness == ImageBrightness.Light) CountLightImages++;
				else CountDarkImages++;

				if(imageMetaData.Plates == null) return;

				switch(imageMetaData.Plates.Length)
				{
					case 1:
						CountOneNumber++;
						break;
					case 2:
						CountTwoNumber++;
						break;
					case 3:
						CountMoreTwoNumber++;
						break;
				}

				CountingInfoAboutPlate(imageMetaData.Plates, stencils, widthImage);
			}
			else
			{
				Unplaced++;
			}
		}

		private void CountingInfoAboutPlate(PlateMetadata[] plates, Dictionary<string, int> stencils, int widthImage)
		{
			foreach(var plate in plates)
			{
				switch(plate.Quality)
				{
					case PlateQuality.High:
						CountHighQuality++;
						break;
					case PlateQuality.Normal:
						CountMediumQuality++;
						break;
					case PlateQuality.Low:
						CountLowQuality++;
						break;
				}

				if(plate.Stencil == string.Empty) continue;

				var findStencil = stencils.FirstOrDefault(x => x.Key.Equals(plate.Stencil.Split(' ')[0]));
				if(findStencil.Key == null) continue;

				var count = findStencil.Value + 1;
				stencils.Remove(findStencil.Key);
				stencils.Add(findStencil.Key, count);

				var percent = Math.Ceiling((decimal)Math.Abs(plate.Coordinates[0].X - plate.Coordinates[1].X) / widthImage * 100);
				if(percent < 10) CountSmallNumber++;
				else if(percent > 10 && percent < 30) CountMediumNumber++;
				else CountLargeNumber++;
			}
		}
	}
}
