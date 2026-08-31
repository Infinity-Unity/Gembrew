using System.IO;
using UnityEngine;



public static class SaveManager
{
    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        File.WriteAllText(path,json);
        Debug.Log($"Сохранено: {json} в {path}");
    }

    public static SaveData Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
 
        if (!File.Exists(path))
        {
            Debug.Log("Указаного файла не существует!");
            return null;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"Загружено: {data.gold} из {json} из папки {path}");
        return data;
    }
}
