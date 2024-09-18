using ImageMagick;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
//using static System.Net.WebRequestMethods;

namespace Y2U {
	public partial class Form1 : Form {
		private const int STATUS_IDLE = 0;
		private const int STATUS_GETTING_STATS = 1;
		private const int STATUS_DOWNLOADING = 2;
		private int status = STATUS_IDLE;

		private readonly YoutubeDownload youtube = new YoutubeDownload();
		private readonly HttpClient httpClient = new HttpClient();
		private DownloadSelection userDownloadSelection = new DownloadSelection();

		//private bool downloading = false;
		//private bool gettingStats = false;
		private bool isUrlValid = false;
		private string thumbnailUrl = "";

		private string videoPath = "";
		private string audioPath = "";

		// to avoid spamming requests
		private System.Windows.Forms.Timer debounceTimer = new System.Windows.Forms.Timer();
		private System.Windows.Forms.Timer resizeThumbnailLoader = new System.Windows.Forms.Timer();
		private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		private TaskCompletionSource<bool> gotInfoTask = new TaskCompletionSource<bool>();

		public Form1() {
			InitializeComponent();
			textBoxSavePath.Text = Directory.GetCurrentDirectory();
			this.Text = $"Y2U - {System.Windows.Forms.Application.ProductVersion}";

			checkFFmpeg();
			loadSavedSettings();
			//LoadThumbnail("https://img3.gelbooru.com//images/a6/c8/a6c8bba909950809911901c9db25a268.png");// little test

			debounceTimer.Interval = 250; // delay of 250 ms
			debounceTimer.Tick += async (s, e) => { // event will be called when the timer hits the interval
				debounceTimer.Stop();
				status = STATUS_GETTING_STATS;
				labelStatus.Text = "Getting Video Info";

				try {
					disableDownloadButtons();
					Video video = await youtube.YoutubeClient.Videos.GetAsync(textBoxUrl.Text, cancellationTokenSource.Token);
					StreamManifest streamManifest = await youtube.YoutubeClient.Videos.Streams.GetManifestAsync(textBoxUrl.Text, cancellationTokenSource.Token);
					thumbnailUrl = video.Thumbnails.GetWithHighestResolution().Url;
					LoadThumbnail(thumbnailUrl);
					DisplayInfo(video, streamManifest);
					UpdateQualityOptions(streamManifest, video);

					//by reaching here the url must be valid
					isUrlValid = true;
					status = STATUS_IDLE;
					labelStatus.Text = "Idle";
					gotInfoTask.TrySetResult(true);
					enableDownloadButtons();
				} catch (Exception ex) {
					enableDownloadButtons();
					isUrlValid = false;
					status = STATUS_IDLE;
					labelStatus.Text = "Failed To Get Video Info";
					//debex.Data
					Debug.WriteLine(ex);

					Debug.WriteLine($"set exception for getInfoTask successful: {gotInfoTask.TrySetException(ex)}");
					return;
				}
			};

			resizeThumbnailLoader.Interval = 250;
			resizeThumbnailLoader.Tick += (s, e) => {
				resizeThumbnailLoader.Stop();
				LoadThumbnail(this.thumbnailUrl);
			};
		}

		public Form1(string url, string outputPath, bool isVid, int resolution, string bitrate) : this() {
			textBoxUrl.Text = url;
			textBoxSavePath.Text = outputPath;

			this.checkBoxTopQualityVideo.Checked = resolution == -1;
			this.checkBoxTopQualityAud.Checked = string.IsNullOrEmpty(bitrate);

			switch (resolution) {
				case Resolutions.UHD:
					radioButton4kVid.Checked = true;
					break;
				case Resolutions.WHD:
					radioButton1440pVid.Checked = true;
					break;
				case Resolutions.HD:
					radioButton1080pVid.Checked = true;
					break;
				case Resolutions.LHD:
					radioButton720pVid.Checked = true;
					break;
				case Resolutions.SD:
					radioButton480pVid.Checked = true;
					break;
				case Resolutions.LD:
					radioButton360pVid.Checked = true;
					break;
				case Resolutions.VLD:
					radioButton240pVid.Checked = true;
					break;
				case Resolutions.XLD:
					radioButton144pVid.Checked = true;
					break;
			}
			Debug.WriteLine("instant downloading!");
			InstantDownload(url, isVid, resolution, bitrate);
		}

		private void loadSavedSettings() {
			SaveData? saveData = SaveDataHandler.readSaveData();
			if (saveData != null) {
				this.textBoxSavePath.Text = saveData?.SavePath ?? this.textBoxSavePath.Text;
			}
		}

