using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mallenom;

using Recar2.Algorithms;
using Recar2.ImageMetadatas;

namespace Recar2.MetaEditor
{
	sealed class FilterCriterion
	{
		public bool OnlyImportant { set; get; }

		public string Pattern
		{
			set
			{
				_pattern = value;
				if(string.IsNullOrWhiteSpace(_pattern))
				{
					_rxFilter = null;
				}
				else
				{
					var rxPattern = $".*{_pattern}.*";
					_rxFilter = new Regex(rxPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
				}
			}
			get => _pattern;
		}

		[CanBeNull]
		private Regex _rxFilter;

		private string _pattern;

		public bool IsAllow(ImageMetadata metadata)
		{
			if(metadata != null && _rxFilter != null)
			{
				if(OnlyImportant && !metadata.Important) return false;
				if(!metadata.Plates?.Any(p => _rxFilter.IsMatch(p.Number) || _rxFilter.IsMatch(p.Stencil)) ?? false) return false;
			}
			return true;
		}
	}

	sealed class Core : IDisposable
	{
		#region Data

		private static readonly Dictionary<string, string> CyrillicToLatinMap = new Dictionary<string, string>
		{
			{"А", "A"},
			{"В", "B"},
			{"Г", "G"},
			{"Д", "D"},
			{"Е", "E"},
			{"З", "Z"},
			{"К", "K"},
			{"Л", "L"},
			{"О", "O"},
			{"Р", "P"},
			{"С", "C"},
			{"Т", "T"},
			{"Н", "H"},
			{"У", "Y"},
			{"Х", "X"},
			{"М", "M"}
		};

		private static readonly string[] Eng =
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F",
			"G",
			"H",
			"I",
			"J",
			"K",
			"L",
			"M",
			"N",
			"O",
			"P",
			"Q",
			"R",
			"S",
			"T",
			"U",
			"V",
			"W",
			"X",
			"Y",
			"Z",
			"_"
		};

		private static readonly string[] Digits = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

		private readonly MetadataStorage _metadataStorage;

		#endregion

		#region Create & destroy

		public Core()
		{
			_metadataStorage = new MetadataStorage();
		}

		public void Dispose()
		{
		}

		#endregion

		#region Methods

		[CanBeNull]
		public ImageMetadata LoadMetadata(string imagePath)
		{
			var metadataFilename = Path.ChangeExtension(imagePath, "xml");
			return File.Exists(metadataFilename) ? new MetadataStorage().LoadMetadata(metadataFilename) : null;
		}

		[CanBeNull]
		public async Task<ImageMetadata> LoadMetadataAsync(string imagePath, CancellationToken cancellationToken)
		{
			var metadataFilename = Path.ChangeExtension(imagePath, "xml");
			if(File.Exists(metadataFilename))
			{
				return await new MetadataStorage().LoadMetadataAsync(metadataFilename, cancellationToken);
			}
			return default;
		}

		public void SaveMetadata(string imagePath, ImageMetadata metadata)
		{
			var metadataFilename = Path.ChangeExtension(imagePath, "xml");
			_metadataStorage.SaveMetadata(metadataFilename, metadata);
		}

		public static void HideEmptyDirectories(TreeNodeCollection nodes)
		{
			foreach(var node in nodes.Cast<TreeNode>().Where(n => n.Tag is DirectoryItem && n.Nodes.Count == 0).ToArray())
			{
				nodes.RemoveAt(node.Index);
			}
		}

		/// <summary> Поиск всех файлов для подсчета статистики. </summary>
		/// <param name="directoryName"> Выбранная директория. </param>
		/// <param name="list"> Лист с объектами </param>
		/// <param name="stencils"></param>
		/// <param name="widthImage"></param>
		public static void SearchAllFiles(string directoryName, List<DirectoryStatistics> list, Dictionary<string, int> stencils, int widthImage)
		{
			var files = Directory.GetFileSystemEntries(directoryName);

			foreach(var file in files)
			{
				if(string.IsNullOrWhiteSpace(Path.GetExtension(file)))
				{
					var directory = new DirectoryStatistics
					{
						DirectoryName = Path.GetFileName(file),
						PathDirectory = file
					};
					list.Add(directory);
					Statistics(file, list, stencils, widthImage);
				}
				else
				{
					if(file.IsImage())
					{
						list.First().Counting(file, stencils, widthImage);
					}
				}
			}
		}

