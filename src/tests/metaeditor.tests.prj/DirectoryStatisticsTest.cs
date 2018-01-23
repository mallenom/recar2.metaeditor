using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using NUnit.Framework;

using Recar2.ImageMetadatas;

namespace Recar2.MetaEditor.Tests
{
	[TestFixture]
	class DirectoryStatisticsTest
	{
		[Test]
		public void Counting_FileName_CorrectResultStatistics()
		{
			var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, "sample1.xml");
			var storage = new MetadataStorage();
			var metadata = new ImageMetadata
			{
				Description = "Описание",
				Brightness = ImageBrightness.Dark,
				Plates = new[]
				{
					new PlateMetadata() {Number = "T288EC98", Quality = PlateQuality.Low, Stencil = "RU_N01_a000aa00"},
					new PlateMetadata() {Number = "C534YX177", Quality = PlateQuality.Normal, Stencil = "RU_N02_a000aa100"},
				},
			};
			metadata.Plates[0].Coordinates[0] = new Point(111, 45);
			storage.SaveMetadata(fileName, metadata);
			var dictionary = new Dictionary<string, int>
			{
				{"RU_N01_a000aa00", 0},
				{"RU_N01_a000aa100", 0},
			};
			var stat = new DirectoryStatistics();

			stat.Counting(fileName, dictionary, 1000);

			Assert.That(stat.CountImages, Is.EqualTo(1));
			Assert.That(stat.CountDarkImages, Is.EqualTo(1));
			Assert.That(stat.CountLightImages, Is.Zero);
			Assert.That(stat.CountLargeNumber, Is.Zero);
			Assert.That(stat.CountMediumNumber, Is.EqualTo(1));
			Assert.That(stat.CountSmallNumber, Is.Zero);
			Assert.That(stat.CountHighQuality, Is.Zero);
			Assert.That(stat.CountMediumQuality, Is.EqualTo(1));
			Assert.That(stat.CountLowQuality, Is.EqualTo(1));
			Assert.That(stat.CountOneNumber, Is.Zero);
			Assert.That(stat.CountTwoNumber, Is.EqualTo(1));
			Assert.That(stat.CountMoreTwoNumber, Is.Zero);
			Assert.That(stat.Marked, Is.Zero);
			Assert.That(stat.Unplaced, Is.EqualTo(1));
		}
	}
}
