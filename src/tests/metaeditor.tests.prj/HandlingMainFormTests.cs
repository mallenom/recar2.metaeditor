using System;

using NUnit.Framework;

using Recar2.ImageMetadatas;

namespace Recar2.MetaEditor.Tests
{
	[TestFixture]
	class HandlingMainFormTests
	{
		[Test]
		public void FormatNumber_CarNumber_CorrectNumber()
		{
			var number = "а123yр35";

			var result = Core.SanitizePlate(number);

			Assert.That(result, Is.EqualTo("A123YP35"));
		}

		[Test]
		public void FormatNumberToStencil_CarNumber_Stencil()
		{
			var number = "1344_CAB";

			var result = Core.FormatNumberToStencils(number, "mn");

			Assert.That(result, Is.EqualTo("0000_aaa"));
		}

		[Test]
		public void ChangeNumber_CarNumberAndPlate_EditedNumber()
		{
			var number = "C123AB35";
			var plate = new PlateMetadata
			{
				Number = "C124AC35",
			};

			Core.ChangeNumber(number, plate);

			Assert.That(plate.Number, Is.EqualTo(number));
		}
	}
}
