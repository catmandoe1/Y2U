using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Y2U {
	public class SaveDataHandler {
		private static readonly string path = Directory.GetCurrentDirectory() + @"\save_data.json";

		public static void writeSaveData(SaveData saveData) {
			//string path = Directory.GetCurrentDirectory() + @"\save_data.json";

			string json = JsonSerializer.Serialize<SaveData>(saveData, new JsonSerializerOptions() { WriteIndented = true });

			File.WriteAllText(path, json);
		}

		public static SaveData? readSaveData() {
			try {
				string json = File.ReadAllText(path);

				return JsonSerializer.Deserialize<SaveData>(json);
			} catch {
				return null;
			}
		}
	}
}
