using System.IO;
using UnityEngine;

namespace SaveLoaderProject
{
    public class FileSaveLoader
    {
       
        public void SaveToFile(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                Debug.LogWarning("Data is empty. File is not saved");
                return;
            }

            string dir = Application.persistentDataPath + ConstVars.directory;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string fullPath = Path.Combine(dir, ConstVars.fileName);
            File.WriteAllText(fullPath, data);
            Debug.Log("file saved to : " + dir);
        }

        public string LoadFromFile()
        {
            string data = "";
            string dir = Application.persistentDataPath + ConstVars.directory;
            string fullPath = Path.Combine(dir, ConstVars.fileName);

            if (File.Exists(fullPath))
            {
                data = File.ReadAllText(fullPath);
            }
            return data;
        }
    }

}

