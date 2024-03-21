using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProjectManager.Models;
using ProjectManager.JSON_Utils;
using ProjectManager.Data;

namespace ProjectManager.JSON_Utils
{
    public static class JsonFunctions
    {

		

		public static bool ModExistJson(string fileName)
		{
			try
			{
				using (FileStream jsonwFile = File.OpenRead(fileName))
				{
					JsonSerializer.Deserialize<string>(jsonwFile);
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static async Task ModCreateJson(string fileName, StateAndSettings stateAndSettings)
		{
			using (FileStream newPath = File.Create(fileName)) 
            {
				string jsonString = JsonSerializer.Serialize(stateAndSettings);

			};

			using (FileStream file = File.OpenWrite(fileName))
			{
				var options = new JsonSerializerOptions { WriteIndented = true };
				JsonSerializer.Serialize(file, stateAndSettings);

			}
		}
		public static StateAndSettings ModReadJson(string fileName)
		{
			try
			{
				using (FileStream newFile = File.OpenRead(fileName))
				{
                    var jsonFile = JsonSerializer.Deserialize<Dictionary<string,int>>(newFile);

                    var stateAndSettings = new StateAndSettings()
                    {
                        ActiveJob = jsonFile["ActiveJob"],
                        ActivePerson = jsonFile["ActivePerson"],

                    };
					return stateAndSettings;
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

			}

			
			return new StateAndSettings();
		}

		public static async Task ModSaveJson(string fileName, StateAndSettings stateAndSettings)
        {
            if(File.Exists(fileName))
			{

				using (FileStream file = File.OpenWrite(fileName))
				{
					var options = new JsonSerializerOptions { WriteIndented = true };
					JsonSerializer.Serialize(file, stateAndSettings);

				}

			}
        }


		/* OLD */
		public static bool ExistJsonFile(string fileName)
        {
            try
            {
                using (FileStream newFile = File.OpenRead(fileName))
                {
                    JsonSerializer.Deserialize<Dictionary<string, string>>(newFile);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task CreateJsonFile(string fileName)
        {
            using (FileStream newPath = File.Create(fileName)) { };
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["active"] = "1";
            using (FileStream file = File.OpenWrite(fileName))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.Serialize(file, dic);

            }
        }

        public static Dictionary<string, string> ReadJsonFile(string fileName)
        {
            try
            {
                using (FileStream newFile = File.OpenRead(fileName))
                {
                    return JsonSerializer.Deserialize<Dictionary<string, string>>(newFile);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            Dictionary<string, string> dic = new Dictionary<string, string>()
            {{"message", "File not found."}};
            return dic;
        }

        public static void AddToJsonFile(string fileName, Dictionary<string, string> dictionary)
        {


            Dictionary<string, string> ExistentDic()
            {
                Dictionary<string, string> existentDic = new Dictionary<string, string>();
                using (FileStream file = File.OpenRead(fileName))
                {

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var existing = JsonSerializer.Deserialize<Dictionary<string, string>>(file);
                    existentDic = existing;

                }
                return existentDic;
            }

            Dictionary<string, string> dicToSave = ExistentDic();

            using (FileStream file = File.OpenWrite(fileName))
            {

                foreach (var kv in dictionary)
                {
                    dicToSave[kv.Key] = kv.Value;
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.Serialize(file, dicToSave);

            }

        }

        public static void ChangeActive(string fileName, int id)
        {

            Dictionary<string, string> dicToSave = new Dictionary<string, string>();
            void ExistentDic()
            {
                Dictionary<string, string> existentDic = new Dictionary<string, string>();
                using (FileStream file = File.OpenRead(fileName))
                {

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var existing = JsonSerializer.Deserialize<Dictionary<string, string>>(file);
                    dicToSave = existing;

                }
            }

            using (FileStream file = File.OpenWrite(fileName))
            {
                dicToSave["active"] = id.ToString();

                var options = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.Serialize(file, dicToSave);

            }

        }
    }

}