		private void saveSettings() {
			SaveData saveData = new SaveData() {
				SavePath = this.textBoxSavePath.Text
			};

			SaveDataHandler.writeSaveData(saveData);
		}

		private void disableDownloadButtons() {
			buttonDownloadAudio.UseWaitCursor = true;
			buttonDownloadVideo.UseWaitCursor = true;

			buttonDownloadAudio.Enabled = false;
			buttonDownloadVideo.Enabled = false;
		}

		private void enableDownloadButtons() {
			buttonDownloadAudio.UseWaitCursor = false;
			buttonDownloadVideo.UseWaitCursor = false;

			buttonDownloadAudio.Enabled = true;
			buttonDownloadVideo.Enabled = true;
		}

		private async void InstantDownload(string url, bool isVid, int resolution, string bitrate) {
			// url check
			//if(!await checkUrlFull(url)) { Console.WriteLine("Invalid Youtube url!"); return; }
			debounceTimer.Stop();
			debounceTimer.Start();

			try {
				await gotInfoTask.Task;
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
				Console.WriteLine("Failed to get video info from url, possible broken url");
			}

			await Task.Delay(1500); // makes sure the info task is complete

			this.userDownloadSelection.overrideAudioSelection = bitrate;
			this.userDownloadSelection.selectedVideoStreamKey = resolution;
			Debug.WriteLine($"instant download resoltuion ======================================== {resolution} ========================================");

			if (isVid) {
				buttonDownloadVideo.PerformClick();
				Debug.WriteLine("simulated video download button press");
			} else {
				buttonDownloadAudio.PerformClick();
				Debug.WriteLine("simulated audio download button press");
			}
		}

		#region checks

		private bool isSavePathSelected() {
			string path = textBoxSavePath.Text;

			if (string.IsNullOrEmpty(path))
				return false;
			if (path == "Enter Save Path")
				return false;
			if (!Directory.Exists(path))
				return false;

			return true;
		}

