using System;

using Mallenom;

using Recar2.Harvest;
using Recar2.ImageMetadatas;

namespace Recar2.MetaEditor
{
	sealed class ImageItem
	{
		public string Path { get; }

		public ImageMetadata Metadata { get; }

		public IntermediateMetadata AlgorithmMetadata { get; } 

		public ImageItem(string path, ImageMetadata metadata, IntermediateMetadata algorithmMetadata)
		{
			Path = path;
			Metadata = metadata;
			AlgorithmMetadata = algorithmMetadata;
		}
	}

	sealed class DirectoryItem
	{
		public string Path { get; }

		[CanBeNull]
		public DirStatistics Statistics { get; }

		public DirectoryItem(string path)
		{
			Path = path;
			Statistics = new DirStatistics();
		}
	}
}