		/// <summary> Конвертирует строку в большие буквы и заменяет русские буквы на английские. </summary>
		/// <param name="number"> Значение из _txtAddPlate </param>
		public static string SanitizePlate(string number)
		{
			if(string.IsNullOrWhiteSpace(number)) throw new ArgumentNullException();

			var value = number.ToUpper();
			var s = new StringBuilder();
			foreach(var ch in value)
			{
				if(CyrillicToLatinMap.ContainsKey(ch.ToString()))
				{
					s.Append(CyrillicToLatinMap[ch.ToString()]);
				}
				else
				{
					if(Eng.Any(x => x.Equals(ch.ToString())))
					{
						s.Append(ch.ToString());
					}
					else if(Digits.Any(x => x.Equals(ch.ToString())))
					{
						s.Append(ch.ToString());
					}
					else s.Append("?");
				}
			}
			if(value.Length > s.Length) throw new ArgumentException("Invalid character.");

			return s.ToString();
		}

		public static PlateQuality Quality(bool high, bool normal)
		{
			if(high) return PlateQuality.High;
			return normal ? PlateQuality.Normal : PlateQuality.Low;
		}

		/// <summary> Возвращает картинку выбранного шаблона. </summary>
		/// <param name="selectedStencil"> Выбранный шаблон. </param>
		public static Image StencilImage(string selectedStencil)
		{
			var filename = Path.Combine(Application.StartupPath, @"StencilsImages");
			var notFoundNumber = Image.FromFile(Path.Combine(Application.StartupPath, @"StencilsImages/nullNumber.png"));

			foreach(var file in Directory.GetFiles(filename))
			{
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
				if(fileNameWithoutExtension.Equals(selectedStencil))
				{
					return Image.FromFile(file);
				}
			}

			return notFoundNumber;
		}

		/// <summary> Конвертирует номер в шаблон заданной страны. </summary>
		/// <param name="number"> Выбранный номер. </param>
		/// <param name="country"> Выбранная страна. </param>
		/// <returns> Шаблон. </returns>
		public static string FormatNumberToStencils(string number, string country)
		{
			var s = new StringBuilder();

			foreach(var ch in number)
			{
				if(Eng.Any(x => x.Equals(ch.ToString())))
				{
					if(ch.Equals('_'))
					{
						s.Append("_");
						continue;
					}
					s.Append("a");
				}
				else
				{
					if(Digits.Any(x => x.Equals(ch.ToString()))) s.Append("0");
				}
			}

			if(number.Length == 9)
			{
				if(country.Equals("ru") && number.Substring(6, 3).Equals("100"))
				{
					s.Remove(6, 3);
					s.Append("100");
				}
				else if(country.Equals("ru") && number.Substring(6, 1).Equals("7"))
				{
					s.Remove(6, 3);
					s.Append("700");
				}
			}

			return s.ToString();
		}

		/// <summary> Изменяет выбранный номер. </summary>
		/// <param name="number">Номер из _txtAddPlate</param>
		/// <param name="plate">Последний выбранный plate</param>
		public static void ChangeNumber(string number, PlateMetadata plate)
		{
			if(!plate.Number.Equals(number))
			{
				plate.Number = SanitizePlate(number);
			}
		}

		#endregion

		#region Private methods

		private static void Statistics(string directoryName, ICollection<DirectoryStatistics> list, Dictionary<string, int> stencils, int widthImage)
		{
			var files = Directory.GetFiles(directoryName).Where(p => p.IsImage());
			foreach(var file in files)
			{
				list.Last().Counting(file, stencils, widthImage);
			}

			foreach(var directory in Directory.GetDirectories(directoryName))
			{
				var dir = new DirectoryStatistics
				{
					DirectoryName = Path.GetFileName(directory),
					PathDirectory = directory
				};
				list.Add(dir);
				Statistics(directory, list, stencils, widthImage);
			}
		}

		#endregion
	}
}
