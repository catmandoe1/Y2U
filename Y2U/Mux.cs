using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode.Videos.Streams;

namespace Y2U {
	public class Mux {
		private string videoPath = "";
		private string audioPath = "";
		private string outputPath = "";

		public Mux(string videoPath, string audioPath, string outputPath) {
			this.videoPath = videoPath;
			this.audioPath = audioPath;
			this.outputPath = outputPath;
		}

		/// <summary>
		/// Removes any invalid characters from inputted filepath like quotes.
		/// </summary>
		/// <param name="path"></param>
		/// <returns>filepath without any invalid characters</returns>
		public static string sanitizeFileName(string filename) {
			char[] invalidChars = Path.GetInvalidFileNameChars();
			//string filename = Path.GetFileName(path);
			//string? directory = Path.GetDirectoryName(path);

			//string validFilename = Regex.Replace(filename, $"[{Regex.Escape(new string(invalidChars))}]", "");
			//string validFilename = string.Concat(filename.Split(invalidChars));
			string validFilename = string.Concat(filename.Split(invalidChars));

			//if (directory != null) {
			//	return Path.Combine(directory, validFilename);
			//}

			// if directory root
			return validFilename;
		}

		/// <summary>
		/// checks if ffmpeg is available
		/// </summary>
		/// <returns>true if present and false if not</returns>
		public static async Task<bool> checkFFmpegAvailable() {
			try {
				using (Process process = new Process()) {
					process.StartInfo.FileName = "ffmpeg";
					process.StartInfo.Arguments = "";
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.RedirectStandardError = true;
					process.StartInfo.CreateNoWindow = true; // makes it look better
					process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();

					process.Start();
					await process.WaitForExitAsync();
				}

				return true;
			} catch {
				return false;
			}
		}
		public async Task<bool> startVideo(string bitrate) {
			return await start($"-i \"{this.videoPath}\" -i \"{this.audioPath}\" -c:v copy -c:a aac -b:a {bitrate} -strict experimental -y \"{this.outputPath}\"");
		}

		/// <summary>
		/// not exactly a "mux" but cant be bothered making new thing
		/// </summary>
		/// <returns></returns>
		public async Task<bool> startAudio(string bitrate) {
			Debug.WriteLine($"converting audio with target bitrate of {bitrate}");
			//bitrate = "48k";
			return await start($"-i \"{this.audioPath}\" -b:a {bitrate} -y \"{this.outputPath}\"");
		}

		/// <summary>
		/// Takes a video and audio file and "muxes" them into a single file using ffmpeg
		/// </summary>
		/// <returns>returns true if successful and false if not</returns>
		private async Task<bool> start(string operation) {
			try {
				Debug.WriteLine("muxing...");
				using (Process process = new Process()) {
					process.StartInfo.FileName = "ffmpeg";
					//process.StartInfo.Arguments = $"-i \"{this.videoPath}\" -i \"{this.audioPath}\" -c:v copy -c:a aac -strict experimental -y \"{this.outputPath}\"";
					process.StartInfo.Arguments = operation;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.RedirectStandardError = true;
					process.StartInfo.CreateNoWindow = true; // makes it look better
					process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();

					Debug.WriteLine("ffmpeg starting with args: ", process.StartInfo.Arguments);

					process.Start();

					//Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
					//Task<string> errorTask = process.StandardError.ReadToEndAsync();


					// To log the output
					using (StreamReader outputReader = process.StandardOutput)
					using (StreamReader errorReader = process.StandardError) {
						Task outputTask = Task.Run(async () => {
							while (!outputReader.EndOfStream) {
								string? line = await outputReader.ReadLineAsync();
								Debug.WriteLine(line);
								Console.WriteLine(line);
							}
						});

						Task errorTask = Task.Run(async () => {
							while (!errorReader.EndOfStream) {
								string? line = await errorReader.ReadLineAsync();
								Debug.WriteLine(line);
								Console.WriteLine(line);
							}
						});

						await Task.WhenAll(outputTask, errorTask);
					}
					//string outstr = await outputTask;
					//string errorstr = await errorTask;

					//Debug.WriteLine(outstr);
					//Debug.WriteLine(errorstr);
					//Console.WriteLine(outstr);
					//Console.WriteLine(errorstr);


					await process.WaitForExitAsync();
				}
				Debug.WriteLine("finished muxing");

				//foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory())) {
				//	if (file.EndsWith(".tmp")) {
				//		File.Delete(file);
				//	}
				//}

				if (File.Exists(videoPath)) File.Delete(videoPath);
				if (File.Exists(audioPath)) File.Delete(audioPath);

				Debug.WriteLine("deleted tmp files");
				return true;

				//File.Move(sanitizeFileName(this.outputPath), Regex.Escape(this.outputPath)); // rename file with original name
			} catch {
				Debug.WriteLine("failed to launch ffmpeg, check if its in PATH");
				Console.WriteLine("failed to launch ffmpeg, check if its in PATH");

				MessageBox.Show("FFmpeg failed to launch!\nMake sure FFmpeg is available in PATH (system environment variables) and relaunch.", "FFmpeg Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}
	}
}
