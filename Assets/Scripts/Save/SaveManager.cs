using System.IO;
using UnityEngine;



public static class SaveManager
{
    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        File.WriteAllText(path,json);
    }

    public static SaveData Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
 
        if (!File.Exists(path))
        {
            return null;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data;
    }
}
