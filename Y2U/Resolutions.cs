using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y2U {
	public class Resolutions {
		public const int UHD = 2160; //	ultra high definition
		public const int WHD = 1440; //	wide high definition
		public const int HD = 1080; //	high definition
		public const int LHD = 720; //	low - high definition
		public const int SD = 480; //	standard definition
		public const int LD = 360; //	low definition
		public const int VLD = 240;	//	very low definition
		public const int XLD = 144; //	extremely low definition

		public static List<int> resolutions = new List<int>() {
			UHD, WHD, HD, LHD, SD, LD, VLD, XLD
		};

		//audio bitrates - not exactly a resolution but its fine
		public const string AUDIO_HIGH = "192k";
		public const string AUDIO_MEDIUM = "128k";
		public const string AUDIO_LOW = "96k";
		public const string AUDIO_VERY_LOW = "48k";

		public static List<string> bitrates = new List<string>() {
			AUDIO_HIGH, AUDIO_MEDIUM, AUDIO_LOW, AUDIO_VERY_LOW
		};

		public static bool IsValidResolution(int resolution) {
			return resolutions.Contains(resolution);
		}

		/// <summary>
		/// just works
		/// </summary>
		/// <param name="bitrate">format of "_k" eg. "192k"</param>
		/// <returns></returns>
		public static bool IsValidBitrate(string bitrate) {
			return bitrates.Contains(bitrate);
		}
	}
}
