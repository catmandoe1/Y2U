using Microsoft.VisualBasic.ApplicationServices;
using System.Runtime.InteropServices;
using System.Threading;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace Y2U {
	internal static class Program {
		[DllImport("kernel32.dll")]
		static extern bool AttachConsole(int dwProcessId);
		private const int ATTACH_PARENT_PROCESS = -1;

		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.

			if (args.Length == 0) {
				RunForm();
			} else {
				// fixes Console.WriteLine()
				AttachConsole(ATTACH_PARENT_PROCESS);

				//NoGui noGui = new NoGui(args);
				//noGui.run().GetAwaiter().GetResult();
				if (args[0] == "-help" || args[0] == "/?" || args[0] == "-h") { 
					help();
					return;
				}

				bool noGUI = args.Last() == "-nogui";
				bool downloadThumbnail = false;
				int resolution = -1;
				string bitrate = null;

				string url = "";
				bool isVid = false;
				string output = Directory.GetCurrentDirectory();

				if (noGUI) {
					if (args.Length % 2 == 0) {
						Console.WriteLine("Missing/invalid amount of arguments");
					}
				} else {
					if (args.Length % 2 != 0) {
						Console.WriteLine("Missing/invalid amount of arguments");
					}
				}

				for (int i = 0; i < args.Length - 1; i += 2) {
					//Console.WriteLine(i + " " + args[i]);
					if (args[i] == "-video") {
						isVid = true;
						url = args[i + 1];

					} else if (args[i] == "-audio") {
						url = args[i + 1];

					} else if (args[i] == "-o") {
						output = args[i + 1];

					} else if (args[i] == "-vq") {
						string svq = args[i + 1];
						int vq;
						try {
							vq = int.Parse(svq);
							if (Resolutions.IsValidResolution(vq)) {
								resolution = vq;
							} else {
								Console.WriteLine($"Video quality \"{vq}\" is not a valid quality");
								return;
							}
						} catch {
							Console.WriteLine($"Video quality \"{svq}\" is not a number.");
							return;
						}

					} else if (args[i] == "-aq") {
						string aq = args[i + 1];

						if (Resolutions.IsValidBitrate(aq)) {
							bitrate = aq;
						} else {
							Console.WriteLine($"Audio quality \"{aq}\" is a not valid quality");
							return;
						}
					} else if (args[i] == "-dt") {
						downloadThumbnail = args[i + 1] == "true" || args[i + 1] == "1" || args[i + 1] == "t";
						//Console.WriteLine(args[i + 1] == "true" || args[i + 1] == "1" || args[i + 1] == "t");


					} else {
						Console.WriteLine($"Invalid argument {args[i]}");
					}
				}

				if (!noGUI) {
					// TODO add form thingy
					RunForm(url, output, isVid, resolution, bitrate, downloadThumbnail);
				} else {
					YoutubeDownload youtube = new YoutubeDownload();
					Console.WriteLine("Getting video details");
					Video video = youtube.YoutubeClient.Videos.GetAsync(url).Result;
					StreamManifest streamManifest = youtube.YoutubeClient.Videos.Streams.GetManifestAsync(url).Result;
					DownloadSelection selection = new DownloadSelection(streamManifest, video, downloadThumbnail);

					selection.overrideAudioSelection = bitrate;
					selection.selectedVideoStreamKey = resolution;

					NoGui nogui = new NoGui(url, output, selection);
					if (isVid) {
						nogui.video().GetAwaiter().GetResult();
					} else {
						nogui.audio().GetAwaiter().GetResult();
					}
				}

				//if (args[0] != "-video" && args[0] != "-audio") {
				//	Console.WriteLine($"Invalid argument: {args[0]}");
				//	return; }



				//if ((noGUI && args.Length < 3) || (!noGUI && args.Length < 2)) {
				//	Console.WriteLine("Missing arguments");
				//	return;
				//}

				//bool isVid = args[0] == "-video";
				//string url = args[1];
				//string outputPath;

				//// -video url -nogui
				//if (noGUI) {
				//	if (args.Length < 4) {
				//		outputPath = Directory.GetCurrentDirectory();
				//		//  -video url path -nogui
				//	} else {
				//		outputPath = args[2];
				//	}
				
				//// -video url
				//} else {
				//	if (args.Length < 3) {
				//		outputPath = Directory.GetCurrentDirectory();
				//		// -video url path
				//	} else {
				//		outputPath = args[2];
				//	}
				//}
				
				//if (!noGUI) {
				//	RunForm(url, outputPath, isVid);
				//} else {
				//	NoGui nogui = new NoGui(url ,outputPath);
				//	if (isVid) {
				//		nogui.video().GetAwaiter().GetResult();
				//	} else {
				//		nogui.audio().GetAwaiter().GetResult();
				//	}
				//}
			}
		}

		public static void RunForm() {
			ApplicationConfiguration.Initialize();
			Application.Run(new Form1());
		}

		public static void RunForm(string url, string outputPath, bool isVid, int resolution, string bitrate, bool downloadThumbnail) {
			ApplicationConfiguration.Initialize();
			Application.Run(new Form1(url, outputPath, isVid, resolution, bitrate, downloadThumbnail));
		}

		public static void help() {
			Console.WriteLine("\nUsage: Y2U.exe -video | -audio <youtube_url> [options]\n");

			Console.WriteLine("Description:");
			Console.WriteLine("    Y2U.exe is a tool to download videos and audio from YouTube.\n");

			Console.WriteLine("Options:");
			Console.WriteLine("    -help | -h | /?               Show this help message and exit");
			Console.WriteLine("    -video <youtube_url>          Download video");
			Console.WriteLine("    -audio <youtube_url>          Download audio");
			Console.WriteLine("    -vq <video_quality>           Selects the video quality to download, defaults to highest if given not available (optional).");
			Console.WriteLine("                                        Quality    Description");
			Console.WriteLine($"                                        {Resolutions.UHD}       Ultra HD / 4k / 2160p");
			Console.WriteLine($"                                        {Resolutions.WHD}       Wide HD / 1440p");
			Console.WriteLine($"                                        {Resolutions.HD}       HD / 1080p");
			Console.WriteLine($"                                        {Resolutions.LHD}        HD / 720p");
			Console.WriteLine($"                                        {Resolutions.SD}        SD / 480p");
			Console.WriteLine($"                                        {Resolutions.LD}        Low Definition / 360p");
			Console.WriteLine($"                                        {Resolutions.VLD}        Very Low Definition / 240p");
			Console.WriteLine($"                                        {Resolutions.XLD}        Extremely Low Definition / 144p\n");

			Console.WriteLine("    -aq <audio_quality>           Selects the audio quality to download, can be used with a video download (optional).");
			Console.WriteLine("                                        Bitrate (kbps)");
			Console.WriteLine($"                                        {Resolutions.AUDIO_HIGH}");
			Console.WriteLine($"                                        {Resolutions.AUDIO_MEDIUM}");
			Console.WriteLine($"                                        {Resolutions.AUDIO_LOW}");
			Console.WriteLine($"                                        {Resolutions.AUDIO_VERY_LOW}\n");

			Console.WriteLine("    -o <directory>                Specifies the output directory, selected current directory if left blank (optional)");
			Console.WriteLine("    -dt <true | false>            Download or not the video's thumbnail (default = false, optional)");
			Console.WriteLine("    -nogui                        Use no graphical user interface (MUST be the LAST argument & optional)\n");

			Console.WriteLine("Examples:");
			Console.WriteLine("    Video download (specific directory):");
			Console.WriteLine("        Y2U.exe -video \"full youtube url\" -o \"C:/output/directory/\"\n");

			Console.WriteLine("    Audio download (specific directory):");
			Console.WriteLine("        Y2U.exe -audio \"full youtube url\" -o \"C:/output/directory/\"\n");

			Console.WriteLine("    Download to current directory:");
			Console.WriteLine("        Y2U.exe -video \"full youtube url\"\n");

			Console.WriteLine("    Video download (specific quality of 480p):");
			Console.WriteLine($"        Y2U.exe -video \"full youtube url\" -vq {Resolutions.SD}\n");

			Console.WriteLine("    Audio download (specific quality of 128kbps):");
			Console.WriteLine($"        Y2U.exe -video \"full youtube url\" -vq {Resolutions.AUDIO_MEDIUM}\n");

			Console.WriteLine("    Video and thumbnail download (current directory):");
			Console.WriteLine("        Y2U.exe -video \"full youtube url\" -dt true\n");

			Console.WriteLine("    No GUI:");
			Console.WriteLine("        Y2U.exe -video \"full youtube url\" -nogui\n");

			// OLD METHOD BELOW!
			//Console.WriteLine("help:");
			//Console.WriteLine("Y2U.exe -help");
			//Console.WriteLine();
			////Console.WriteLine("");
			//Console.WriteLine();
			//Console.WriteLine("video download example:");
			//Console.WriteLine("Y2U.exe -video \"full youtube url\" -o \"C:/output/directory/\"");
			//Console.WriteLine();
			//Console.WriteLine("audio download example:");
			//Console.WriteLine("Y2U.exe -audio \"full youtube url\" -o \"C:/output/directory/\"");
			//Console.WriteLine();
			//Console.WriteLine("remove \"-o\" to select current directory");
			//Console.WriteLine("Y2U.exe -video \"full youtube url\" -o \"C:/output/directory/\"");
			//Console.WriteLine();
			//Console.WriteLine("add \"-nogui\" for no gui (must be the LAST argument)");
			//Console.WriteLine("output directory can be left blank to select current directory");


			// Y2U.exe -video "url" "path"

			// basic usage would be
			// Y2U.exe - video "url" "path"
			// but short hand can be used for
			// Y2U.exe -video "url" which would take the missing path as current directory.
			// theres a nogui flag that if included is always at the end of the args
			// Y2U.exe -video "url" "path" -nogui
			// this works also for shorthand
			// Y2U.exe -video "url" -nogui
		}
	}
}