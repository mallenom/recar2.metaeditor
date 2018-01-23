using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Mallenom.Diagnostics.Logs;

namespace Recar2.MetaEditor
{
	partial class Statistics : Form
	{
		#region Data

		private static readonly ILog Log = LogManager.GetCurrentClassLog();

		private readonly List<DirectoryStatistics> _listDirectoryStatistics;

		private readonly Dictionary<string, int> _stencils = new Dictionary<string, int>();

		private readonly StencilsProvider _stencilsProvider;

		private readonly int _widthImage;

		private string _path;

		#endregion

		#region Methods

		private void LoadCountries()
		{
			_cmbCountry.Items.Clear();
			_cmbCountry.Items.Add("Select country");
			foreach(var modelDescription in _stencilsProvider.GetModels())
			{
				_cmbCountry.Items.Add(new CountryItem(modelDescription));
			}

			_cmbCountry.SelectedIndex = 0;
		}

		public void FillingDictionaryStencils()
		{
			try
			{
				_stencils.Clear();
				foreach(var modelDescription in _stencilsProvider.GetModels())
				{
					var country = new CountryItem(modelDescription);
					foreach(var template in _stencilsProvider.GetStencils(country.ModelId))
					{
						_stencils.Add(new StencilItem(template).StencilId, 0);
					}
				}
			}
			catch(Exception exc)
			{
				Log.Error("Load stencils fail", exc);
			}
		}

		private void FolderDialog()
		{
			using(var folderDialog = new FolderBrowserDialog())
			{
				folderDialog.SelectedPath = _path;
				if(folderDialog.ShowDialog(this) == DialogResult.Cancel) return;

				_path = folderDialog.SelectedPath;
			}
		}

		private void FillingStencilsGrid(CountryItem country)
		{
			foreach(var value in _stencils)
			{
				if(country == null)
				{
					_dgvStensils.Rows.Add(value.Key, value.Value);
				}
				else
				{
					foreach(var template in _stencilsProvider.GetStencils(country.ModelId))
					{
						var stencil = new StencilItem(template).StencilId;
						if(!value.Key.Equals(stencil)) continue;
						_dgvStensils.Rows.Add(value.Key, value.Value);
					}
				}
			}
		}

		/// <summary> Заполнение диаграммы разметки изображений. </summary>
		/// <param name="marked">Число размеченных.</param>
		/// <param name="unmarked">Число не размеченных.</param>
		private void FillingMarkupChart(int marked, int unmarked)
		{
			_chartPieMarkup.Series[0].Points.Clear();
			_chartPieMarkup.Series[0].Points.Add(marked);
			_chartPieMarkup.Series[0].Points.Add(unmarked);
			_chartPieMarkup.Series[0].Points[0].LegendText = $"Marked - {marked}";
			_chartPieMarkup.Series[0].Points[1].LegendText = $"Unmarked - {unmarked}";
			_chartPieMarkup.Series[0].Points[0].Label = marked.ToString();
			_chartPieMarkup.Series[0].Points[1].Label = unmarked.ToString();
		}

		/// <summary>Заполнение диаграммы времени суток.</summary>
		/// <param name="light">Число дневных изображений.</param>
		/// <param name="dark">Число ночных изображений.</param>
		private void FillingTimesDayChart(int light, int dark)
		{
			_chartPieTimesDay.Series[0].Points.Clear();
			_chartPieTimesDay.Series[0].Points.Add(light);
			_chartPieTimesDay.Series[0].Points.Add(dark);
			_chartPieTimesDay.Series[0].Points[0].LegendText = $"Light - {light}";
			_chartPieTimesDay.Series[0].Points[1].LegendText = $"Dark - {dark}";
			_chartPieTimesDay.Series[0].Points[0].Label = light.ToString();
			_chartPieTimesDay.Series[0].Points[1].Label = dark.ToString();
		}

