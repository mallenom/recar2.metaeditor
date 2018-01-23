using Recar2.MetaEditor.UI;

namespace Recar2.MetaEditor
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this._btnStatistics = new System.Windows.Forms.Button();
			this._log = new Mallenom.Diagnostics.Logs.LogView();
			this._txtPlate = new System.Windows.Forms.TextBox();
			this._txtDescription = new System.Windows.Forms.TextBox();
			this._lbxPlates = new System.Windows.Forms.ListBox();
			this._grpBrightness = new System.Windows.Forms.GroupBox();
			this._rdiDark = new System.Windows.Forms.RadioButton();
			this._rdiLight = new System.Windows.Forms.RadioButton();
			this._ctrStencil = new Recar2.Controls.PlateControl();
			this._cmbCountries = new System.Windows.Forms.ComboBox();
			this._grpQuality = new System.Windows.Forms.GroupBox();
			this._rdiLow = new System.Windows.Forms.RadioButton();
			this._rdiNormal = new System.Windows.Forms.RadioButton();
			this._rdiHigh = new System.Windows.Forms.RadioButton();
			this._cmbStencils = new System.Windows.Forms.ComboBox();
			this._trvImages = new System.Windows.Forms.TreeView();
			this._txtImagesDirectory = new System.Windows.Forms.LinkLabel();
			this._sptImage = new System.Windows.Forms.SplitContainer();
			this._sptTree = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this._chkImportant = new System.Windows.Forms.CheckBox();
			this._txtTreeFilter = new System.Windows.Forms.TextBox();
			this._btnApplyFilter = new System.Windows.Forms.Button();
			this._grpDirectoryStatistics = new System.Windows.Forms.GroupBox();
			this._lnkRefreshInfo = new System.Windows.Forms.LinkLabel();
			this._txtImageInfo = new System.Windows.Forms.RichTextBox();
			this._txtDirectoryInfo = new System.Windows.Forms.RichTextBox();
			this._btnRemovePlate = new System.Windows.Forms.Button();
			this._btnAddPlate = new System.Windows.Forms.Button();
			this._btnSaveMetadata = new System.Windows.Forms.Button();
			this._btnSaveMetadataAndNext = new System.Windows.Forms.Button();
			this._cmbImagesDirectory = new System.Windows.Forms.ComboBox();
			this._image = new Recar2.MetaEditor.UI.ImageControl();
			this._imgPlate = new Mallenom.Imaging.Image();
			this._cntMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copy = new System.Windows.Forms.ToolStripMenuItem();
			this.paste = new System.Windows.Forms.ToolStripMenuItem();
			this.cut = new System.Windows.Forms.ToolStripMenuItem();
			this._menu = new System.Windows.Forms.MenuStrip();
			this._statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._linksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._newVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._redmineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeystoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyTreeSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.focusLightingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyPlateList = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyQuality = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyCountries = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyStencils = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyEntryField = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyAddPlate = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyDeletePlate = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeySave = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyEditingPlate = new System.Windows.Forms.ToolStripMenuItem();
			this._hotKeyClose = new System.Windows.Forms.ToolStripMenuItem();
			this._rdiMarkup = new System.Windows.Forms.RadioButton();
			this._rdiAlgorithmResult = new System.Windows.Forms.RadioButton();
			this._grpBrightness.SuspendLayout();
			this._grpQuality.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._sptImage)).BeginInit();
			this._sptImage.Panel1.SuspendLayout();
			this._sptImage.Panel2.SuspendLayout();
			this._sptImage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._sptTree)).BeginInit();
			this._sptTree.Panel1.SuspendLayout();
			this._sptTree.Panel2.SuspendLayout();
			this._sptTree.SuspendLayout();
			this._grpDirectoryStatistics.SuspendLayout();
			this._cntMenuStrip.SuspendLayout();
			this._menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// _btnStatistics
			// 
			this._btnStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnStatistics.Location = new System.Drawing.Point(1293, 2);
			this._btnStatistics.Name = "_btnStatistics";
			this._btnStatistics.Size = new System.Drawing.Size(112, 21);
			this._btnStatistics.TabIndex = 31;
			this._btnStatistics.Text = "Statistics";
			this._btnStatistics.UseVisualStyleBackColor = true;
			this._btnStatistics.Click += new System.EventHandler(this._btnStatistics_Click);
			// 
			// _log
			// 
			this._log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._log.AppenderName = "root";
			this._log.Location = new System.Drawing.Point(3, 642);
			this._log.Name = "_log";
			this._log.Size = new System.Drawing.Size(534, 24);
			this._log.TabIndex = 32;
			// 
			// _txtPlate
			// 
			this._txtPlate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtPlate.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this._txtPlate.Location = new System.Drawing.Point(3, 479);
			this._txtPlate.Name = "_txtPlate";
			this._txtPlate.Size = new System.Drawing.Size(394, 64);
			this._txtPlate.TabIndex = 12;
			this._txtPlate.Text = "0123456789";
			this._txtPlate.TextChanged += new System.EventHandler(this._txtPlate_TextChanged);
			this._txtPlate.KeyDown += new System.Windows.Forms.KeyEventHandler(this._txtPlate_KeyDown);
			// 
			// _txtDescription
			// 
			this._txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtDescription.Location = new System.Drawing.Point(3, 277);
			this._txtDescription.Multiline = true;
			this._txtDescription.Name = "_txtDescription";
			this._txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._txtDescription.Size = new System.Drawing.Size(422, 53);
			this._txtDescription.TabIndex = 6;
			this._txtDescription.TextChanged += new System.EventHandler(this.OnParameterChanged);
			this._txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this._txtDescription_KeyDown);
			// 
			// _lbxPlates
			// 
			this._lbxPlates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._lbxPlates.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this._lbxPlates.FormattingEnabled = true;
			this._lbxPlates.ItemHeight = 24;
			this._lbxPlates.Location = new System.Drawing.Point(3, 334);
			this._lbxPlates.Name = "_lbxPlates";
			this._lbxPlates.Size = new System.Drawing.Size(422, 76);
			this._lbxPlates.TabIndex = 8;
			this._lbxPlates.SelectedIndexChanged += new System.EventHandler(this._lbxPlates_SelectedIndexChanged);
			// 
			// _grpBrightness
			// 
			this._grpBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._grpBrightness.Controls.Add(this._rdiDark);
			this._grpBrightness.Controls.Add(this._rdiLight);
			this._grpBrightness.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._grpBrightness.Location = new System.Drawing.Point(432, 277);
			this._grpBrightness.Name = "_grpBrightness";
			this._grpBrightness.Size = new System.Drawing.Size(108, 53);
			this._grpBrightness.TabIndex = 7;
			this._grpBrightness.TabStop = false;
			this._grpBrightness.Text = "Lighting";
			// 
			// _rdiDark
			// 
			this._rdiDark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._rdiDark.AutoSize = true;
			this._rdiDark.Location = new System.Drawing.Point(7, 32);
			this._rdiDark.Name = "_rdiDark";
			this._rdiDark.Size = new System.Drawing.Size(49, 19);
			this._rdiDark.TabIndex = 1;
			this._rdiDark.Text = "Dark";
			this._rdiDark.UseVisualStyleBackColor = true;
			this._rdiDark.CheckedChanged += new System.EventHandler(this.OnParameterChanged);
			// 
			// _rdiLight
			// 
			this._rdiLight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._rdiLight.AutoSize = true;
			this._rdiLight.Checked = true;
			this._rdiLight.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._rdiLight.Location = new System.Drawing.Point(7, 15);
			this._rdiLight.Name = "_rdiLight";
			this._rdiLight.Size = new System.Drawing.Size(58, 20);
			this._rdiLight.TabIndex = 0;
			this._rdiLight.TabStop = true;
			this._rdiLight.Text = "Light";
			this._rdiLight.UseVisualStyleBackColor = true;
			this._rdiLight.CheckedChanged += new System.EventHandler(this.OnParameterChanged);
			// 
			// _ctrStencil
			// 
			this._ctrStencil.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._ctrStencil.IsAutoResolveStencil = false;
			this._ctrStencil.Location = new System.Drawing.Point(3, 549);
			this._ctrStencil.Name = "_ctrStencil";
			this._ctrStencil.Size = new System.Drawing.Size(394, 93);
			this._ctrStencil.TabIndex = 15;
			this._ctrStencil.TabStop = false;
			this._ctrStencil.Text = null;
			// 
			// _cmbCountries
			// 
			this._cmbCountries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._cmbCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbCountries.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._cmbCountries.FormattingEnabled = true;
			this._cmbCountries.Location = new System.Drawing.Point(3, 418);
			this._cmbCountries.Name = "_cmbCountries";
			this._cmbCountries.Size = new System.Drawing.Size(534, 23);
			this._cmbCountries.TabIndex = 10;
			this._cmbCountries.SelectedIndexChanged += new System.EventHandler(this._cmbCountries_SelectedIndexChanged);
			// 
			// _grpQuality
			// 
			this._grpQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._grpQuality.Controls.Add(this._rdiLow);
			this._grpQuality.Controls.Add(this._rdiNormal);
			this._grpQuality.Controls.Add(this._rdiHigh);
			this._grpQuality.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._grpQuality.Location = new System.Drawing.Point(432, 338);
			this._grpQuality.Name = "_grpQuality";
			this._grpQuality.Size = new System.Drawing.Size(105, 72);
			this._grpQuality.TabIndex = 9;
			this._grpQuality.TabStop = false;
			this._grpQuality.Text = "Plate quality";
			// 
			// _rdiLow
			// 
			this._rdiLow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._rdiLow.AutoSize = true;
			this._rdiLow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._rdiLow.Location = new System.Drawing.Point(7, 49);
			this._rdiLow.Name = "_rdiLow";
			this._rdiLow.Size = new System.Drawing.Size(53, 20);
			this._rdiLow.TabIndex = 2;
			this._rdiLow.Text = "Low";
			this._rdiLow.UseVisualStyleBackColor = true;
			this._rdiLow.CheckedChanged += new System.EventHandler(this._rdiLow_CheckedChanged);
			// 
			// _rdiNormal
			// 
			this._rdiNormal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._rdiNormal.AutoSize = true;
			this._rdiNormal.Checked = true;
			this._rdiNormal.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._rdiNormal.Location = new System.Drawing.Point(7, 32);
			this._rdiNormal.Name = "_rdiNormal";
			this._rdiNormal.Size = new System.Drawing.Size(71, 20);
			this._rdiNormal.TabIndex = 1;
			this._rdiNormal.TabStop = true;
			this._rdiNormal.Text = "Normal";
			this._rdiNormal.UseVisualStyleBackColor = true;
			this._rdiNormal.CheckedChanged += new System.EventHandler(this._rdiNormal_CheckedChanged);
			// 
			// _rdiHigh
			// 
			this._rdiHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._rdiHigh.AutoSize = true;
			this._rdiHigh.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._rdiHigh.Location = new System.Drawing.Point(7, 15);
			this._rdiHigh.Name = "_rdiHigh";
			this._rdiHigh.Size = new System.Drawing.Size(57, 20);
			this._rdiHigh.TabIndex = 0;
			this._rdiHigh.Text = "High";
			this._rdiHigh.UseVisualStyleBackColor = true;
			this._rdiHigh.CheckedChanged += new System.EventHandler(this._rdiHigh_CheckedChanged);
			// 
			// _cmbStencils
			// 
			this._cmbStencils.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._cmbStencils.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbStencils.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._cmbStencils.FormattingEnabled = true;
			this._cmbStencils.Location = new System.Drawing.Point(3, 447);
			this._cmbStencils.Name = "_cmbStencils";
			this._cmbStencils.Size = new System.Drawing.Size(534, 23);
			this._cmbStencils.TabIndex = 11;
			this._cmbStencils.SelectedIndexChanged += new System.EventHandler(this._cmbStencil_SelectedIndexChanged);
			// 
			// _trvImages
			// 
			this._trvImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._trvImages.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this._trvImages.HideSelection = false;
			this._trvImages.Location = new System.Drawing.Point(0, 36);
			this._trvImages.Name = "_trvImages";
			this._trvImages.Size = new System.Drawing.Size(267, 628);
			this._trvImages.TabIndex = 5;
			// 
			// _txtImagesDirectory
			// 
			this._txtImagesDirectory.Location = new System.Drawing.Point(1187, 0);
			this._txtImagesDirectory.Name = "_txtImagesDirectory";
			this._txtImagesDirectory.Size = new System.Drawing.Size(100, 23);
			this._txtImagesDirectory.TabIndex = 55;
			// 
			// _sptImage
			// 
			this._sptImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this._sptImage.Location = new System.Drawing.Point(0, 24);
			this._sptImage.Name = "_sptImage";
			// 
			// _sptImage.Panel1
			// 
			this._sptImage.Panel1.Controls.Add(this._sptTree);
			this._sptImage.Panel1.Controls.Add(this._cmbImagesDirectory);
			// 
			// _sptImage.Panel2
			// 
			this._sptImage.Panel2.Controls.Add(this._image);
			this._sptImage.Panel2.Controls.Add(this._imgPlate);
			this._sptImage.Size = new System.Drawing.Size(1407, 689);
			this._sptImage.SplitterDistance = 814;
			this._sptImage.TabIndex = 53;
			// 
			// _sptTree
			// 
			this._sptTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this._sptTree.Location = new System.Drawing.Point(0, 23);
			this._sptTree.Name = "_sptTree";
			// 
			// _sptTree.Panel1
			// 
			this._sptTree.Panel1.Controls.Add(this.label1);
			this._sptTree.Panel1.Controls.Add(this._trvImages);
			this._sptTree.Panel1.Controls.Add(this._chkImportant);
			this._sptTree.Panel1.Controls.Add(this._txtTreeFilter);
			this._sptTree.Panel1.Controls.Add(this._btnApplyFilter);
			// 
			// _sptTree.Panel2
			// 
			this._sptTree.Panel2.Controls.Add(this._grpDirectoryStatistics);
			this._sptTree.Panel2.Controls.Add(this._txtDescription);
			this._sptTree.Panel2.Controls.Add(this._log);
			this._sptTree.Panel2.Controls.Add(this._btnRemovePlate);
			this._sptTree.Panel2.Controls.Add(this._txtPlate);
			this._sptTree.Panel2.Controls.Add(this._btnAddPlate);
			this._sptTree.Panel2.Controls.Add(this._btnSaveMetadata);
			this._sptTree.Panel2.Controls.Add(this._cmbCountries);
			this._sptTree.Panel2.Controls.Add(this._ctrStencil);
			this._sptTree.Panel2.Controls.Add(this._grpQuality);
			this._sptTree.Panel2.Controls.Add(this._cmbStencils);
			this._sptTree.Panel2.Controls.Add(this._lbxPlates);
			this._sptTree.Panel2.Controls.Add(this._grpBrightness);
			this._sptTree.Panel2.Controls.Add(this._btnSaveMetadataAndNext);
			this._sptTree.Size = new System.Drawing.Size(814, 666);
			this._sptTree.SplitterDistance = 270;
			this._sptTree.TabIndex = 33;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(13, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Filter by plate";
			// 
			// _chkImportant
			// 
			this._chkImportant.AutoSize = true;
			this._chkImportant.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._chkImportant.Location = new System.Drawing.Point(276, 8);
			this._chkImportant.Name = "_chkImportant";
			this._chkImportant.Size = new System.Drawing.Size(113, 20);
			this._chkImportant.TabIndex = 4;
			this._chkImportant.Text = "Only important";
			this._chkImportant.UseVisualStyleBackColor = true;
			this._chkImportant.CheckedChanged += new System.EventHandler(this._chkImportant_CheckedChanged);
			// 
			// _txtTreeFilter
			// 
			this._txtTreeFilter.Location = new System.Drawing.Point(97, 7);
			this._txtTreeFilter.Name = "_txtTreeFilter";
			this._txtTreeFilter.Size = new System.Drawing.Size(85, 23);
			this._txtTreeFilter.TabIndex = 2;
			// 
			// _btnApplyFilter
			// 
			this._btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._btnApplyFilter.Location = new System.Drawing.Point(188, 6);
			this._btnApplyFilter.Name = "_btnApplyFilter";
			this._btnApplyFilter.Size = new System.Drawing.Size(82, 23);
			this._btnApplyFilter.TabIndex = 3;
			this._btnApplyFilter.Text = "Done";
			this._btnApplyFilter.UseVisualStyleBackColor = true;
			this._btnApplyFilter.Click += new System.EventHandler(this._btnApplyFilter_Click);
			// 
			// _grpDirectoryStatistics
			// 
			this._grpDirectoryStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._grpDirectoryStatistics.Controls.Add(this._lnkRefreshInfo);
			this._grpDirectoryStatistics.Controls.Add(this._txtImageInfo);
			this._grpDirectoryStatistics.Controls.Add(this._txtDirectoryInfo);
			this._grpDirectoryStatistics.Location = new System.Drawing.Point(3, 3);
			this._grpDirectoryStatistics.Name = "_grpDirectoryStatistics";
			this._grpDirectoryStatistics.Size = new System.Drawing.Size(534, 268);
			this._grpDirectoryStatistics.TabIndex = 33;
			this._grpDirectoryStatistics.TabStop = false;
			this._grpDirectoryStatistics.Text = "Info";
			// 
			// _lnkRefreshInfo
			// 
			this._lnkRefreshInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._lnkRefreshInfo.AutoSize = true;
			this._lnkRefreshInfo.LinkColor = System.Drawing.Color.Blue;
			this._lnkRefreshInfo.Location = new System.Drawing.Point(482, 183);
			this._lnkRefreshInfo.Name = "_lnkRefreshInfo";
			this._lnkRefreshInfo.Size = new System.Drawing.Size(46, 15);
			this._lnkRefreshInfo.TabIndex = 1;
			this._lnkRefreshInfo.TabStop = true;
			this._lnkRefreshInfo.Text = "Refresh";
			this._lnkRefreshInfo.VisitedLinkColor = System.Drawing.Color.Blue;
			this._lnkRefreshInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkRefreshInfo_LinkClicked);
			// 
			// _txtImageInfo
			// 
			this._txtImageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtImageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtImageInfo.Location = new System.Drawing.Point(3, 201);
			this._txtImageInfo.Name = "_txtImageInfo";
			this._txtImageInfo.ReadOnly = true;
			this._txtImageInfo.Size = new System.Drawing.Size(528, 61);
			this._txtImageInfo.TabIndex = 2;
			this._txtImageInfo.Text = "";
			// 
			// _txtDirectoryInfo
			// 
			this._txtDirectoryInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtDirectoryInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtDirectoryInfo.Location = new System.Drawing.Point(3, 19);
			this._txtDirectoryInfo.Name = "_txtDirectoryInfo";
			this._txtDirectoryInfo.ReadOnly = true;
			this._txtDirectoryInfo.Size = new System.Drawing.Size(528, 183);
			this._txtDirectoryInfo.TabIndex = 0;
			this._txtDirectoryInfo.Text = "";
			// 
			// _btnRemovePlate
			// 
			this._btnRemovePlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnRemovePlate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_btnRemovePlate.BackgroundImage")));
			this._btnRemovePlate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this._btnRemovePlate.Location = new System.Drawing.Point(473, 479);
			this._btnRemovePlate.Name = "_btnRemovePlate";
			this._btnRemovePlate.Size = new System.Drawing.Size(64, 64);
			this._btnRemovePlate.TabIndex = 14;
			this._btnRemovePlate.UseVisualStyleBackColor = true;
			this._btnRemovePlate.Click += new System.EventHandler(this._btnRemovePlate_Click);
			// 
			// _btnAddPlate
			// 
			this._btnAddPlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnAddPlate.BackgroundImage = global::Recar2.MetaEditor.Properties.Resources.addIcon;
			this._btnAddPlate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this._btnAddPlate.Location = new System.Drawing.Point(403, 479);
			this._btnAddPlate.Name = "_btnAddPlate";
			this._btnAddPlate.Size = new System.Drawing.Size(64, 64);
			this._btnAddPlate.TabIndex = 13;
			this._btnAddPlate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this._btnAddPlate.UseVisualStyleBackColor = true;
			this._btnAddPlate.Click += new System.EventHandler(this._btnAddPlate_Click);
			// 
			// _btnSaveMetadata
			// 
			this._btnSaveMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnSaveMetadata.BackgroundImage = global::Recar2.MetaEditor.Properties.Resources.save;
			this._btnSaveMetadata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this._btnSaveMetadata.Location = new System.Drawing.Point(403, 570);
			this._btnSaveMetadata.Name = "_btnSaveMetadata";
			this._btnSaveMetadata.Size = new System.Drawing.Size(64, 64);
			this._btnSaveMetadata.TabIndex = 16;
			this._btnSaveMetadata.UseVisualStyleBackColor = true;
			this._btnSaveMetadata.Click += new System.EventHandler(this._btnSaveMetadata_Click);
			// 
			// _btnSaveMetadataAndNext
			// 
			this._btnSaveMetadataAndNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnSaveMetadataAndNext.BackgroundImage = global::Recar2.MetaEditor.Properties.Resources.save_next;
			this._btnSaveMetadataAndNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this._btnSaveMetadataAndNext.Location = new System.Drawing.Point(473, 570);
			this._btnSaveMetadataAndNext.Name = "_btnSaveMetadataAndNext";
			this._btnSaveMetadataAndNext.Size = new System.Drawing.Size(64, 64);
			this._btnSaveMetadataAndNext.TabIndex = 17;
			this._btnSaveMetadataAndNext.UseVisualStyleBackColor = true;
			this._btnSaveMetadataAndNext.Click += new System.EventHandler(this._btnSaveMetadataAndNext_Click);
			// 
			// _cmbImagesDirectory
			// 
			this._cmbImagesDirectory.Dock = System.Windows.Forms.DockStyle.Top;
			this._cmbImagesDirectory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbImagesDirectory.FormattingEnabled = true;
			this._cmbImagesDirectory.Location = new System.Drawing.Point(0, 0);
			this._cmbImagesDirectory.Name = "_cmbImagesDirectory";
			this._cmbImagesDirectory.Size = new System.Drawing.Size(814, 23);
			this._cmbImagesDirectory.TabIndex = 34;
			this._cmbImagesDirectory.SelectedIndexChanged += new System.EventHandler(this._cmbImagesDirectory_SelectedIndexChanged);
			// 
			// _image
			// 
			this._image.Dock = System.Windows.Forms.DockStyle.Fill;
			this._image.IsPlateVisible = false;
			this._image.Location = new System.Drawing.Point(0, 195);
			this._image.Margin = new System.Windows.Forms.Padding(48, 22, 48, 22);
			this._image.Name = "_image";
			this._image.Size = new System.Drawing.Size(589, 494);
			this._image.TabIndex = 5;
			// 
			// _imgPlate
			// 
			this._imgPlate.BackColor = System.Drawing.Color.Tomato;
			this._imgPlate.DefaultMatrixType = Mallenom.Imaging.MatrixType.Matrix;
			this._imgPlate.Dock = System.Windows.Forms.DockStyle.Top;
			this._imgPlate.DoubleBuffer = true;
			this._imgPlate.FooterColor = System.Drawing.Color.Yellow;
			this._imgPlate.FooterFont = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this._imgPlate.Location = new System.Drawing.Point(0, 0);
			this._imgPlate.Name = "_imgPlate";
			this._imgPlate.Size = new System.Drawing.Size(589, 195);
			this._imgPlate.SizeMode = Mallenom.Imaging.ImageSizeMode.Zoom;
			this._imgPlate.TabIndex = 39;
			// 
			// _cntMenuStrip
			// 
			this._cntMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copy,
            this.paste,
            this.cut});
			this._cntMenuStrip.Name = "contextMenuStrip1";
			this._cntMenuStrip.Size = new System.Drawing.Size(140, 70);
			// 
			// copy
			// 
			this.copy.Name = "copy";
			this.copy.Size = new System.Drawing.Size(139, 22);
			this.copy.Text = "Копировать";
			this.copy.Click += new System.EventHandler(this._copy_Click);
			// 
			// paste
			// 
			this.paste.Name = "paste";
			this.paste.Size = new System.Drawing.Size(139, 22);
			this.paste.Text = "Вставить";
			this.paste.Click += new System.EventHandler(this._paste_Click);
			// 
			// cut
			// 
			this.cut.Name = "cut";
			this.cut.Size = new System.Drawing.Size(139, 22);
			this.cut.Text = "Вырезать";
			this.cut.Click += new System.EventHandler(this._cut_Click);
			// 
			// _menu
			// 
			this._menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statisticsToolStripMenuItem,
            this._linksToolStripMenuItem,
            this._hotKeystoolStripMenuItem});
			this._menu.Location = new System.Drawing.Point(0, 0);
			this._menu.Name = "_menu";
			this._menu.Size = new System.Drawing.Size(1407, 24);
			this._menu.TabIndex = 0;
			this._menu.Text = "menuStrip1";
			// 
			// _statisticsToolStripMenuItem
			// 
			this._statisticsToolStripMenuItem.Name = "_statisticsToolStripMenuItem";
			this._statisticsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
			this._statisticsToolStripMenuItem.Text = "Statistics";
			this._statisticsToolStripMenuItem.Click += new System.EventHandler(this._statisticsToolStripMenuItem_Click);
			// 
			// _linksToolStripMenuItem
			// 
			this._linksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newVersionToolStripMenuItem,
            this._redmineToolStripMenuItem});
			this._linksToolStripMenuItem.Name = "_linksToolStripMenuItem";
			this._linksToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this._linksToolStripMenuItem.Text = "Links";
			// 
			// _newVersionToolStripMenuItem
			// 
			this._newVersionToolStripMenuItem.Name = "_newVersionToolStripMenuItem";
			this._newVersionToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this._newVersionToolStripMenuItem.Text = "New version";
			this._newVersionToolStripMenuItem.Click += new System.EventHandler(this._newVersionToolStripMenuItem_Click);
			// 
			// _redmineToolStripMenuItem
			// 
			this._redmineToolStripMenuItem.Name = "_redmineToolStripMenuItem";
			this._redmineToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this._redmineToolStripMenuItem.Text = "Redmine";
			this._redmineToolStripMenuItem.Click += new System.EventHandler(this._redmineToolStripMenuItem_Click);
			// 
			// _hotKeystoolStripMenuItem
			// 
			this._hotKeystoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._hotKeyTreeSelect,
            this.focusLightingToolStripMenuItem,
            this._hotKeyPlateList,
            this._hotKeyQuality,
            this._hotKeyCountries,
            this._hotKeyStencils,
            this._hotKeyEntryField,
            this._hotKeyAddPlate,
            this._hotKeyDeletePlate,
            this._hotKeySave,
            this._hotKeyEditingPlate,
            this._hotKeyClose});
			this._hotKeystoolStripMenuItem.Name = "_hotKeystoolStripMenuItem";
			this._hotKeystoolStripMenuItem.Size = new System.Drawing.Size(100, 20);
			this._hotKeystoolStripMenuItem.Text = "Hot Keys (Beta)";
			// 
			// _hotKeyTreeSelect
			// 
			this._hotKeyTreeSelect.Name = "_hotKeyTreeSelect";
			this._hotKeyTreeSelect.Size = new System.Drawing.Size(220, 22);
			this._hotKeyTreeSelect.Text = "Focus tree (Ctrl+T)";
			// 
			// focusLightingToolStripMenuItem
			// 
			this.focusLightingToolStripMenuItem.Name = "focusLightingToolStripMenuItem";
			this.focusLightingToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.focusLightingToolStripMenuItem.Text = "Focus lighting (Ctrl+L)";
			// 
			// _hotKeyPlateList
			// 
			this._hotKeyPlateList.Name = "_hotKeyPlateList";
			this._hotKeyPlateList.Size = new System.Drawing.Size(220, 22);
			this._hotKeyPlateList.Text = "Focus plate list (Ctrl+N)";
			// 
			// _hotKeyQuality
			// 
			this._hotKeyQuality.Name = "_hotKeyQuality";
			this._hotKeyQuality.Size = new System.Drawing.Size(220, 22);
			this._hotKeyQuality.Text = "Focus plate quality (Ctrl+Q)";
			// 
			// _hotKeyCountries
			// 
			this._hotKeyCountries.Name = "_hotKeyCountries";
			this._hotKeyCountries.Size = new System.Drawing.Size(220, 22);
			this._hotKeyCountries.Text = "Focus coutries (Ctrl+R)";
			// 
			// _hotKeyStencils
			// 
			this._hotKeyStencils.Name = "_hotKeyStencils";
			this._hotKeyStencils.Size = new System.Drawing.Size(220, 22);
			this._hotKeyStencils.Text = "Focus stencils (Ctrl+M)";
			// 
			// _hotKeyEntryField
			// 
			this._hotKeyEntryField.Name = "_hotKeyEntryField";
			this._hotKeyEntryField.Size = new System.Drawing.Size(220, 22);
			this._hotKeyEntryField.Text = "Entry field (Ctrl+W)";
			// 
			// _hotKeyAddPlate
			// 
			this._hotKeyAddPlate.Name = "_hotKeyAddPlate";
			this._hotKeyAddPlate.Size = new System.Drawing.Size(220, 22);
			this._hotKeyAddPlate.Text = "Add plate (Ctrl+A)";
			// 
			// _hotKeyDeletePlate
			// 
			this._hotKeyDeletePlate.Name = "_hotKeyDeletePlate";
			this._hotKeyDeletePlate.Size = new System.Drawing.Size(220, 22);
			this._hotKeyDeletePlate.Text = "Delete plate (Ctrl+D)";
			// 
			// _hotKeySave
			// 
			this._hotKeySave.Name = "_hotKeySave";
			this._hotKeySave.Size = new System.Drawing.Size(220, 22);
			this._hotKeySave.Text = "Save (Ctrl+S)";
			// 
			// _hotKeyEditingPlate
			// 
			this._hotKeyEditingPlate.Name = "_hotKeyEditingPlate";
			this._hotKeyEditingPlate.Size = new System.Drawing.Size(220, 22);
			this._hotKeyEditingPlate.Text = "Editing plate (Enter)";
			// 
			// _hotKeyClose
			// 
			this._hotKeyClose.Name = "_hotKeyClose";
			this._hotKeyClose.Size = new System.Drawing.Size(220, 22);
			this._hotKeyClose.Text = "Close (Esc)";
			// 
			// _rdiMarkup
			// 
			this._rdiMarkup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._rdiMarkup.AutoSize = true;
			this._rdiMarkup.Location = new System.Drawing.Point(1006, 4);
			this._rdiMarkup.Name = "_rdiMarkup";
			this._rdiMarkup.Size = new System.Drawing.Size(66, 19);
			this._rdiMarkup.TabIndex = 56;
			this._rdiMarkup.TabStop = true;
			this._rdiMarkup.Text = "Markup";
			this._rdiMarkup.UseVisualStyleBackColor = true;
			this._rdiMarkup.CheckedChanged += new System.EventHandler(this.OnShowModeChanged);
			// 
			// _rdiAlgorithmResult
			// 
			this._rdiAlgorithmResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._rdiAlgorithmResult.AutoSize = true;
			this._rdiAlgorithmResult.Location = new System.Drawing.Point(1087, 4);
			this._rdiAlgorithmResult.Name = "_rdiAlgorithmResult";
			this._rdiAlgorithmResult.Size = new System.Drawing.Size(111, 19);
			this._rdiAlgorithmResult.TabIndex = 57;
			this._rdiAlgorithmResult.TabStop = true;
			this._rdiAlgorithmResult.Text = "Algorithm result";
			this._rdiAlgorithmResult.UseVisualStyleBackColor = true;
			this._rdiAlgorithmResult.CheckedChanged += new System.EventHandler(this.OnShowModeChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(1407, 713);
			this.Controls.Add(this._rdiAlgorithmResult);
			this.Controls.Add(this._rdiMarkup);
			this.Controls.Add(this._txtImagesDirectory);
			this.Controls.Add(this._sptImage);
			this.Controls.Add(this._btnStatistics);
			this.Controls.Add(this._menu);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainMenuStrip = this._menu;
			this.MinimumSize = new System.Drawing.Size(1043, 600);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Loading / Unloading xml document";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this._grpBrightness.ResumeLayout(false);
			this._grpBrightness.PerformLayout();
			this._grpQuality.ResumeLayout(false);
			this._grpQuality.PerformLayout();
			this._sptImage.Panel1.ResumeLayout(false);
			this._sptImage.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._sptImage)).EndInit();
			this._sptImage.ResumeLayout(false);
			this._sptTree.Panel1.ResumeLayout(false);
			this._sptTree.Panel1.PerformLayout();
			this._sptTree.Panel2.ResumeLayout(false);
			this._sptTree.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._sptTree)).EndInit();
			this._sptTree.ResumeLayout(false);
			this._grpDirectoryStatistics.ResumeLayout(false);
			this._grpDirectoryStatistics.PerformLayout();
			this._cntMenuStrip.ResumeLayout(false);
			this._menu.ResumeLayout(false);
			this._menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ImageControl _image;
		private System.Windows.Forms.Button _btnStatistics;
		private Mallenom.Diagnostics.Logs.LogView _log;
		private System.Windows.Forms.TextBox _txtPlate;
		private System.Windows.Forms.TextBox _txtDescription;
		private System.Windows.Forms.ListBox _lbxPlates;
		private System.Windows.Forms.GroupBox _grpBrightness;
		private System.Windows.Forms.RadioButton _rdiDark;
		private System.Windows.Forms.RadioButton _rdiLight;
		private Recar2.Controls.PlateControl _ctrStencil;
		private System.Windows.Forms.ComboBox _cmbCountries;
		private System.Windows.Forms.Button _btnAddPlate;
		private System.Windows.Forms.Button _btnRemovePlate;
		private System.Windows.Forms.GroupBox _grpQuality;
		private System.Windows.Forms.RadioButton _rdiLow;
		private System.Windows.Forms.RadioButton _rdiNormal;
		private System.Windows.Forms.RadioButton _rdiHigh;
		private System.Windows.Forms.ComboBox _cmbStencils;
		private System.Windows.Forms.Button _btnSaveMetadataAndNext;
		private System.Windows.Forms.TreeView _trvImages;
		private System.Windows.Forms.LinkLabel _txtImagesDirectory;
		private System.Windows.Forms.Button _btnSaveMetadata;
		private System.Windows.Forms.SplitContainer _sptImage;
		private Mallenom.Imaging.Image _imgPlate;
		private System.Windows.Forms.Button _btnApplyFilter;
		private System.Windows.Forms.TextBox _txtTreeFilter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox _chkImportant;
		private System.Windows.Forms.ContextMenuStrip _cntMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem copy;
		private System.Windows.Forms.ToolStripMenuItem paste;
		private System.Windows.Forms.ToolStripMenuItem cut;
		private System.Windows.Forms.MenuStrip _menu;
		private System.Windows.Forms.ToolStripMenuItem _linksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _newVersionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _redmineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _statisticsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _hotKeystoolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyTreeSelect;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyPlateList;
		private System.Windows.Forms.ToolStripMenuItem focusLightingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyQuality;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyCountries;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyStencils;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyEntryField;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyAddPlate;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyDeletePlate;
		private System.Windows.Forms.ToolStripMenuItem _hotKeySave;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyEditingPlate;
		private System.Windows.Forms.ToolStripMenuItem _hotKeyClose;
		private System.Windows.Forms.SplitContainer _sptTree;
		private System.Windows.Forms.GroupBox _grpDirectoryStatistics;
		private System.Windows.Forms.RichTextBox _txtDirectoryInfo;
		private System.Windows.Forms.LinkLabel _lnkRefreshInfo;
		private System.Windows.Forms.RichTextBox _txtImageInfo;
		private System.Windows.Forms.RadioButton _rdiMarkup;
		private System.Windows.Forms.RadioButton _rdiAlgorithmResult;
		private System.Windows.Forms.ComboBox _cmbImagesDirectory;
	}
}

