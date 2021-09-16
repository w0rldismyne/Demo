using UnityEngine;
using System.IO;

namespace Lucerna.Utils
{
    public class JsonSerializer
    {
        public static void SaveData<T>(T data, string path, string fileName) {
            path = Application.persistentDataPath + "/" + path;
            fileName = path + "/" + fileName + ".json";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (!File.Exists(fileName)) File.Create(fileName).Close();

            string contents = JsonUtility.ToJson(data);
            File.WriteAllText(fileName, contents);
        }

        public static T ReadData<T>(string path) {
            path = Application.persistentDataPath + path + ".json";
            if (!File.Exists(path)) throw new IOException("CustomJson::ReadData() --- File does not exist at " + path);

            string contents = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(contents);
        }

        public static void Delete(string path) {
            path = Application.persistentDataPath + path + ".json";
            if (File.Exists(path)) File.Delete(path);
        }
    }
}

