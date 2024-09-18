using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos;

namespace Y2U {
	public class YoutubeDownload {
		public readonly YoutubeClient YoutubeClient = new YoutubeClient();

		/// <summary>
		/// Attempts to delete temporary video and audio files created by <see cref="downloadVideo(IProgress{DownloadProgress}?, string, string, string, string, CancellationTokenSource)"/>
		/// </summary>
		/// <param name="videoPath"></param>
		/// <param name="audioPath"></param>
		public static void DeleteTempFiles(string videoPath, string audioPath) {
			try {
				if (File.Exists(audioPath)) {
					File.Delete(audioPath);
				}
				if (File.Exists(videoPath)) {
					File.Delete(videoPath);
				}
			} catch {
				Debug.WriteLine("failed to delete tmp files");
			}
		}

		/// <summary>
		/// Downloads both video and audio from the provided youtube url into TEMP to be muxed into a single .mp4 file. <b><u>DOES NEED FFMPEG</u></b>
		/// </summary>
		/// <param name="progress">nullable</param>
		/// <param name="streamSelections">streamSelections</param>
		/// <param name="videoPath">full path including filename</param>
		/// <param name="audioPath">full path including filename</param>
		/// <param name="savePath">full path including filename</param>
		/// <param name="cancellationTokenSource">Used to safely cancel a download once already started</param>
		/// <returns></returns>
		public async Task downloadVideo(IProgress<DownloadProgress>? progress, DownloadSelection streamSelections, string videoPath, string audioPath, string savePath, CancellationTokenSource cancellationTokenSource) {
			try {
				//Video video = await YoutubeClient.Videos.GetAsync(url, cancellationTokenSource.Token);
				Debug.WriteLine("downloading video : " + streamSelections.video.Title);
				Console.WriteLine("downloading video : " + streamSelections.video.Title);

				//StreamManifest streamManifest = await YoutubeClient.Videos.Streams.GetManifestAsync(url, cancellationTokenSource.Token);


				//progressBarDownloading.PerformStep();
				//labelStatus.Text = "Downloading Video";
				progress?.Report(new DownloadProgress() { progress = 1, label = "Downloading Video [0%]" });


				// video
				IVideoStreamInfo videoStreamInfo = streamSelections.GetVideoStream();//streamManifest.GetVideoOnlyStreams().GetWithHighestVideoQuality();
				Debug.WriteLine($"using video stream {videoStreamInfo.VideoQuality.Label} for video download");

				//progress
				Progress<double> progressVideo = new Progress<double>(d => {
					progress?.Report(new DownloadProgress() { progress = 1, label = $"Downloading Video [{(int)(d * 100)}%]" });
				});

				//download
				await YoutubeClient.Videos.Streams.DownloadAsync(videoStreamInfo, videoPath, progressVideo, cancellationTokenSource.Token);

				Debug.WriteLine("finished video download");
				progress?.Report(new DownloadProgress() { progress = 2, label = "Downloading Audio [0%]" });

				//audio
				IStreamInfo audioStreamInfo = streamSelections.getAudioStream();//streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
				Debug.WriteLine($"using audio stream {audioStreamInfo.Bitrate} for video download");

				//progress
				Progress<double> progressAudio = new Progress<double>(d => {
					progress?.Report(new DownloadProgress() { progress = 2, label = $"Downloading Audio [{(int)(d * 100)}%]" });
				});

				//download
				await YoutubeClient.Videos.Streams.DownloadAsync(audioStreamInfo, audioPath, progressAudio, cancellationTokenSource.Token);
				Debug.WriteLine("finished audio download");

				string outputPath = Path.Join(savePath, Mux.sanitizeFileName(streamSelections.video.Title) + ".mp4");
				Debug.WriteLine($"tmp files at: {videoPath}, {audioPath}");

				// allows audio download to finish up so text doesnt get overridden
				await Task.Delay(1000);
				progress?.Report(new DownloadProgress() { progress = 3, label = "Muxing Audio and Video" });

				//muxing
				Mux mux = new Mux(videoPath, audioPath, outputPath);
				bool successful = await mux.startVideo(streamSelections.getAudioStreamBitrate());

				if (successful) {
					progress?.Report(new DownloadProgress() { progress = 4, label = "Complete" });
					
					Console.WriteLine($"Downloaded to {outputPath}");
				} else {
					progress?.Report(new DownloadProgress() { progress = 4, label = "Failed to Mux Audio and Video" });
					
					Console.WriteLine("Failed to Mux Audio and Video");
				}

			} catch (OperationCanceledException) {
				Debug.WriteLine("cancelled download");
				DeleteTempFiles(videoPath, audioPath);

			// for extra unexpected stuff
			} catch (Exception ex) {
				Debug.WriteLine(ex.ToString());
				Console.WriteLine(ex.ToString());
			}


		}

