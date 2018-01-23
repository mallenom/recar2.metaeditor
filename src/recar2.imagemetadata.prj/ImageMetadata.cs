using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

using Mallenom;

namespace Recar2.ImageMetadatas
{
	/// <summary>Метаданные изображения.</summary>
	public sealed class ImageMetadata
	{
		private const string Version = "1.0";

		/// <summary>Возвращает и устанавливает параметр, указывающий был ли данные проверены человеком.</summary>
		public bool HumanChecked { get; set; }

		public bool Important { get; set; }

		public ImageBrightness Brightness { get; set; } = ImageBrightness.Light;

		[CanBeNull]
		public string Description { get; set; }

		[CanBeNull]
		public string DongleId { get; set; }

		[CanBeNull]
		public PlateMetadata[] Plates { get; set; }

		/// <summary>Сохраняет данные в XML документ.</summary>
		/// <param name="doc">Документ в который нужно сохранить.</param>
		/// <param name="node"></param>
		public void SaveXml(XmlDocument doc, XmlNode node)
		{
			var versionAttr = doc.CreateAttribute("Version");
			versionAttr.Value = Version;
			node.Attributes?.Append(versionAttr);

			node.WriteParameter("DongleId", DongleId);
			node.WriteParameter("HumanChecked", HumanChecked);
			node.WriteParameter("Important", Important);
			node.WriteParameter("Brightness", Brightness);
			node.WriteParameter("Description", Description);

			var plates = doc.CreateElement("Plates");
			foreach(var plateMetadata in Plates)
			{
				var plateNode = doc.CreateElement("Plate");
				plateMetadata.SaveXml(doc, plateNode);
				plates.AppendChild(plateNode);
			}
			node.AppendChild(plates);
		}

		/// <summary>Выгружает данные из XML файла.</summary>
		/// <param name="doc">XML документ из которого будут получены данные.</param>
		/// <param name="node"></param>
		public void LoadXml(XmlDocument doc, XmlNode node)
		{
			var version = node.Attributes["Version"]; // TODO: Добавить проверку на корректность версии

			DongleId = node.ReadParameter("DongleId", string.Empty);
			HumanChecked = node.ReadParameter("HumanChecked", HumanChecked);
			Important = node.ReadParameter("Important", Important);
			Brightness = node.ReadParameter("Brightness", Brightness);
			Description = node.ReadParameter("Description", string.Empty);

			var platesNode = node.ChildNodes.Cast<XmlNode>().FirstOrDefault(n => n.Name.Equals("Plates", StringComparison.OrdinalIgnoreCase));
			if(platesNode == null) return;

			var plates = new List<PlateMetadata>();
			foreach(XmlNode cnode in platesNode.ChildNodes)
			{
				var plate = new PlateMetadata();
				plate.LoadXml(cnode);
				plates.Add(plate);
			}
			Plates = plates.ToArray();
		}
	}
}
