namespace Recar2.MetaEditor
{
	partial class Statistics
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title7 = new System.Windows.Forms.DataVisualization.Charting.Title();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title8 = new System.Windows.Forms.DataVisualization.Charting.Title();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this._dgvStensils = new System.Windows.Forms.DataGridView();
			this.Stensil = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._cmbCountry = new System.Windows.Forms.ComboBox();
			this._chartPieMarkup = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this._chartPieTimesDay = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this._chartPieQualityNumber = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this._chartPieCountNumber = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this._selectDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this._dgvStensils)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._chartPieMarkup)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._chartPieTimesDay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._chartPieQualityNumber)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._chartPieCountNumber)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _dgvStensils
			// 
			this._dgvStensils.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dgvStensils.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dgvStensils.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Stensil,
            this.Number});
			this._dgvStensils.Location = new System.Drawing.Point(827, 78);
			this._dgvStensils.Name = "_dgvStensils";
			this._dgvStensils.Size = new System.Drawing.Size(274, 486);
			this._dgvStensils.TabIndex = 3;
			// 
			// Stensil
			// 
			this.Stensil.HeaderText = "Stencils";
			this.Stensil.Name = "Stensil";
			this.Stensil.Width = 150;
			// 
			// Number
			// 
			this.Number.HeaderText = "Amount";
			this.Number.Name = "Number";
			this.Number.Width = 80;
			// 
			// _cmbCountry
			// 
			this._cmbCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbCountry.FormattingEnabled = true;
			this._cmbCountry.Location = new System.Drawing.Point(827, 51);
			this._cmbCountry.Name = "_cmbCountry";
			this._cmbCountry.Size = new System.Drawing.Size(274, 21);
			this._cmbCountry.TabIndex = 4;
			this._cmbCountry.SelectedIndexChanged += new System.EventHandler(this._cmbCountry_SelectedIndexChanged);
			// 
			// _chartPieMarkup
			// 
			chartArea5.Name = "AreaMarkup";
			this._chartPieMarkup.ChartAreas.Add(chartArea5);
			legend5.Name = "Legend1";
			this._chartPieMarkup.Legends.Add(legend5);
			this._chartPieMarkup.Location = new System.Drawing.Point(11, 51);
			this._chartPieMarkup.Name = "_chartPieMarkup";
			series5.ChartArea = "AreaMarkup";
			series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
			series5.Legend = "Legend1";
			series5.Name = "Series1";
			this._chartPieMarkup.Series.Add(series5);
			this._chartPieMarkup.Size = new System.Drawing.Size(267, 268);
			this._chartPieMarkup.TabIndex = 21;
			this._chartPieMarkup.Text = "chartPieMarkup";
			title5.Name = "Title1";
			title5.Text = "Statistics on the markup";
			this._chartPieMarkup.Titles.Add(title5);
			// 
			// _chartPieTimesDay
			// 
			chartArea6.Name = "ChartArea1";
			this._chartPieTimesDay.ChartAreas.Add(chartArea6);
			legend6.Name = "Legend1";
			this._chartPieTimesDay.Legends.Add(legend6);
			this._chartPieTimesDay.Location = new System.Drawing.Point(284, 51);
			this._chartPieTimesDay.Name = "_chartPieTimesDay";
			series6.ChartArea = "ChartArea1";
			series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
			series6.Legend = "Legend1";
			series6.Name = "Series1";
			this._chartPieTimesDay.Series.Add(series6);
			this._chartPieTimesDay.Size = new System.Drawing.Size(267, 268);
			this._chartPieTimesDay.TabIndex = 22;
			this._chartPieTimesDay.Text = "chartPieTimesDay";
			title6.Name = "Title1";
			title6.Text = "Time of Day Statistics";
			this._chartPieTimesDay.Titles.Add(title6);
			// 
			// _chartPieQualityNumber
			// 
			chartArea7.Name = "ChartArea1";
			this._chartPieQualityNumber.ChartAreas.Add(chartArea7);
			legend7.Name = "Legend1";
			this._chartPieQualityNumber.Legends.Add(legend7);
			this._chartPieQualityNumber.Location = new System.Drawing.Point(557, 51);
			this._chartPieQualityNumber.Name = "_chartPieQualityNumber";
			series7.ChartArea = "ChartArea1";
			series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
			series7.Legend = "Legend1";
			series7.Name = "Series1";
			series7.YValuesPerPoint = 4;
			this._chartPieQualityNumber.Series.Add(series7);
			this._chartPieQualityNumber.Size = new System.Drawing.Size(267, 268);
			this._chartPieQualityNumber.TabIndex = 23;
			this._chartPieQualityNumber.Text = "chartPieQualityNumber";
			title7.Name = "Title1";
			title7.Text = "PlateCoordinates quality statistics";
			this._chartPieQualityNumber.Titles.Add(title7);
			// 
			// _chartPieCountNumber
			// 
			chartArea8.Name = "ChartArea1";
			this._chartPieCountNumber.ChartAreas.Add(chartArea8);
			legend8.Name = "Legend1";
			this._chartPieCountNumber.Legends.Add(legend8);
			this._chartPieCountNumber.Location = new System.Drawing.Point(11, 334);
			this._chartPieCountNumber.Name = "_chartPieCountNumber";
			series8.ChartArea = "ChartArea1";
			series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
			series8.Legend = "Legend1";
			series8.Name = "Series1";
			this._chartPieCountNumber.Series.Add(series8);
			this._chartPieCountNumber.Size = new System.Drawing.Size(267, 239);
			this._chartPieCountNumber.TabIndex = 24;
			this._chartPieCountNumber.Text = "chartPieCountNumber";
			title8.Name = "Title1";
			title8.Text = "Statistics of the number of plates in the image";
			this._chartPieCountNumber.Titles.Add(title8);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectDirectoryToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1105, 24);
			this.menuStrip1.TabIndex = 25;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// _selectDirectoryToolStripMenuItem
			// 
			this._selectDirectoryToolStripMenuItem.Name = "_selectDirectoryToolStripMenuItem";
			this._selectDirectoryToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
			this._selectDirectoryToolStripMenuItem.Text = "Select directory";
			this._selectDirectoryToolStripMenuItem.Click += new System.EventHandler(this._selectDirectoryToolStripMenuItem_Click);
			// 
			// Statistics
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1105, 576);
			this.Controls.Add(this._chartPieCountNumber);
			this.Controls.Add(this._chartPieQualityNumber);
			this.Controls.Add(this._chartPieTimesDay);
			this.Controls.Add(this._chartPieMarkup);
			this.Controls.Add(this._cmbCountry);
			this.Controls.Add(this._dgvStensils);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(664, 300);
			this.Name = "Statistics";
			this.Text = "Statistics";
			this.Load += new System.EventHandler(this.Statistics_Load);
			((System.ComponentModel.ISupportInitialize)(this._dgvStensils)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._chartPieMarkup)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._chartPieTimesDay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._chartPieQualityNumber)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._chartPieCountNumber)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.DataGridView _dgvStensils;
		private System.Windows.Forms.ComboBox _cmbCountry;
		private System.Windows.Forms.DataVisualization.Charting.Chart _chartPieMarkup;
		private System.Windows.Forms.DataVisualization.Charting.Chart _chartPieTimesDay;
		private System.Windows.Forms.DataVisualization.Charting.Chart _chartPieQualityNumber;
		private System.Windows.Forms.DataVisualization.Charting.Chart _chartPieCountNumber;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem _selectDirectoryToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn Stensil;
		private System.Windows.Forms.DataGridViewTextBoxColumn Number;
	}
}