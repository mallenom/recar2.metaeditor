using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

using Mallenom;

namespace Recar2.ImageMetadatas
{
	/// <summary>Метаданные номера на изображении.</summary>
	public sealed class PlateMetadata
	{
		#region Data

		[CanBeNull]
		public string Number { set; get; }

		[CanBeNull]
		public string Stencil { set; get; }

		public PlateQuality Quality { set; get; } = PlateQuality.Normal;

		[CanBeNull]
		public string Country { get; set; }

		public double Confidence { get; set; }

		public PlateDataSymbol[] PlateDataSymbols { get; set; }

		public Point[] Coordinates { get; set; } = new Point[4];

		#endregion

		/// <summary>Сохраняет данные в XML.</summary>
		/// <param name="doc">XML документ в который нужно сохранить</param>
		/// <param name="node">дочерний XML элемент в который нужно сохранить</param>
		public void SaveXml(XmlDocument doc, XmlNode node)
		{
			node.WriteParameter("Number", Number?.Trim());
			node.WriteParameter("Stencil", Stencil?.Trim());
			node.WriteParameter("Quality", Quality);
			node.WriteParameter("Country", Country?.Trim());
			node.WriteParameter("Confidence", Confidence);

			var region = doc.CreateElement("Region");
			foreach(var p in Coordinates)
			{
				var point = doc.CreateElement("Point");
				var pointAttributeX = doc.CreateAttribute("X");
				pointAttributeX.Value = XmlConvert.ToString(p.X);
				var pointAttributeY = doc.CreateAttribute("Y");
				pointAttributeY.Value = XmlConvert.ToString(p.Y);
				point.Attributes.Append(pointAttributeX);
				point.Attributes.Append(pointAttributeY);
				region.AppendChild(point);
			}
			node.AppendChild(region);

			if(PlateDataSymbols == null) return;
			var symbols = doc.CreateElement("Symbols");
			foreach(var data in PlateDataSymbols)
			{
				var symbol = doc.CreateElement("Symbol");
				data.SaveXml(doc, symbol);
				symbols.AppendChild(symbol);
			}
			node.AppendChild(symbols);
		}

		/// <summary>Загружает данные из XML.</summary>
		/// <param name="node"></param>
		public void LoadXml(XmlNode node)
		{
			Number = node.ReadParameter("Number", string.Empty)?.Trim();
			Stencil = node.ReadParameter("Stencil", string.Empty)?.Trim();
			Quality = node.ReadParameter("Quality", Quality);
			Country = node.ReadParameter("Country", string.Empty)?.Trim();
			Confidence = node.ReadParameter("Confidence", Confidence);

			var regionNode = node.ChildNodes.Cast<XmlNode>().FirstOrDefault(n => n.Name.Equals("Region"));
			if(regionNode != null)
			{
				var index = 0;
				foreach(XmlNode pointNode in regionNode.ChildNodes)
				{
					var x = XmlConvert.ToInt32(pointNode.Attributes?["X"].Value ?? "0");
					var y = XmlConvert.ToInt32(pointNode.Attributes?["Y"].Value ?? "0");
					Coordinates[index] = new Point(x, y);
					++index;
					if(index >= Coordinates.Length) break;
				}
			}

			var symbolsNode = node.ChildNodes.Cast<XmlNode>().FirstOrDefault(n => n.Name.Equals("Symbols"));
			var plateDataSymbols = new List<PlateDataSymbol>();
			if(symbolsNode == null) return;
			foreach(XmlNode symbol in symbolsNode.ChildNodes)
			{
				var plateData = new PlateDataSymbol();
				plateData.LoadXml(symbol);
				plateDataSymbols.Add(plateData);
			}
			PlateDataSymbols = plateDataSymbols.ToArray();
		}

		public override string ToString() => $"{Number ?? string.Empty} ({Stencil ?? string.Empty})";
	}
}