		/// <summary>Заполнение диаграммы качества номера. </summary>
		/// <param name="high">Число высококачественных номеров.</param>
		/// <param name="normal">Число среднего качества номеров.</param>
		/// <param name="low">Число низкокачественных номеров.</param>
		private void FillingQualityNumberChart(int high, int normal, int low)
		{
			_chartPieQualityNumber.Series[0].Points.Clear();
			_chartPieQualityNumber.Series[0].Points.Add(high);
			_chartPieQualityNumber.Series[0].Points.Add(normal);
			_chartPieQualityNumber.Series[0].Points.Add(low);
			_chartPieQualityNumber.Series[0].Points[0].LegendText = $"High - {high}";
			_chartPieQualityNumber.Series[0].Points[1].LegendText = $"Normal - {normal}";
			_chartPieQualityNumber.Series[0].Points[2].LegendText = $"Low - {low}";
			_chartPieQualityNumber.Series[0].Points[0].Label = high.ToString();
			_chartPieQualityNumber.Series[0].Points[1].Label = normal.ToString();
			_chartPieQualityNumber.Series[0].Points[2].Label = low.ToString();
		}

		/// <summary>Заполнение диаграммы количество номеров на изображении.</summary>
		/// <param name="one">Число одного номера на изображении.</param>
		/// <param name="two">Число нескольких номеров на изображении.</param>
		/// <param name="twoMore">Число больше двух номеров на изображении. </param>
		private void FillingCountNumberChart(int one, int two, int twoMore)
		{
			_chartPieCountNumber.Series[0].Points.Clear();
			_chartPieCountNumber.Series[0].Points.Add(one);
			_chartPieCountNumber.Series[0].Points.Add(two);
			_chartPieCountNumber.Series[0].Points.Add(twoMore);
			_chartPieCountNumber.Series[0].Points[0].LegendText = $"One plate - {one}";
			_chartPieCountNumber.Series[0].Points[1].LegendText = $"Two plate - {two}";
			_chartPieCountNumber.Series[0].Points[2].LegendText = $"More two plate - {twoMore}";
			_chartPieCountNumber.Series[0].Points[0].Label = one.ToString();
			_chartPieCountNumber.Series[0].Points[1].Label = two.ToString();
			_chartPieCountNumber.Series[0].Points[2].Label = twoMore.ToString();
		}

		private void FillingCharts()
		{
			try
			{
				Cursor = Cursors.WaitCursor;

				var directory = new DirectoryStatistics
				{
					DirectoryName = Path.GetFileName(_path),
					PathDirectory = _path
				};

				_listDirectoryStatistics.Add(directory);

				Core.SearchAllFiles(_path, _listDirectoryStatistics, _stencils, _widthImage);

				_dgvStensils.Rows.Clear();
				var marked = 0;
				var unmarked = 0;
				var light = 0;
				var dark = 0;
				var high = 0;
				var normal = 0;
				var low = 0;
				var oneNumber = 0;
				var twoNumber = 0;
				var twoMoreNumber = 0;
				foreach(var value in _listDirectoryStatistics)
				{
					marked += value.Marked;
					unmarked += value.Unplaced;

					light += value.CountLightImages;
					dark += value.CountDarkImages;

					high += value.CountHighQuality;
					normal += value.CountMediumQuality;
					low += value.CountLowQuality;

					oneNumber += value.CountOneNumber;
					twoNumber += value.CountTwoNumber;
					twoMoreNumber += value.CountMoreTwoNumber;

					FillingStencilsGrid(null);
				}

				FillingMarkupChart(marked, unmarked);
				FillingTimesDayChart(light, dark);
				FillingQualityNumberChart(high, normal, low);
				FillingCountNumberChart(oneNumber, twoNumber, twoMoreNumber);

				_listDirectoryStatistics.Clear();
			}
			catch(Exception exc)
			{
				Log.Error(exc.Message, exc);
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		#endregion

		#region Event handlers

		public Statistics(string path, int widthImage)
		{
			InitializeComponent();

			_widthImage = widthImage;
			_path = path;
			_listDirectoryStatistics = new List<DirectoryStatistics>();
			_stencilsProvider = new StencilsProvider();
		}

		private void Statistics_Load(object sender, EventArgs e)
		{
			try
			{
				LoadCountries();
				FillingDictionaryStencils();
			}
			catch(Exception exc)
			{
				Log.Error(exc.Message, exc);
			}
		}

		private void _lblSelectDirectory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			
		}

		private void _cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			CountryItem country;
			if(_cmbCountry.SelectedItem.ToString().Equals("Select country")) country = null;
			else country = (CountryItem)_cmbCountry.SelectedItem;

			_dgvStensils.Rows.Clear();
			FillingStencilsGrid(country);
		}

		private void _selectDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FolderDialog();
			FillingCharts();
		}

		#endregion


	}
}
