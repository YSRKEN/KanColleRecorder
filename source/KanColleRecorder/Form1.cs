using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KanColleRecorder {
	public partial class MainForm : Form {
		// 艦これのFlashにおける左上座標、および縦横幅
		int flashWidth = 800, flashHeight = 480;
		int flashPositionX = 0, flashPositionY = 0;

		public MainForm() {
			InitializeComponent();
		}

		// 「座標取得」ボタンを押した際の処理
		private void GetFlashPositionButton_Click(object sender, EventArgs e) {
			// 艦これのFlashにおける左上座標、および縦横幅を決定する
			// 便宜上決め打ちしているが、本来は計算で算出する
			flashPositionX = 374;
			flashPositionY = 127;
			flashWidth = 800;
			flashHeight = 480;
			// 決定した左上座標をボタン上に表示する
			GetFlashPositionButton.Text = "(" + flashPositionX.ToString() + "," + flashPositionY.ToString() + ")";
		}

		// 「画像保存」ボタンを押した際の処理
		private void SaveScreenShotButton_Click(object sender, EventArgs e) {
			// 保存用のBitmapを確保し、そこにディスプレイの画像をコピーする
			// (この手法だと他ウィンドウの映り込みは回避できない)
			var bmp = new Bitmap(flashWidth, flashHeight);
			var g = Graphics.FromImage(bmp);
			g.CopyFromScreen(new Point(flashPositionX, flashPositionY), new Point(0, 0), bmp.Size);
			g.Dispose();
			// Bitmapを保存する
			bmp.Save("sample.png");
		}
	}
}