		/// <summary>
		/// Downloads the audio from the youtube video provided in the url to the savePath. <b><u>DOES (now) NEED FFMPEG</u></b>
		/// </summary>
		/// <param name="progress">nullable</param>
		/// <param name="streamSelections">streamSelections</param>
		/// <param name="savePath">full path including filename</param>
		/// <param name="cancellationTokenSource">Used to safely cancel a download once already started</param>
		/// <returns></returns>
		public async Task downloadAudio(IProgress<DownloadProgress>? progress, DownloadSelection streamSelections, string savePath, CancellationTokenSource cancellationTokenSource) {
			try {
				//Video video = await YoutubeClient.Videos.GetAsync(url, cancellationTokenSource.Token);
				Debug.WriteLine("downloading audio from : " + streamSelections.video.Title);
				Console.WriteLine("downloading audio from : " + streamSelections.video.Title);

				//StreamManifest streamManifest = await YoutubeClient.Videos.Streams.GetManifestAsync(url, cancellationTokenSource.Token);


				progress?.Report(new DownloadProgress() { progress = 1, label = "Downloading Audio [0%]" });

				//Console.WriteLine("Downloading Audio");

				//downloading audio
				IStreamInfo audioStreamInfo = streamSelections.getAudioStream();//streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
				//Debug.WriteLine(audioStreamInfo.Container.ToString());
				//top quality streams are (from the looks of it) always webm's which are useless
				string audioPath = Path.Join(savePath, Mux.sanitizeFileName(streamSelections.video.Title) + "." + audioStreamInfo.Container.ToString());

				Progress<double> progressAudio = new Progress<double>(d => {
					progress?.Report(new DownloadProgress() { progress = 1, label = $"Downloading Audio [{(int)(d * 100)}%]" });					
				});

				await YoutubeClient.Videos.Streams.DownloadAsync(audioStreamInfo, audioPath, progressAudio, cancellationTokenSource.Token);
				Debug.WriteLine("finished audio download");
				Console.WriteLine($"Downloaded to {audioPath}");

				await Task.Delay(1000);
				progress?.Report(new DownloadProgress() { progress = 2, label = "Converting File Types" });
				Console.WriteLine($"Converting file types (.{audioStreamInfo.Container} -> .mp3)");
				Debug.WriteLine($"Converting file types (.{audioStreamInfo.Container} -> .mp3)");
				
				string outputPath = Path.Join(savePath, Mux.sanitizeFileName(streamSelections.video.Title) + ".mp3");
				//return;

				Mux mux = new Mux("", audioPath, outputPath);
				bool successful = await mux.startAudio(streamSelections.getAudioStreamBitrate());

				if(successful) {
					progress?.Report(new DownloadProgress() { progress = 3, label = "Complete" });

					Console.WriteLine($"Downloaded to {outputPath}");
				} else {
					progress?.Report(new DownloadProgress() { progress = 3, label = "Failed to Convert Audio" });

					Console.WriteLine("Failed to Convert Audio");
				}

			} catch (OperationCanceledException) {
				Debug.WriteLine("cancelled audio download");

			// for extra unexpected stuff
			} catch (Exception ex) {
				//I'll getcha, getcha, getcha!
				Debug.WriteLine(ex.ToString());
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
