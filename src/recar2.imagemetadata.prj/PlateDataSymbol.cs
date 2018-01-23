using System;
using System.Drawing;
using System.Globalization;
using System.Xml;

namespace Recar2.ImageMetadatas
{
	/// <summary> Метаданные символов </summary>
	public sealed class PlateDataSymbol
	{
		#region Data

		public string Value { get; set; }

		public double Confidence { get; set; }

		public Point[] Coordinates { get; set; } = new Point[4];

		#endregion

		/// <summary>Сохраняет данные в XML.</summary>
		/// <param name="doc">XML документ в который нужно сохранить</param>
		/// <param name="node">дочерний XML элемент в который нужно сохранить</param>
		public void SaveXml(XmlDocument doc, XmlNode node)
		{
			var valueAttr = doc.CreateAttribute("Value");
			var confidenceAttr = doc.CreateAttribute("Confidence");

			valueAttr.Value = Value?.Trim();
			confidenceAttr.Value = XmlConvert.ToString(Confidence);

			node.Attributes?.Append(valueAttr);
			node.Attributes?.Append(confidenceAttr);

			if(Coordinates == null) return;
			foreach(var p in Coordinates)
			{
				var point = doc.CreateElement("Point");
				var pointAttributeX = doc.CreateAttribute("X");
				pointAttributeX.Value = XmlConvert.ToString(p.X);
				var pointAttributeY = doc.CreateAttribute("Y");
				pointAttributeY.Value = XmlConvert.ToString(p.Y);
				point.Attributes.Append(pointAttributeX);
				point.Attributes.Append(pointAttributeY);
				node.AppendChild(point);
			}
		}

		/// <summary>Загружает данные из XML.</summary>
		/// <param name="node"></param>
		public void LoadXml(XmlNode node)
		{
			Value = node.Attributes?["Value"]?.Value;
			var confidenceAttr = node.Attributes?["Confidence"]?.Value;
			Confidence = confidenceAttr != null ? XmlConvert.ToDouble(confidenceAttr) : 0.0;

			var index = 0;
			foreach(XmlNode pointNode in node.ChildNodes)
			{
				var x = XmlConvert.ToInt32(pointNode.Attributes?["X"].Value ?? "0");
				var y = XmlConvert.ToInt32(pointNode.Attributes?["Y"].Value ?? "0");
				Coordinates[index] = new Point(x, y);
				++index;
				if(index >= Coordinates.Length) break;
			}
		}

	}
}
