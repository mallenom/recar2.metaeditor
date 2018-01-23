using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Diagnostics.Logs;

using Recar2.Models;

namespace Recar2.MetaEditor
{
	/// <summary>Предоставляет список шаблонов номеров, поддерживаемых ядром распознавания.</summary>
	sealed class StencilsProvider
	{
		private static readonly ILog Log = LogManager.GetCurrentClassLog();

		private sealed class DumbDecoder : IDecoder
		{
			public string GetPostfix() => string.Empty;

			public Stream Decode(Stream stream) => stream;
		}

		private const string ModelFilename = @"recar2.models.dll";

		private readonly ModelsLoader _modelsLoader;

		public StencilsProvider()
		{
			var path = Path.Combine(Application.StartupPath, ModelFilename);
			if(!File.Exists(path)) throw new ArgumentException($"Models file {path} not found.", nameof(path));
			var assembly = Assembly.LoadFile(path);
			var container = new AssemblyModelResourcesContainer(assembly);
			_modelsLoader = new ModelsLoader(container, new DumbDecoder());
			Log.Info($"Loaded plate models: {_modelsLoader.ModelDescriptions.Count}.");
		}

		/// <summary>Возвращает список описаний зарегистрированных стран.</summary>
		/// <returns>Список описаний стран.</returns>
		public IReadOnlyCollection<ModelDescription> GetModels()
		{
			return _modelsLoader.ModelDescriptions;
		}

		/// <summary>Возвращает список описаний шаблонов номеров для указанной страны.</summary>
		/// <param name="modelId">Идентификатор страны.</param>
		/// <returns>Список описаний шаблонов.</returns>
		public IReadOnlyCollection<StencilDescription> GetStencils(string modelId)
		{
			return _modelsLoader.ModelDescriptions.FirstOrDefault(md => md.Id.Equals(modelId))?.Stencils ?? throw new ArgumentException(@"Model not found.", nameof(modelId));
		}

		/// <summary>Возвращает страну, к которой относиться указанный шаблон номера.</summary>
		/// <param name="stencilId">Идентификатор шаблона номера.</param>
		/// <returns>Описание страны или <c>null</c>.</returns>
		[CanBeNull]
		public ModelDescription GetModel(string stencilId)
		{
			return _modelsLoader.ModelDescriptions.FirstOrDefault(m => m.Stencils.Any(s => s.Id.Equals(stencilId, StringComparison.OrdinalIgnoreCase)));
		}

		public bool HasModel(string modelId)
		{
			return _modelsLoader.ModelDescriptions.Any(m => m.Id.Equals(modelId));
		}
	}
}
