namespace KanColleRecorder {
	partial class MainForm {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
			this.GetFlashPositionButton = new System.Windows.Forms.Button();
			this.SaveScreenShotButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// GetFlashPositionButton
			// 
			this.GetFlashPositionButton.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.GetFlashPositionButton.Location = new System.Drawing.Point(12, 12);
			this.GetFlashPositionButton.Name = "GetFlashPositionButton";
			this.GetFlashPositionButton.Size = new System.Drawing.Size(102, 37);
			this.GetFlashPositionButton.TabIndex = 0;
			this.GetFlashPositionButton.Text = "座標取得";
			this.GetFlashPositionButton.UseVisualStyleBackColor = true;
			// 
			// SaveScreenShotButton
			// 
			this.SaveScreenShotButton.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.SaveScreenShotButton.Location = new System.Drawing.Point(120, 12);
			this.SaveScreenShotButton.Name = "SaveScreenShotButton";
			this.SaveScreenShotButton.Size = new System.Drawing.Size(102, 37);
			this.SaveScreenShotButton.TabIndex = 1;
			this.SaveScreenShotButton.Text = "画像保存";
			this.SaveScreenShotButton.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(234, 61);
			this.Controls.Add(this.SaveScreenShotButton);
			this.Controls.Add(this.GetFlashPositionButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "KanColleRecorder";
			this.TopMost = true;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button GetFlashPositionButton;
		private System.Windows.Forms.Button SaveScreenShotButton;
	}
}

