using System.Threading;
using System.Threading.Tasks;

namespace Recar2.ImageMetadatas
{
	public interface IMetaDataStorage
	{
		Task SaveMetadataAsync(string metadataFilename, ImageMetadata metadata, CancellationToken token);

		Task<ImageMetadata> LoadMetadataAsync(string metadataFilename, CancellationToken token);
	}
}
