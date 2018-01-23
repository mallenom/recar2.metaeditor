namespace Recar2.MetaEditor.UI
{
	partial class ImageControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._image = new Mallenom.Imaging.Image();
			this.SuspendLayout();
			// 
			// _image
			// 
			this._image.Dock = System.Windows.Forms.DockStyle.Fill;
			this._image.DoubleBuffer = true;
			this._image.FooterColor = System.Drawing.Color.Tomato;
			this._image.FooterFont = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this._image.Location = new System.Drawing.Point(0, 0);
			this._image.Margin = new System.Windows.Forms.Padding(48, 22, 48, 22);
			this._image.Name = "_image";
			this._image.Size = new System.Drawing.Size(606, 453);
			this._image.SizeMode = Mallenom.Imaging.ImageSizeMode.Zoom;
			this._image.TabIndex = 0;
			// 
			// ImageControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this._image);
			this.Margin = new System.Windows.Forms.Padding(48, 22, 48, 22);
			this.Name = "ImageControl";
			this.Size = new System.Drawing.Size(606, 453);
			this.ResumeLayout(false);

		}

		#endregion

		private Mallenom.Imaging.Image _image;
	}
}
