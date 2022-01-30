using System.IO;
using System.Text;
using UnityEngine;

namespace Asteroids
{
    public class JsonSaveSystem : ISaveSystem
    {
        private readonly string _path;

        public JsonSaveSystem(string fileName)
        {
            _path = CreatePath(fileName);
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data, prettyPrint: true);
            using (var writer = new StreamWriter(_path))
            {
                writer.WriteLine(json);
            }
        }

        public SaveData Load()
        {
            if (!File.Exists(_path)) return new SaveData();

            string json = "";
            using (var reader = new StreamReader(_path))
            {
                json = reader.ReadToEnd();
            }
            return JsonUtility.FromJson<SaveData>(json.ToString());
        }

        private string CreatePath(string fileName)
        {
            string directory = Path.Combine(Application.dataPath, "Data");
            Directory.CreateDirectory(directory);
            return Path.Combine(directory, fileName);
        }
    }
}
