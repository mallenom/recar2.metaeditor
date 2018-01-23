using System;
using System.Drawing;
using System.Windows.Forms;

using Recar2.MetaEditor.Properties;

using Mallenom.Widgets;

namespace Recar2.MetaEditor.UI
{
	public sealed class ButtonsWidget : Widget
	{
		private static readonly Image ImgPrevArrow = Resources.ImgPrevArrow;
		private static readonly Image ImgNextArrow = Resources.ImgNextArrow;

		private static readonly int ButtonHeight = 48;
		private static readonly int ButtonWidth  = 48;

		public event EventHandler PrevButtonClick
		{
			add => PrevButton.Click += value;
			remove => PrevButton.Click -= value;
		}
		public event EventHandler NextButtonClick
		{
			add => NextButton.Click += value;
			remove => NextButton.Click -= value;
		}
		
		public Widget PrevButton { get; }

		public Widget NextButton { get; }

		public ButtonsWidget()
		{
			Layout = new WidgetLayout
			{
				Dock   = DockStyle.Fill,
				Margin = new Padding(0),
			};

			Children.Add(PrevButton = CreateButton(ImgPrevArrow, new Size(ButtonWidth, ButtonHeight)));
			Children.Add(NextButton = CreateButton(ImgNextArrow, new Size(ButtonWidth, ButtonHeight)));
		}

		private static Widget CreateButton(Image icon, Size size)
		{
			return new ButtonWidget()
			{
				Renderer = new ButtonRenderer(),
				ImageMask = icon,
				Cursor = Cursors.Hand,
				Layout = new WidgetLayout { Margin = new Padding(0) },
				Size = size,
			};
		}

		private static int CalculateY(int h1, int h2, int top) => (h1 - h2) / 2 + top;

		protected override void OnSizeChanged(EventArgs e)
		{
			const int x1 = 0;
			var y1 = CalculateY(Height, ButtonHeight, ClientRectangle.Top);
			PrevButton.Location = new Point(x1, y1);

			var x2 = ClientRectangle.Right - ButtonWidth;
			var y2 = y1;

			NextButton.Location = new Point(x2, y2);

			base.OnSizeChanged(e);
		}
	}
}
