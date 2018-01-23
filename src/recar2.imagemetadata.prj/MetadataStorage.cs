using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Recar2.ImageMetadatas
{
	/// <summary>Класс для сохранения и загрузки метаданных изображений.</summary>
	public sealed class MetadataStorage : IMetaDataStorage
	{
		public ImageMetadata LoadMetadata(string metadataFilename)
		{
			var doc = new XmlDocument();
			doc.Load(metadataFilename);
			var metadata = new ImageMetadata();
			metadata.LoadXml(doc, doc.DocumentElement);
			return metadata;
		}

		public void SaveMetadata(string metadataFilename, ImageMetadata metadata)
		{
			using(var writer = XmlWriter.Create(metadataFilename))
			{
				writer.WriteStartElement("Image");
			}
			var doc = new XmlDocument();
			doc.Load(metadataFilename);
			metadata.SaveXml(doc, doc.DocumentElement);
			doc.Save(metadataFilename);
		}

		public async Task SaveMetadataAsync(string metadataFilename, ImageMetadata metadata, CancellationToken token)
		{
			using(var writer = XmlWriter.Create(metadataFilename))
			{
				writer.WriteStartElement("Image");
			}
			var doc = new XmlDocument();
			doc.Load(metadataFilename);
			metadata.SaveXml(doc, doc.DocumentElement);
			doc.Save(metadataFilename);
		}

		public async Task<ImageMetadata> LoadMetadataAsync(string metadataFilename, CancellationToken token)
		{
			return await Task.Run(() =>
			{
				var doc = new XmlDocument();
				doc.Load(metadataFilename);
				var metadata = new ImageMetadata();
				metadata.LoadXml(doc, doc.DocumentElement);
				return metadata;
			}, token);
		}
	}
}