		private bool checkSavePath() {
			if (isSavePathSelected()) { return true; }

			MessageBox.Show("No file path chosen!", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}

		private bool checkUrl() {
			if (isUrlValid) { return true; }
			if (status == STATUS_GETTING_STATS) {
				MessageBox.Show("Still getting video info!", "Busy", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return false;
			}

			MessageBox.Show("Invalid Youtube url!", "Url Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return false;
		}

		/// <summary>
		/// attempts to get video info from given url, invalid urls will fail
		/// </summary>
		/// <param name="url"></param>
		/// <returns>true if valid, false if invalid</returns>
		private async Task<bool> checkUrlFull(string url) {
			if (status != STATUS_IDLE) { return false; } // most likely never will happen but incase

			try {
				await youtube.YoutubeClient.Videos.GetAsync(url, cancellationTokenSource.Token);
				return true;
			} catch {
				MessageBox.Show("Invalid Youtube url!", "Url Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
		}

		private async Task<bool> checkFFmpeg() {
			if (await Mux.checkFFmpegAvailable()) { return true; }

			MessageBox.Show("FFmpeg failed to launch/could not be found!\n\nMake sure FFmpeg is available in PATH (system environment variables) and relaunch.", "FFmpeg Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}

		#endregion

		#region video info

		/// <summary>
		/// updates lables with new data
		/// </summary>
		/// <param name="video"></param>
		/// <param name="manifest"></param>
		private void DisplayInfo(Video video, StreamManifest manifest) {

			// format: hh:mm:ss
			labelDuration.Text = $"Duration: " +
			$"{(video.Duration.HasValue ? video.Duration.Value.Hours.ToString("00") : "00")}" +
			$":{(video.Duration.HasValue ? video.Duration.Value.Minutes.ToString("00") : "00")}" +
			$":{(video.Duration.HasValue ? video.Duration.Value.Seconds.ToString("00") : "00")}";

			labelTitle.Text = video.Title;
			labelAuthor.Text = video.Author.ChannelTitle;
			labelUploadDate.Text = video.UploadDate.ToString("F");

			IVideoStreamInfo vidStream = manifest.GetVideoOnlyStreams().GetWithHighestVideoQuality();
			labelVideoQuality.Text = $"Resolution: {vidStream.VideoResolution}, FPS: {vidStream.VideoQuality.Framerate}, Bitrate: {vidStream.Bitrate}";
			IStreamInfo audStream = manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
			labelAudioQuality.Text = $"Bitrate: {audStream.Bitrate}";
		}

		

		private void UpdateQualityOptions(StreamManifest manifest, Video video) {
			// TODO make less spammy
			DownloadSelection newDownloadSelection = new DownloadSelection();

			IEnumerable<VideoOnlyStreamInfo> videoStreams = manifest.GetVideoOnlyStreams();
			List<AudioOnlyStreamInfo> audioStreams = newDownloadSelection.sortAudioStreams(manifest.GetAudioOnlyStreams());

			newDownloadSelection.highestQualityVideoStream = videoStreams.GetWithHighestVideoQuality();
			newDownloadSelection.highestQualityAudioStream = audioStreams.GetWithHighestBitrate();
			newDownloadSelection.useHighestQualityVid = checkBoxTopQualityVideo.Checked;
			newDownloadSelection.useHighestQualityAud = checkBoxTopQualityAud.Checked;
			newDownloadSelection.manifest = manifest;
			newDownloadSelection.video = video;

			// reset selections

			radioButton4kVid.Enabled = false;
			radioButton4kVid.Text = "4k";
			radioButton1440pVid.Enabled = false;
			radioButton1440pVid.Text = "1440p";
			radioButton1080pVid.Enabled = false;
			radioButton1080pVid.Text = "1080p";
			radioButton720pVid.Enabled = false;
			radioButton720pVid.Text = "720p";
			radioButton480pVid.Enabled = false;
			radioButton480pVid.Text = "480p";
			radioButton360pVid.Enabled = false;
			radioButton360pVid.Text = "360p";
			radioButton240pVid.Enabled = false;
			radioButton240pVid.Text = "240p";
			radioButton144pVid.Enabled = false;
			radioButton144pVid.Text = "144p";

			listBoxAudQualities.Items.Clear();


			// gather and sort streams
			foreach (VideoOnlyStreamInfo vs in videoStreams) {
				// filter only mp4 streams
				if (vs.Container != YoutubeExplode.Videos.Streams.Container.Mp4) {
					//continue;
				}

				Debug.WriteLine($"{vs} - {vs.VideoResolution}, {vs.Bitrate}, {vs.Size}");

				switch (vs.VideoQuality.MaxHeight) {
					case Resolutions.UHD:
						if (radioButton4kVid.Enabled) { break; }

						radioButton4kVid.Text = $"4k - {vs.Size}";
						radioButton4kVid.Enabled = true;
						newDownloadSelection.videoStreams?.Add(2160, vs);
						break;
					case Resolutions.WHD:
						if (radioButton1440pVid.Enabled) { break; }

						radioButton1440pVid.Text = $"1440p - {vs.Size}";
						radioButton1440pVid.Enabled = true;
						newDownloadSelection.videoStreams?.Add(1440, vs);
						break;
					case Resolutions.HD:
						if (radioButton1080pVid.Enabled) { break; }

						radioButton1080pVid.Text = $"1080p - {vs.Size}";
						radioButton1080pVid.Enabled = true;
						newDownloadSelection.videoStreams?.Add(1080, vs);
						break;
					case Resolutions.LHD:
						if (radioButton720pVid.Enabled) { break; }

						radioButton720pVid.Text = $"720p - {vs.Size}";
						radioButton720pVid.Enabled = true;
						newDownloadSelection.videoStreams?.Add(720, vs);
						break;
					case Resolutions.SD:
						if (radioButton480pVid.Enabled) { break; }

						radioButton480pVid.Text = $"480p - {vs.Size}";
						radioButton480pVid.Enabled = true;
						newDownloadSelection.videoStreams?.Add(480, vs);
						break;
					case Resolutions.LD:
						if (radioButton360pVid.Enabled) { break; }

						radioButton360pVid.Text = $"360p - {vs.Size}";
						radioButton360pVid.Enabled = true;
						newDownloadSelection.videoStreams?.Add(360, vs);
						break;
					case Resolutions.VLD:
						if (radioButton240pVid.Enabled) { break; }

						radioButton240pVid.Text = $"240p - {vs.Size}";
						radioButton240pVid.Enabled = true;
						newDownloadSelection.videoStreams?.Add(240, vs);
						break;
					case Resolutions.XLD:
						if (radioButton144pVid.Enabled) { break; }

						radioButton144pVid.Text = $"144p - {vs.Size}";
						radioButton144pVid.Enabled = true;
						newDownloadSelection.videoStreams?.Add(144, vs);
						break;
					default:
						Debug.WriteLine(vs.ToString() + " is not valid");
						break;
				}
			}

			foreach (AudioOnlyStreamInfo audioStream in audioStreams) {
				string text = $"{audioStream.Bitrate} - {audioStream.Size}";

				Debug.WriteLine($"{audioStream} - {audioStream.Bitrate.KiloBitsPerSecond}");
				listBoxAudQualities.Items.Add(text);

				newDownloadSelection.audioStreams?.Add(audioStream);
			}

			// remove previously valid but now invalid selections
			//if (!radioButton4kVid.Enabled & radioButton4kVid.Checked) { radioButton4kVid.Checked = false; radioButton1440pVid.Checked = true; }
			//if (!radioButton1440pVid.Enabled & radioButton1440pVid.Checked) { radioButton1440pVid.Checked = false; radioButton1080pVid.Checked = true; }
			//if (!radioButton1080pVid.Enabled & radioButton1080pVid.Checked) { radioButton1080pVid.Checked = false; radioButton720pVid.Checked = true; }
			//if (!radioButton720pVid.Enabled & radioButton720pVid.Checked) { radioButton720pVid.Checked = false; radioButton480pVid.Checked = true; }
			//if (!radioButton480pVid.Enabled & radioButton480pVid.Checked) { radioButton480pVid.Checked = false; radioButton360pVid.Checked = true; }
			//if (!radioButton360pVid.Enabled & radioButton360pVid.Checked) { radioButton360pVid.Checked = false; radioButton240pVid.Checked = true; }
			//if (!radioButton240pVid.Enabled & radioButton240pVid.Checked) { radioButton240pVid.Checked = false; radioButton144pVid.Checked = true; }
			//if (!radioButton144pVid.Enabled & radioButton144pVid.Checked) { radioButton144pVid.Checked = false; checkBoxTopQualityVideo.Checked = true; }
			(RadioButton radioButton, EventHandler checkChangedFunction)[] radioButtons = [
				(radioButton4kVid, radioButton4kVid_CheckedChanged),
				(radioButton1440pVid, radioButton1440pVid_CheckedChanged),
				(radioButton1080pVid, radioButton1080pVid_CheckedChanged),
				(radioButton720pVid, radioButton720pVid_CheckedChanged),
				(radioButton480pVid, radioButton480pVid_CheckedChanged),
				(radioButton360pVid, radioButton360pVid_CheckedChanged),
				(radioButton240pVid, radioButton240pVid_CheckedChanged),
				(radioButton144pVid, radioButton144pVid_CheckedChanged)
			];

			for (int i = 0; i < radioButtons.Length - 1; i++) {
				if (!radioButtons[i].radioButton.Enabled && radioButtons[i].radioButton.Checked) {
					radioButtons[i].radioButton.Checked = false;

					if (i + 1 < radioButtons.Length) {
						radioButtons[i + 1].checkChangedFunction(null, EventArgs.Empty);
						radioButtons[i + 1].radioButton.Checked = true;
					}
				}
			}

			if (!radioButton144pVid.Enabled && radioButton144pVid.Checked) {
				radioButton144pVid.Checked = false;
				checkBoxTopQualityVideo.Checked = true;
			}

			this.userDownloadSelection = newDownloadSelection;

		}


		private async void LoadThumbnail(string imgUrl) {
			if (string.IsNullOrEmpty(imgUrl)) { return; }
			Uri uri = new Uri(imgUrl);
			if (uri.Scheme != Uri.UriSchemeHttps) { return; }
			//if (uri.Host != "i.ytimg.com") { return; } // hoping this is the same for all yt thumbnails
			Debug.WriteLine("loading thumbnail");

			try {
				byte[] imgBytes = await httpClient.GetByteArrayAsync(imgUrl, cancellationTokenSource.Token);
				using (MemoryStream inMs = new MemoryStream(imgBytes)) {
					using (MagickImage image = new MagickImage(inMs)) {
						//change format of image to png
						image.Format = MagickFormat.Png;
						//resize image to size of image box
						image.Resize(pictureBoxThumbnail.Size.Width, pictureBoxThumbnail.Size.Height);

						using (MemoryStream outMs = new MemoryStream()) {
							image.Write(outMs);
							outMs.Position = 0;

							pictureBoxThumbnail.Image = new Bitmap(outMs);
						}
					}
				}
			} catch (OperationCanceledException) {
				Debug.WriteLine("cancelled thumbnail download");
			} catch {
				Debug.WriteLine("Failed to load thumbnail");
			}
		}

		#endregion

		#region download buttons

		private async void buttonDownloadVideo_Click(object sender, EventArgs e) {
			if (status == STATUS_DOWNLOADING) {
				Debug.WriteLine("already downloading");
				return;
			} else if (status == STATUS_GETTING_STATS) {
				Debug.WriteLine("busy getting info");
				return;
			}
			if (!checkUrl()) { return; }
			if (!checkSavePath()) { return; }
			if (!await checkFFmpeg()) { return; }

			status = STATUS_DOWNLOADING; // must be here to prevent soft lock
			progressBarDownloading.Value = 0;
			progressBarDownloading.Maximum = 4;
			disableDownloadButtons();

			labelStatus.Text = "Setup";
			try {
				videoPath = Path.GetTempFileName();
				audioPath = Path.GetTempFileName();
			} catch (IOException) {
				Debug.WriteLine("failed to create temporary files for video and/or audio");
				labelStatus.Text = "Failed to create temporary files";
				progressBarDownloading.Value = 0;
				status = STATUS_IDLE;
				return;
			}

			string url = textBoxUrl.Text;

			IProgress<DownloadProgress> progress = new Progress<DownloadProgress>(p => {
				progressBarDownloading.Value = p.progress;
				labelStatus.Text = p.label;
			});
			Debug.WriteLine("Yes, your Highness. I shall download this glorious video for you (c.c.).");
			await youtube.downloadVideo(progress, this.userDownloadSelection, videoPath, audioPath, textBoxSavePath.Text, cancellationTokenSource);

			status = STATUS_IDLE;
			enableDownloadButtons();
		}


		private async void buttonDownloadAudio_Click(object sender, EventArgs e) {
			if (status == STATUS_DOWNLOADING) {
				Debug.WriteLine("already downloading");
				return;
			} else if (status == STATUS_GETTING_STATS) {
				Debug.WriteLine("busy getting info");
				return;
			}
			if (!checkUrl()) { return; }
			if (!checkSavePath()) { return; }
			if (!await checkFFmpeg()) { return; }

			status = STATUS_DOWNLOADING; // must be here to prevent soft lock
			progressBarDownloading.Value = 0;
			progressBarDownloading.Maximum = 3;
			labelStatus.Text = "Setup";
			disableDownloadButtons();

			string url = textBoxUrl.Text;

			IProgress<DownloadProgress> progress = new Progress<DownloadProgress>(p => {
				progressBarDownloading.Value = p.progress;
				labelStatus.Text = p.label;
			});
			await youtube.downloadAudio(progress, this.userDownloadSelection, textBoxSavePath.Text, cancellationTokenSource);

			status = STATUS_IDLE;
			enableDownloadButtons();
		}

		#endregion

		private void buttonChangeSavePath_Click(object sender, EventArgs e) {
			DialogResult dialogResult = folderBrowserDialogSavePath.ShowDialog();
			if (dialogResult == DialogResult.OK) {
				textBoxSavePath.Text = folderBrowserDialogSavePath.SelectedPath;
			}
		}

		private void textBoxUrl_TextChanged(object sender, EventArgs e) {
			debounceTimer.Stop();
			debounceTimer.Start();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			if (status == STATUS_DOWNLOADING) {
				DialogResult result = MessageBox.Show("You are currently downloading!\nAre you sure you want to close?", "Still Downloading", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if (result == DialogResult.No) {
					e.Cancel = true;
					return;
				}

				//safely close form and cancel all connections (maybe)
				cancellationTokenSource.Cancel();
			}

			saveSettings();
		}

		#region quality_selections

		private void listBoxAudQualities_SelectedIndexChanged(object sender, EventArgs e) {
			//Debug.WriteLine(listBoxAudQualities.SelectedIndex.ToString());
			this.userDownloadSelection.selectedAudioStreamIndex = listBoxAudQualities.SelectedIndex;
		}

		private void checkBoxTopQualityAud_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.useHighestQualityAud = checkBoxTopQualityAud.Checked;
		}

		private void checkBoxTopQualityVideo_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.useHighestQualityVid = checkBoxTopQualityVideo.Checked;
		}

		private void radioButton4kVid_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.selectedVideoStreamKey = Resolutions.UHD;
		}

		private void radioButton1440pVid_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.selectedVideoStreamKey = Resolutions.WHD;
		}

		private void radioButton1080pVid_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.selectedVideoStreamKey = Resolutions.HD;
		}

		private void radioButton720pVid_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.selectedVideoStreamKey = Resolutions.LHD;
		}

		private void radioButton480pVid_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.selectedVideoStreamKey = Resolutions.SD;
		}

		private void radioButton360pVid_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.selectedVideoStreamKey = Resolutions.LD;
		}

		private void radioButton240pVid_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.selectedVideoStreamKey = Resolutions.VLD;
		}

		private void radioButton144pVid_CheckedChanged(object sender, EventArgs e) {
			userDownloadSelection.selectedVideoStreamKey = Resolutions.XLD;
		}

		#endregion

		private void Form1_SizeChanged(object sender, EventArgs e) {
			resizeThumbnailLoader.Stop();
			resizeThumbnailLoader.Start();
		}
	}
}
