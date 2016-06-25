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
	public partial class MainForm : Form, SelectFormCallback {
		// 艦これのFlashにおける左上座標および縦横幅
		int FlashDisplayNumber;
		Rectangle FlashRect;
		// ディスプレイの枚数分だけ用意する選択用フォーム
		List<SelectForm> form_list;

		public MainForm() {
			InitializeComponent();
		}

		// 「座標取得」ボタンを押した際の処理
		private void GetFlashPositionButton_Click(object sender, EventArgs e) {
			// 全ディスプレイの画像を取得し、生成したフォームに貼り付ける
			form_list = new List<SelectForm>();
			foreach (var screen in Screen.AllScreens) {
				var form = new SelectForm(this, screen);
				form_list.Add(form);
			}
			// 各ディスプレイ毎に表示する
			foreach(var form in form_list) {
				form.Show();
			}
			//Activate();
		/*
			// 決定した左上座標をボタン上に表示する
			GetFlashPositionButton.Text = "(" + flashPositionX.ToString() + "," + flashPositionY.ToString() + ")";
		*/
		}

		// 「画像保存」ボタンを押した際の処理
		private void SaveScreenShotButton_Click(object sender, EventArgs e) {
			/* 保存用のBitmapを確保し、そこにディスプレイの画像をコピーする
			 * ただしこの手法だと他ウィンドウの映り込みは回避できない
			 */
			var bmp = new Bitmap(FlashRect.Width, FlashRect.Height);
			var g = Graphics.FromImage(bmp);
			var form = form_list[FlashDisplayNumber];
			g.CopyFromScreen(new Point(form.Left + FlashRect.Left, form.Top + FlashRect.Top), Point.Empty, bmp.Size);
			g.Dispose();
			// Bitmapを保存する
			var dt = DateTime.Now;
			bmp.Save(dt.ToString("yyyy-MM-dd hh-mm-ss-fff") + ".png");
		}

		// 範囲選択完了時に起きるイベント
		public void SelectFinish(Rectangle Rect) {
			// 選択したディスプレイ番号・範囲をinterface経由で取得する
			for(int i = 0; i < form_list.Count; ++i) {
				var form = form_list[i];
				form.Close();
				if(form.DragedFlg) {
					FlashDisplayNumber = i;
					FlashRect = Rect;
				}
			}
			// 艦これのFlashにおける左上座標、および縦横幅を決定する
			// 決定した左上座標をボタン上に表示する
			GetFlashPositionButton.Text = "(" + FlashRect.Left.ToString() + "," + FlashRect.Top.ToString() + ")";
		}

		// 範囲選択用フォーム
		class SelectForm : Form, SelectBoxCallback {
			// Private変数・メソッド
			//イベント用interface
			SelectFormCallback Event;
			//画像描画用PictureBox
			SelectBox DrawBox;
			// Public変数・メソッド
			//選択が完了したフラグ
			public bool DragedFlg = false;
			//コンストラクタ
			public SelectForm(SelectFormCallback Event, Screen screen) {
				// ディスプレイから画像をコピーする
				var displayImage = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
				var g = Graphics.FromImage(displayImage);
				g.CopyFromScreen(screen.Bounds.Location, Point.Empty, displayImage.Size);
				g.Dispose();
				// PictureBoxの初期化
				DrawBox = new SelectBox(this, displayImage);
				/* フォームの初期化
				 * ・位置とサイズはディスプレイと同一
				 * ・枠線と最大最小ボタンは無し
				 * ・最前面表示にした上でPictureBoxを貼り付ける
				 */
				Bounds = screen.Bounds;
				Size = displayImage.Size;
				Location = screen.Bounds.Location;
				StartPosition = FormStartPosition.Manual;
				FormBorderStyle = FormBorderStyle.None;
				MaximizeBox = MinimizeBox = false;
				TopMost = true;
				Controls.Add(DrawBox);
				//interfaceの受け渡し
				this.Event = Event;
			}
			// 範囲選択完了時に起きるイベント
			public void SelectFinish() {
				DragedFlg = true;
				Event.SelectFinish(DrawBox.Rect);   //イベントをフォームに通知する
				Close();
			}

			// 範囲選択用PictureBox
			public class SelectBox : PictureBox {
				// Private変数・メソッド
				//イベント用interface
				SelectBoxCallback Event;
				//選択開始・終了座標
				Point firstPosition, lastPosition;
				//選択中フラグ
				bool DraggingFlg = false;
				//ディスプレイ画像のバッファ
				Image DisplayImage;
				// 選択範囲を計算する
				Rectangle CalcRectangle(Point first, Point last) {
					int px = Math.Min(first.X, last.X);
					int py = Math.Min(first.Y, last.Y);
					int wx = Math.Max(first.X, last.X) - px;
					int wy = Math.Max(first.Y, last.Y) - py;
					return new Rectangle(px, py, wx, wy);
				}

				// Public変数・メソッド
				public Rectangle Rect;
				// コンストラクタ
				public SelectBox(SelectBoxCallback Event, Image image) {
					// オブジェクト自体の初期化
					Location = Point.Empty;
					Size = image.Size;
					Image = image;
					// DisplayImageにもコピーしておく
					DisplayImage = new Bitmap(Size.Width, Size.Height);
					var g = Graphics.FromImage(DisplayImage);
					g.DrawImage(image, Point.Empty);
					g.Dispose();
					//interfaceの受け渡し
					this.Event = Event;
				}
				// 選択開始
				protected override void OnMouseDown(MouseEventArgs e) {
					DraggingFlg = true;
					firstPosition = PointToClient(MousePosition);
				}
				// 選択終了
				protected override void OnMouseUp(MouseEventArgs e) {
					DraggingFlg = false;
					lastPosition = PointToClient(MousePosition);
					Event.SelectFinish();   //イベントをフォームに通知する
				}
				// 選択中
				protected override void OnMouseMove(MouseEventArgs e) {
					// 選択中でない場合は無視する
					if(!DraggingFlg)
						return;
					// 選択範囲を計算する
					lastPosition = PointToClient(MousePosition);
					Rect = CalcRectangle(firstPosition, lastPosition);
					// 表示を更新する
					var g = Graphics.FromImage(Image);
					g.DrawImage(DisplayImage, Point.Empty);
					var p = new Pen(Color.LightBlue, 2);
					g.DrawRectangle(p, Rect);
					p.Dispose();
					g.Dispose();

					Refresh();
				}
			}

		}

		// 範囲選択PictureBox用interface
		interface SelectBoxCallback {
			void SelectFinish();
		}
	}
	// 範囲選択フォーム用interface
	internal interface SelectFormCallback {
		void SelectFinish(Rectangle rect);
	}
}
