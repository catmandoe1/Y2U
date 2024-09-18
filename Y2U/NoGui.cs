using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y2U {
	public class NoGui {
		//private string[] args;
		private YoutubeDownload youtube;
		private string url;
		private string outputPath;
		private DownloadSelection selection;
		private Progress<DownloadProgress> progress;
		private string prevLabel = "";


		//public NoGui(string[] args) {
		//	if (args.Length < 1) {
		//		throw new ArgumentException("arguments cant be empty");
		//	}
		//	this.args = args;
		//	youtube = new YoutubeDownload();
		//}

		//public async Task run() {
		//	if (args[0] == "-help" || args[0] == "/?") { help(); return; }
		//	if (args[0] == "-video") { await video(); return; }
		//	if (args[0] == "-audio") { await audio(); return; }
		//	Console.WriteLine("Unknown arguments");
		//}

		//private void checkArgs() {
		//	if (args[1] != "-url") { throw new ArgumentException($"unknown argument: {args[1]}"); }
		//	if (args[3] != "-out") { throw new ArgumentException($"unknown argument: {args[3]}"); }
		//}

		/// <summary>
		/// Console interface so the program can be ran without the need for a GUI. Handy for use in a batch script
		/// </summary>
		public NoGui(string url, string outputPath, DownloadSelection selection) {
			youtube = new YoutubeDownload();
			this.url = url;
			this.outputPath = outputPath;
			this.selection = selection;

			this.progress = new Progress<DownloadProgress>(p => {
				if (p.label != prevLabel) {
					Console.WriteLine($"stage: {p.progress} - {p.label}");
					prevLabel = p.label;
				}
			});
		}


		public async Task video() {
			if (!await Mux.checkFFmpegAvailable()) { Console.WriteLine("Failed to launch FFmpeg, check if its in PATH"); return; }

			string videoPath = "";
			string audioPath = "";

			try {
				videoPath = Path.GetTempFileName();
				audioPath = Path.GetTempFileName();
			} catch (IOException) {
				Console.WriteLine("Failed to create temporary files for video and/or audio");
			}
			Console.WriteLine($"Downloading video from {url}");

			await youtube.downloadVideo(progress, this.selection, videoPath, audioPath, outputPath, new CancellationTokenSource());
		}

		public async Task audio() {
			if (!await Mux.checkFFmpegAvailable()) { Console.WriteLine("Failed to launch FFmpeg, check if its in PATH"); return; }

			Console.WriteLine($"Downloading audio from {url}");
			await youtube.downloadAudio(progress, this.selection, outputPath, new CancellationTokenSource());
		}
	}
}
