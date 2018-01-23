using System;
using System.Drawing;
using System.IO;

using NUnit.Framework;

using Recar2.ImageMetadatas;

// ReSharper disable InconsistentNaming
namespace Recar2.MetaEditor.Tests
{
	[TestFixture]
	class MetadataStorageTest
	{
		[Test]
		public void SaveAndLoadMetadata()
		{
			var filename = Path.Combine(TestContext.CurrentContext.TestDirectory, "sample1.xml");
			var storage = new MetadataStorage();
			var metadata = new ImageMetadata()
			{
				Description = "Описание",
				Brightness = ImageBrightness.Dark,
				DongleId="1000DF",
				Plates = new[]
				{
					new PlateMetadata()
					{
						Number = "T288EC98",
						Quality = PlateQuality.Low,
						Stencil = "RU_N01_a000aa00",
						Confidence=1,
						PlateDataSymbols=new[]
						{
							new PlateDataSymbol() {Value="1"  },
							new PlateDataSymbol() {Value="E", Confidence=0.5, Coordinates=new[] {new Point(11,45) } },
						},
					},
					new PlateMetadata()
					{
						Number = "C534YX177",
						Quality = PlateQuality.Normal,
						Stencil = "RU_N02_a000aa100",
						PlateDataSymbols=new[]
						{
							new PlateDataSymbol() {Value="A", Confidence=0, },
							new PlateDataSymbol() {Value="D", Confidence=1 },
						},
					},
				},
			};
			metadata.Plates[0].Coordinates[0] = new Point(111, 45);


			storage.SaveMetadata(filename, metadata);

			var loadedMetadata = storage.LoadMetadata(filename);

			Assert.That(loadedMetadata.HumanChecked, Is.EqualTo(metadata.HumanChecked));
			Assert.That(loadedMetadata.Brightness, Is.EqualTo(metadata.Brightness));
			Assert.That(loadedMetadata.Description, Is.EqualTo(metadata.Description));
			Assert.That(loadedMetadata.DongleId, Is.EqualTo(metadata.DongleId));
			Assert.That(loadedMetadata.Plates.Length, Is.EqualTo(metadata.Plates.Length));
			Assert.That(loadedMetadata.Plates[0].Number, Is.EqualTo(metadata.Plates[0].Number));
			Assert.That(loadedMetadata.Plates[0].Stencil, Is.EqualTo(metadata.Plates[0].Stencil));
			Assert.That(loadedMetadata.Plates[0].Quality, Is.EqualTo(metadata.Plates[0].Quality));
			Assert.That(loadedMetadata.Plates[0].Confidence, Is.EqualTo(metadata.Plates[0].Confidence));
			Assert.That(loadedMetadata.Plates[0].Coordinates[0], Is.EqualTo(metadata.Plates[0].Coordinates[0]));
			Assert.That(loadedMetadata.Plates[0].PlateDataSymbols[0].Value, Is.EqualTo(metadata.Plates[0].PlateDataSymbols[0].Value));
			Assert.That(loadedMetadata.Plates[0].PlateDataSymbols[0].Confidence, Is.EqualTo(metadata.Plates[0].PlateDataSymbols[0].Confidence));
			Assert.That(loadedMetadata.Plates[0].PlateDataSymbols[0].Coordinates[0], Is.EqualTo(metadata.Plates[0].PlateDataSymbols[0].Coordinates[0]));


		}
	}
}
// ReSharper restore InconsistentNaming
