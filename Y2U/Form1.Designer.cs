namespace Y2U {
	partial class Form1 {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			buttonDownloadVideo = new Button();
			textBoxUrl = new TextBox();
			pictureBoxThumbnail = new PictureBox();
			labelTitle = new Label();
			labelDuration = new Label();
			textBoxSavePath = new TextBox();
			buttonChangeSavePath = new Button();
			folderBrowserDialogSavePath = new FolderBrowserDialog();
			progressBarDownloading = new ProgressBar();
			labelPreStatus = new Label();
			labelStatus = new Label();
			buttonDownloadAudio = new Button();
			groupBoxDetails = new GroupBox();
			labelAudioQuality = new Label();
			labelTopAudioQualityTitle = new Label();
			labelVideoQuality = new Label();
			labelTopVideoQualityTitle = new Label();
			labelUploadDate = new Label();
			labelUploadDateTitle = new Label();
			labelAuthor = new Label();
			labelAuthorTitle = new Label();
			tabControlQuality = new TabControl();
			tabPageVideoQuality = new TabPage();
			radioButton144pVid = new RadioButton();
			radioButton240pVid = new RadioButton();
			radioButton360pVid = new RadioButton();
			radioButton480pVid = new RadioButton();
			radioButton720pVid = new RadioButton();
			radioButton1440pVid = new RadioButton();
			radioButton1080pVid = new RadioButton();
			labelVidQualOptions = new Label();
			radioButton4kVid = new RadioButton();
			checkBoxTopQualityVideo = new CheckBox();
			tabPage2 = new TabPage();
			textBoxAudioQualityHelp = new TextBox();
			checkBoxTopQualityAud = new CheckBox();
			listBoxAudQualities = new ListBox();
			labelAudQualOptions = new Label();
			((System.ComponentModel.ISupportInitialize)pictureBoxThumbnail).BeginInit();
			groupBoxDetails.SuspendLayout();
			tabControlQuality.SuspendLayout();
			tabPageVideoQuality.SuspendLayout();
			tabPage2.SuspendLayout();
			SuspendLayout();
			// 
			// buttonDownloadVideo
			// 
			buttonDownloadVideo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonDownloadVideo.BackColor = Color.Red;
			buttonDownloadVideo.Cursor = Cursors.Hand;
			buttonDownloadVideo.FlatAppearance.BorderSize = 0;
			buttonDownloadVideo.FlatStyle = FlatStyle.Flat;
			buttonDownloadVideo.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			buttonDownloadVideo.ForeColor = Color.White;
			buttonDownloadVideo.Location = new Point(500, 52);
			buttonDownloadVideo.Margin = new Padding(3, 4, 3, 4);
			buttonDownloadVideo.Name = "buttonDownloadVideo";
			buttonDownloadVideo.Size = new Size(400, 28);
			buttonDownloadVideo.TabIndex = 0;
			buttonDownloadVideo.Text = "Download Video (.mp4)";
			buttonDownloadVideo.UseVisualStyleBackColor = false;
			buttonDownloadVideo.Click += buttonDownloadVideo_Click;
			// 
			// textBoxUrl
			// 
			textBoxUrl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			textBoxUrl.Cursor = Cursors.IBeam;
			textBoxUrl.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			textBoxUrl.Location = new Point(14, 14);
			textBoxUrl.Margin = new Padding(3, 4, 3, 4);
			textBoxUrl.Name = "textBoxUrl";
			textBoxUrl.Size = new Size(886, 30);
			textBoxUrl.TabIndex = 1;
			textBoxUrl.Text = "Paste Youtube Link";
			textBoxUrl.TextChanged += textBoxUrl_TextChanged;
			// 
			// pictureBoxThumbnail
			// 
			pictureBoxThumbnail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			pictureBoxThumbnail.Image = Properties.Resources.yt_logo480x270;
			pictureBoxThumbnail.Location = new Point(14, 52);
			pictureBoxThumbnail.Margin = new Padding(3, 4, 3, 4);
			pictureBoxThumbnail.Name = "pictureBoxThumbnail";
			pictureBoxThumbnail.Size = new Size(480, 270);
			pictureBoxThumbnail.SizeMode = PictureBoxSizeMode.CenterImage;
			pictureBoxThumbnail.TabIndex = 3;
			pictureBoxThumbnail.TabStop = false;
			// 
			// labelTitle
			// 
			labelTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			labelTitle.AutoEllipsis = true;
			labelTitle.Font = new Font("Tahoma", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelTitle.Location = new Point(12, 326);
			labelTitle.Name = "labelTitle";
			labelTitle.Size = new Size(482, 36);
			labelTitle.TabIndex = 4;
			labelTitle.Text = "Y2U";
			labelTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// labelDuration
			// 
			labelDuration.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			labelDuration.Font = new Font("Tahoma", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelDuration.Location = new Point(12, 355);
			labelDuration.Name = "labelDuration";
			labelDuration.Size = new Size(482, 36);
			labelDuration.TabIndex = 5;
			labelDuration.Text = "Duration: 00:00:00";
			labelDuration.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// textBoxSavePath
			// 
			textBoxSavePath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			textBoxSavePath.Cursor = Cursors.IBeam;
			textBoxSavePath.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			textBoxSavePath.Location = new Point(12, 395);
			textBoxSavePath.Margin = new Padding(3, 4, 3, 4);
			textBoxSavePath.Name = "textBoxSavePath";
			textBoxSavePath.Size = new Size(398, 26);
			textBoxSavePath.TabIndex = 6;
			textBoxSavePath.Text = "Enter Save Path";
			// 
			// buttonChangeSavePath
			// 
			buttonChangeSavePath.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonChangeSavePath.BackColor = Color.Red;
			buttonChangeSavePath.Cursor = Cursors.Hand;
			buttonChangeSavePath.FlatAppearance.BorderSize = 0;
			buttonChangeSavePath.FlatStyle = FlatStyle.Flat;
			buttonChangeSavePath.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			buttonChangeSavePath.ForeColor = Color.White;
			buttonChangeSavePath.Location = new Point(416, 395);
			buttonChangeSavePath.Name = "buttonChangeSavePath";
			buttonChangeSavePath.Size = new Size(78, 26);
			buttonChangeSavePath.TabIndex = 7;
			buttonChangeSavePath.Text = "Change";
			buttonChangeSavePath.UseVisualStyleBackColor = false;
			buttonChangeSavePath.Click += buttonChangeSavePath_Click;
			// 
			// folderBrowserDialogSavePath
			// 
			folderBrowserDialogSavePath.RootFolder = Environment.SpecialFolder.CommonVideos;
			// 
			// progressBarDownloading
			// 
			progressBarDownloading.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			progressBarDownloading.Location = new Point(500, 148);
			progressBarDownloading.Name = "progressBarDownloading";
			progressBarDownloading.Size = new Size(400, 25);
			progressBarDownloading.Step = 1;
			progressBarDownloading.TabIndex = 8;
			// 
			// labelPreStatus
			// 
			labelPreStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			labelPreStatus.AutoSize = true;
			labelPreStatus.Location = new Point(500, 127);
			labelPreStatus.Name = "labelPreStatus";
			labelPreStatus.Size = new Size(54, 18);
			labelPreStatus.TabIndex = 9;
			labelPreStatus.Text = "Status:";
			// 
			// labelStatus
			// 
			labelStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			labelStatus.AutoSize = true;
			labelStatus.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelStatus.Location = new Point(560, 127);
			labelStatus.Name = "labelStatus";
			labelStatus.Size = new Size(38, 18);
			labelStatus.TabIndex = 10;
			labelStatus.Text = "Idle";
			// 
			// buttonDownloadAudio
			// 
			buttonDownloadAudio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonDownloadAudio.BackColor = Color.Red;
			buttonDownloadAudio.Cursor = Cursors.Hand;
			buttonDownloadAudio.FlatAppearance.BorderSize = 0;
			buttonDownloadAudio.FlatStyle = FlatStyle.Flat;
			buttonDownloadAudio.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			buttonDownloadAudio.ForeColor = Color.White;
			buttonDownloadAudio.Location = new Point(500, 87);
			buttonDownloadAudio.Margin = new Padding(3, 4, 3, 4);
			buttonDownloadAudio.Name = "buttonDownloadAudio";
			buttonDownloadAudio.Size = new Size(400, 27);
			buttonDownloadAudio.TabIndex = 11;
			buttonDownloadAudio.Text = "Download Audio (.mp3)";
			buttonDownloadAudio.UseVisualStyleBackColor = false;
			buttonDownloadAudio.Click += buttonDownloadAudio_Click;
			// 
			// groupBoxDetails
			// 
			groupBoxDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			groupBoxDetails.Controls.Add(labelAudioQuality);
			groupBoxDetails.Controls.Add(labelTopAudioQualityTitle);
			groupBoxDetails.Controls.Add(labelVideoQuality);
			groupBoxDetails.Controls.Add(labelTopVideoQualityTitle);
			groupBoxDetails.Controls.Add(labelUploadDate);
			groupBoxDetails.Controls.Add(labelUploadDateTitle);
			groupBoxDetails.Controls.Add(labelAuthor);
			groupBoxDetails.Controls.Add(labelAuthorTitle);
			groupBoxDetails.Location = new Point(500, 179);
			groupBoxDetails.Name = "groupBoxDetails";
			groupBoxDetails.Size = new Size(400, 242);
			groupBoxDetails.TabIndex = 12;
			groupBoxDetails.TabStop = false;
			groupBoxDetails.Text = "Details";
			// 
			// labelAudioQuality
			// 
			labelAudioQuality.AutoSize = true;
			labelAudioQuality.Location = new Point(24, 194);
			labelAudioQuality.Name = "labelAudioQuality";
			labelAudioQuality.Size = new Size(30, 18);
			labelAudioQuality.TabIndex = 7;
			labelAudioQuality.Text = "n/a";
			// 
			// labelTopAudioQualityTitle
			// 
			labelTopAudioQualityTitle.AutoSize = true;
			labelTopAudioQualityTitle.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelTopAudioQualityTitle.Location = new Point(6, 176);
			labelTopAudioQualityTitle.Name = "labelTopAudioQualityTitle";
			labelTopAudioQualityTitle.Size = new Size(145, 18);
			labelTopAudioQualityTitle.TabIndex = 6;
			labelTopAudioQualityTitle.Text = "Top Audio Quality:";
			// 
			// labelVideoQuality
			// 
			labelVideoQuality.AutoSize = true;
			labelVideoQuality.Location = new Point(24, 143);
			labelVideoQuality.Name = "labelVideoQuality";
			labelVideoQuality.Size = new Size(30, 18);
			labelVideoQuality.TabIndex = 5;
			labelVideoQuality.Text = "n/a";
			// 
			// labelTopVideoQualityTitle
			// 
			labelTopVideoQualityTitle.AutoSize = true;
			labelTopVideoQualityTitle.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelTopVideoQualityTitle.Location = new Point(6, 125);
			labelTopVideoQualityTitle.Name = "labelTopVideoQualityTitle";
			labelTopVideoQualityTitle.Size = new Size(145, 18);
			labelTopVideoQualityTitle.TabIndex = 4;
			labelTopVideoQualityTitle.Text = "Top Video Quality:";
			// 
			// labelUploadDate
			// 
			labelUploadDate.AutoSize = true;
			labelUploadDate.Location = new Point(24, 89);
			labelUploadDate.Name = "labelUploadDate";
			labelUploadDate.Size = new Size(30, 18);
			labelUploadDate.TabIndex = 3;
			labelUploadDate.Text = "n/a";
			// 
			// labelUploadDateTitle
			// 
			labelUploadDateTitle.AutoSize = true;
			labelUploadDateTitle.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelUploadDateTitle.Location = new Point(6, 71);
			labelUploadDateTitle.Name = "labelUploadDateTitle";
			labelUploadDateTitle.Size = new Size(104, 18);
			labelUploadDateTitle.TabIndex = 2;
			labelUploadDateTitle.Text = "Upload Date:";
			// 
			// labelAuthor
			// 
			labelAuthor.AutoSize = true;
			labelAuthor.Location = new Point(24, 40);
			labelAuthor.Name = "labelAuthor";
			labelAuthor.Size = new Size(30, 18);
			labelAuthor.TabIndex = 1;
			labelAuthor.Text = "n/a";
			// 
			// labelAuthorTitle
			// 
			labelAuthorTitle.AutoSize = true;
			labelAuthorTitle.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelAuthorTitle.Location = new Point(6, 22);
			labelAuthorTitle.Name = "labelAuthorTitle";
			labelAuthorTitle.Size = new Size(63, 18);
			labelAuthorTitle.TabIndex = 0;
			labelAuthorTitle.Text = "Author:";
			// 
			// tabControlQuality
			// 
			tabControlQuality.Controls.Add(tabPageVideoQuality);
			tabControlQuality.Controls.Add(tabPage2);
			tabControlQuality.Dock = DockStyle.Right;
			tabControlQuality.Location = new Point(906, 0);
			tabControlQuality.Name = "tabControlQuality";
			tabControlQuality.SelectedIndex = 0;
			tabControlQuality.Size = new Size(232, 433);
			tabControlQuality.TabIndex = 13;
			// 
			// tabPageVideoQuality
			// 
			tabPageVideoQuality.Controls.Add(radioButton144pVid);
			tabPageVideoQuality.Controls.Add(radioButton240pVid);
			tabPageVideoQuality.Controls.Add(radioButton360pVid);
			tabPageVideoQuality.Controls.Add(radioButton480pVid);
			tabPageVideoQuality.Controls.Add(radioButton720pVid);
			tabPageVideoQuality.Controls.Add(radioButton1440pVid);
			tabPageVideoQuality.Controls.Add(radioButton1080pVid);
			tabPageVideoQuality.Controls.Add(labelVidQualOptions);
			tabPageVideoQuality.Controls.Add(radioButton4kVid);
			tabPageVideoQuality.Controls.Add(checkBoxTopQualityVideo);
			tabPageVideoQuality.Location = new Point(4, 27);
			tabPageVideoQuality.Name = "tabPageVideoQuality";
			tabPageVideoQuality.Padding = new Padding(3);
			tabPageVideoQuality.Size = new Size(224, 402);
			tabPageVideoQuality.TabIndex = 0;
			tabPageVideoQuality.Text = "Video Quality";
			tabPageVideoQuality.UseVisualStyleBackColor = true;
			// 
			// radioButton144pVid
			// 
			radioButton144pVid.AutoSize = true;
			radioButton144pVid.Font = new Font("Tahoma", 14.25F);
			radioButton144pVid.Location = new Point(6, 260);
			radioButton144pVid.Name = "radioButton144pVid";
			radioButton144pVid.Size = new Size(69, 27);
			radioButton144pVid.TabIndex = 9;
			radioButton144pVid.TabStop = true;
			radioButton144pVid.Text = "144p";
			radioButton144pVid.UseVisualStyleBackColor = true;
			radioButton144pVid.CheckedChanged += radioButton144pVid_CheckedChanged;
			// 
			// radioButton240pVid
			// 
			radioButton240pVid.AutoSize = true;
			radioButton240pVid.Font = new Font("Tahoma", 14.25F);
			radioButton240pVid.Location = new Point(6, 232);
			radioButton240pVid.Name = "radioButton240pVid";
			radioButton240pVid.Size = new Size(69, 27);
			radioButton240pVid.TabIndex = 8;
			radioButton240pVid.TabStop = true;
			radioButton240pVid.Text = "240p";
			radioButton240pVid.UseVisualStyleBackColor = true;
			radioButton240pVid.CheckedChanged += radioButton240pVid_CheckedChanged;
			// 
			// radioButton360pVid
			// 
			radioButton360pVid.AutoSize = true;
			radioButton360pVid.Font = new Font("Tahoma", 14.25F);
			radioButton360pVid.Location = new Point(6, 204);
			radioButton360pVid.Name = "radioButton360pVid";
			radioButton360pVid.Size = new Size(69, 27);
			radioButton360pVid.TabIndex = 7;
			radioButton360pVid.TabStop = true;
			radioButton360pVid.Text = "360p";
			radioButton360pVid.UseVisualStyleBackColor = true;
			radioButton360pVid.CheckedChanged += radioButton360pVid_CheckedChanged;
			// 
			// radioButton480pVid
			// 
			radioButton480pVid.AutoSize = true;
			radioButton480pVid.Font = new Font("Tahoma", 14.25F);
			radioButton480pVid.Location = new Point(6, 176);
			radioButton480pVid.Name = "radioButton480pVid";
			radioButton480pVid.Size = new Size(69, 27);
			radioButton480pVid.TabIndex = 6;
			radioButton480pVid.TabStop = true;
			radioButton480pVid.Text = "480p";
			radioButton480pVid.UseVisualStyleBackColor = true;
			radioButton480pVid.CheckedChanged += radioButton480pVid_CheckedChanged;
			// 
			// radioButton720pVid
			// 
			radioButton720pVid.AutoSize = true;
			radioButton720pVid.Font = new Font("Tahoma", 14.25F);
			radioButton720pVid.Location = new Point(6, 148);
			radioButton720pVid.Name = "radioButton720pVid";
			radioButton720pVid.Size = new Size(69, 27);
			radioButton720pVid.TabIndex = 5;
			radioButton720pVid.TabStop = true;
			radioButton720pVid.Text = "720p";
			radioButton720pVid.UseVisualStyleBackColor = true;
			radioButton720pVid.CheckedChanged += radioButton720pVid_CheckedChanged;
			// 
			// radioButton1440pVid
			// 
			radioButton1440pVid.AutoSize = true;
			radioButton1440pVid.Font = new Font("Tahoma", 14.25F);
			radioButton1440pVid.Location = new Point(6, 92);
			radioButton1440pVid.Name = "radioButton1440pVid";
			radioButton1440pVid.Size = new Size(79, 27);
			radioButton1440pVid.TabIndex = 4;
			radioButton1440pVid.TabStop = true;
			radioButton1440pVid.Text = "1440p";
			radioButton1440pVid.UseVisualStyleBackColor = true;
			radioButton1440pVid.CheckedChanged += radioButton1440pVid_CheckedChanged;
			// 
			// radioButton1080pVid
			// 
			radioButton1080pVid.AutoSize = true;
			radioButton1080pVid.Font = new Font("Tahoma", 14.25F);
			radioButton1080pVid.Location = new Point(6, 120);
			radioButton1080pVid.Name = "radioButton1080pVid";
			radioButton1080pVid.Size = new Size(79, 27);
			radioButton1080pVid.TabIndex = 3;
			radioButton1080pVid.TabStop = true;
			radioButton1080pVid.Text = "1080p";
			radioButton1080pVid.UseVisualStyleBackColor = true;
			radioButton1080pVid.CheckedChanged += radioButton1080pVid_CheckedChanged;
			// 
			// labelVidQualOptions
			// 
			labelVidQualOptions.AutoSize = true;
			labelVidQualOptions.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelVidQualOptions.Location = new Point(3, 43);
			labelVidQualOptions.Name = "labelVidQualOptions";
			labelVidQualOptions.Size = new Size(130, 18);
			labelVidQualOptions.TabIndex = 2;
			labelVidQualOptions.Text = "Quality Options:";
			// 
			// radioButton4kVid
			// 
			radioButton4kVid.AutoSize = true;
			radioButton4kVid.Font = new Font("Tahoma", 14.25F);
			radioButton4kVid.Location = new Point(6, 64);
			radioButton4kVid.Name = "radioButton4kVid";
			radioButton4kVid.Size = new Size(47, 27);
			radioButton4kVid.TabIndex = 1;
			radioButton4kVid.TabStop = true;
			radioButton4kVid.Text = "4k";
			radioButton4kVid.UseVisualStyleBackColor = true;
			radioButton4kVid.CheckedChanged += radioButton4kVid_CheckedChanged;
			// 
			// checkBoxTopQualityVideo
			// 
			checkBoxTopQualityVideo.AutoSize = true;
			checkBoxTopQualityVideo.Checked = true;
			checkBoxTopQualityVideo.CheckState = CheckState.Checked;
			checkBoxTopQualityVideo.Font = new Font("Tahoma", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			checkBoxTopQualityVideo.Location = new Point(6, 6);
			checkBoxTopQualityVideo.Name = "checkBoxTopQualityVideo";
			checkBoxTopQualityVideo.Size = new Size(212, 29);
			checkBoxTopQualityVideo.TabIndex = 0;
			checkBoxTopQualityVideo.Text = "Get Highest Quality";
			checkBoxTopQualityVideo.UseVisualStyleBackColor = true;
			checkBoxTopQualityVideo.CheckedChanged += checkBoxTopQualityVideo_CheckedChanged;
			// 
			// tabPage2
			// 
			tabPage2.Controls.Add(textBoxAudioQualityHelp);
			tabPage2.Controls.Add(checkBoxTopQualityAud);
			tabPage2.Controls.Add(listBoxAudQualities);
			tabPage2.Controls.Add(labelAudQualOptions);
			tabPage2.Location = new Point(4, 24);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(3);
			tabPage2.Size = new Size(224, 405);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "Audio Quality";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// textBoxAudioQualityHelp
			// 
			textBoxAudioQualityHelp.Location = new Point(6, 235);
			textBoxAudioQualityHelp.Multiline = true;
			textBoxAudioQualityHelp.Name = "textBoxAudioQualityHelp";
			textBoxAudioQualityHelp.ReadOnly = true;
			textBoxAudioQualityHelp.Size = new Size(212, 135);
			textBoxAudioQualityHelp.TabIndex = 15;
			textBoxAudioQualityHelp.Text = "Select a quality option from above to use it.\r\n\r\nThe selected quality option will be used in the video download is well. File size might not match up\r\n\r\n";
			// 
			// checkBoxTopQualityAud
			// 
			checkBoxTopQualityAud.AutoSize = true;
			checkBoxTopQualityAud.Checked = true;
			checkBoxTopQualityAud.CheckState = CheckState.Checked;
			checkBoxTopQualityAud.Font = new Font("Tahoma", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			checkBoxTopQualityAud.Location = new Point(6, 6);
			checkBoxTopQualityAud.Name = "checkBoxTopQualityAud";
			checkBoxTopQualityAud.Size = new Size(212, 29);
			checkBoxTopQualityAud.TabIndex = 14;
			checkBoxTopQualityAud.Text = "Get Highest Quality";
			checkBoxTopQualityAud.UseVisualStyleBackColor = true;
			checkBoxTopQualityAud.CheckedChanged += checkBoxTopQualityAud_CheckedChanged;
			// 
			// listBoxAudQualities
			// 
			listBoxAudQualities.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			listBoxAudQualities.FormattingEnabled = true;
			listBoxAudQualities.ItemHeight = 23;
			listBoxAudQualities.Items.AddRange(new object[] { "456", "asd", "gfhjk", "h", "xcghn" });
			listBoxAudQualities.Location = new Point(6, 64);
			listBoxAudQualities.Name = "listBoxAudQualities";
			listBoxAudQualities.Size = new Size(212, 165);
			listBoxAudQualities.TabIndex = 13;
			listBoxAudQualities.SelectedIndexChanged += listBoxAudQualities_SelectedIndexChanged;
			// 
			// labelAudQualOptions
			// 
			labelAudQualOptions.AutoSize = true;
			labelAudQualOptions.Font = new Font("Tahoma", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			labelAudQualOptions.Location = new Point(3, 43);
			labelAudQualOptions.Name = "labelAudQualOptions";
			labelAudQualOptions.Size = new Size(130, 18);
			labelAudQualOptions.TabIndex = 12;
			labelAudQualOptions.Text = "Quality Options:";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 18F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1138, 433);
			Controls.Add(tabControlQuality);
			Controls.Add(groupBoxDetails);
			Controls.Add(buttonDownloadAudio);
			Controls.Add(labelStatus);
			Controls.Add(labelPreStatus);
			Controls.Add(progressBarDownloading);
			Controls.Add(buttonChangeSavePath);
			Controls.Add(textBoxSavePath);
			Controls.Add(labelTitle);
			Controls.Add(pictureBoxThumbnail);
			Controls.Add(textBoxUrl);
			Controls.Add(buttonDownloadVideo);
			Controls.Add(labelDuration);
			Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(3, 4, 3, 4);
			MinimumSize = new Size(1154, 472);
			Name = "Form1";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Y2U";
			FormClosing += Form1_FormClosing;
			SizeChanged += Form1_SizeChanged;
			((System.ComponentModel.ISupportInitialize)pictureBoxThumbnail).EndInit();
			groupBoxDetails.ResumeLayout(false);
			groupBoxDetails.PerformLayout();
			tabControlQuality.ResumeLayout(false);
			tabPageVideoQuality.ResumeLayout(false);
			tabPageVideoQuality.PerformLayout();
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button buttonDownloadVideo;
		private TextBox textBoxUrl;
		private PictureBox pictureBoxThumbnail;
		private Label labelTitle;
		private Label labelDuration;
		private TextBox textBoxSavePath;
		private Button buttonChangeSavePath;
		private FolderBrowserDialog folderBrowserDialogSavePath;
		private ProgressBar progressBarDownloading;
		private Label labelPreStatus;
		private Label labelStatus;
		private Button buttonDownloadAudio;
		private GroupBox groupBoxDetails;
		private Label labelAuthor;
		private Label labelAuthorTitle;
		private Label labelUploadDateTitle;
		private Label labelUploadDate;
		private Label labelVideoQuality;
		private Label labelTopVideoQualityTitle;
		private Label labelAudioQuality;
		private Label labelTopAudioQualityTitle;
		private TabControl tabControlQuality;
		private TabPage tabPageVideoQuality;
		private CheckBox checkBoxTopQualityVideo;
		private TabPage tabPage2;
		private RadioButton radioButton4kVid;
		private Label labelVidQualOptions;
		private RadioButton radioButton360pVid;
		private RadioButton radioButton480pVid;
		private RadioButton radioButton720pVid;
		private RadioButton radioButton1440pVid;
		private RadioButton radioButton1080pVid;
		private RadioButton radioButton144pVid;
		private RadioButton radioButton240pVid;
		private RadioButton radioButton1;
		private RadioButton radioButton2;
		private RadioButton radioButton3;
		private RadioButton radioButton4;
		private RadioButton radioButton5;
		private RadioButton radioButton6;
		private RadioButton radioButton7;
		private Label labelAudQualOptions;
		private ListBox listBoxAudQualities;
		private CheckBox checkBoxTopQualityAud;
		private RadioButton radioButton8;
		private TextBox textBoxAudioQualityHelp;
	}
}
