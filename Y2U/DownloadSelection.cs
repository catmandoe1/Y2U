using AngleSharp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace Y2U {
	public class DownloadSelection {
		public Dictionary<int, VideoOnlyStreamInfo> videoStreams { get; set; }
		public List<AudioOnlyStreamInfo> audioStreams { get; set; }

		[Obsolete]
		public static Dictionary<int, string> VideoToAudioBitrates { get; } = new Dictionary<int, string> {
			{Resolutions.UHD, "384k"},
			{Resolutions.WHD, "256k"},
			{Resolutions.HD, "192k"},
			{Resolutions.LHD, "192k"},
			{Resolutions.SD, "128k"},
			{Resolutions.LD, "96k"},
			{Resolutions.VLD, "48k"},
			{Resolutions.XLD, "48k"}
		};

		public static List<double> AudioBitrates { get; } = new List<double> {
			48, 96, 128, 192, 256, 384
		};

		public IVideoStreamInfo highestQualityVideoStream { get; set; }
		public IStreamInfo highestQualityAudioStream { get; set; }
		public bool useHighestQualityVid { get; set; }
		public bool useHighestQualityAud { get; set; }
		public int selectedAudioStreamIndex { get; set; }
		public int selectedVideoStreamKey { get; set; }
		public string overrideAudioSelection { get; set; }
		public StreamManifest manifest { get; set; }
		public Video video { get; set; }

		public DownloadSelection() {
			this.videoStreams = new Dictionary<int, VideoOnlyStreamInfo>();
			this.audioStreams = new List<AudioOnlyStreamInfo>();

			this.useHighestQualityVid = false;
			this.useHighestQualityAud = false;
			this.selectedAudioStreamIndex = -1;
			this.selectedVideoStreamKey = -1;
		}

		public List<AudioOnlyStreamInfo> sortAudioStreams(IEnumerable<AudioOnlyStreamInfo> audioStreams) {
			return audioStreams.ToList().OrderByDescending(aud => aud.Bitrate).ToList(); // ???
		}

		public DownloadSelection(StreamManifest manifest, Video video) : this() {
			this.manifest = manifest;
			this.video = video;

			IEnumerable<VideoOnlyStreamInfo> videoStreams = manifest.GetVideoOnlyStreams();
			List<AudioOnlyStreamInfo> audioStreams = sortAudioStreams(manifest.GetAudioOnlyStreams());

			this.highestQualityVideoStream = videoStreams.GetWithHighestVideoQuality();
			this.highestQualityAudioStream = audioStreams.GetWithHighestBitrate();

			// gather and sort streams
			foreach (VideoOnlyStreamInfo vs in videoStreams) {
				// filter only mp4 streams
				if (vs.Container != YoutubeExplode.Videos.Streams.Container.Mp4) {
					continue;
				}

				Debug.WriteLine($"{vs} - {vs.VideoResolution}, {vs.Bitrate}, {vs.Size}");

				switch (vs.VideoQuality.MaxHeight) {
					case Resolutions.UHD:
						if (this.videoStreams.ContainsKey(Resolutions.UHD)) { break; }

						this.videoStreams?.Add(2160, vs);
						break;
					case Resolutions.WHD:
						if (this.videoStreams.ContainsKey(Resolutions.WHD)) { break; }

						this.videoStreams?.Add(1440, vs);
						break;
					case Resolutions.HD:
						if (this.videoStreams.ContainsKey(Resolutions.HD)) { break; }

						this.videoStreams?.Add(1080, vs);
						break;
					case Resolutions.LHD:
						if (this.videoStreams.ContainsKey(Resolutions.LHD)) { break; }

						this.videoStreams?.Add(720, vs);
						break;
					case Resolutions.SD:
						if (this.videoStreams.ContainsKey(Resolutions.SD)) { break; }

						this.videoStreams?.Add(480, vs);
						break;
					case Resolutions.LD:
						if (this.videoStreams.ContainsKey(Resolutions.LD)) { break; }

						this.videoStreams?.Add(360, vs);
						break;
					case Resolutions.VLD:
						if (this.videoStreams.ContainsKey(Resolutions.VLD)) {
							break;
						}

						this.videoStreams?.Add(240, vs);
						break;
					case Resolutions.XLD:
						if (this.videoStreams.ContainsKey(Resolutions.XLD)) {
							break;
						}

						this.videoStreams?.Add(144, vs);
						break;
					default:
						Debug.WriteLine(vs.ToString() + " is not valid");
						break;
				}

				foreach (AudioOnlyStreamInfo audioStream in audioStreams) {
					Debug.WriteLine($"{audioStream} - {audioStream.Bitrate.KiloBitsPerSecond}");
					this.audioStreams?.Add(audioStream);
				}
			}
		}

		public IStreamInfo? getAudioStream() {
			if (this.useHighestQualityAud || this.overrideAudioSelection != null) {
				return this.highestQualityAudioStream;
			}
			
			if (this.selectedAudioStreamIndex >= this.audioStreams.Count) { return null; }
			//Debug.WriteLine()

			if (this.selectedAudioStreamIndex != -1) {
				return this.audioStreams[this.selectedAudioStreamIndex];
			} else {
				return this.highestQualityAudioStream;
			}
		}

		// realized this is completely wrong
		//public string getAudioStreamBitrate() {
		//	if (!this.videoStreams.ContainsKey(this.selectedVideoStreamKey)) { return AudioSampleRates[Resolutions.HD]; }

		//	if (this.selectedVideoStreamKey != -1) {
		//		return AudioSampleRates[this.selectedVideoStreamKey];
		//	} else {
		//		return AudioSampleRates.First().Value;
		//	}
		//}

		public static double findClosestBitrate(double bitrate) {
			foreach (double br in AudioBitrates) {
				Debug.WriteLine($" comparing {br} >= {bitrate}");
				if (br >= bitrate) { return br; }
			}
			return AudioBitrates.Last(); // doubt it will ever get here
		}

		public string getAudioStreamBitrate() {
			if (this.overrideAudioSelection != null) {
				return this.overrideAudioSelection;
			}

			IStreamInfo? audioStream = this.getAudioStream();
			if (this.selectedAudioStreamIndex == this.audioStreams.Count - 1) {
				return AudioBitrates[0].ToString() + "k";
			}

			if (audioStream != null) {
				return $"{findClosestBitrate(audioStream.Bitrate.KiloBitsPerSecond)}k";
			}
			Debug.WriteLine("audioStream was null!!!!!!!!!!!!!!! defaulting to 192k");
			return AudioBitrates[4].ToString() + "k"; // 192k
		}

		/// <summary>
		/// if useHighestQualityVid then returns highest quality stream,<br></br>
		/// else returns the selected stream or the highest quality stream if none selected
		/// </summary>
		/// <returns>A video stream based on the specified quality preference or null if the selected stream is not available.</returns>
		public IVideoStreamInfo GetVideoStream() {
			if (this.useHighestQualityVid) {
				return this.highestQualityVideoStream;
			}

			if (!this.videoStreams.ContainsKey(this.selectedVideoStreamKey) && this.selectedVideoStreamKey != -1 && this.videoStreams.Count > 0) {
				return videoStreams.First().Value;
			}

			if (this.selectedVideoStreamKey != -1) {
				return this.videoStreams[this.selectedVideoStreamKey];
			} else {
				return this.highestQualityVideoStream;
			}
		}
	}
}
