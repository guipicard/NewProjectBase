using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    private static string SaveFolder => $"{Application.persistentDataPath }/Saves/";

    private static string Extension => ".save";

    private static List<SaveData> m_SavesList = new List<SaveData>{ HasData(0), HasData(1), HasData(2) };

    public static SaveData HasData(int _index)
    {
        string saveName = $"Save{_index}{Extension}";
        string jsonData = "";

        if (!System.IO.File.Exists(SaveFolder + saveName))
        {
            return null;
        }
        try
        {
            jsonData = System.IO.File.ReadAllText(SaveFolder + saveName);
        }
        catch (Exception e)
        {
            Debug.Log($"[SAVEMANAGER] {e}");
            return null;
        }

        SaveData data = ScriptableObject.CreateInstance<SaveData>();
        JsonUtility.FromJsonOverwrite(jsonData, data);
        return data;
    }

    public static bool Save(SaveData _data, int _index)
    {
        string saveName = $"Save{_index}{Extension}";
        string jsonData = ""; 

        try
        {
            jsonData = JsonUtility.ToJson(_data);
        }
        catch(Exception e)
        {
            Debug.Log($"[SAVEMANAGER] {e}");
            return false;
        }
        if (!System.IO.Directory.Exists(SaveFolder))
        {
            m_SavesList[_index] = _data;
            System.IO.Directory.CreateDirectory(SaveFolder);
        }
        System.IO.File.WriteAllText(SaveFolder + saveName, jsonData);

        return true;
    }

    public static bool Load(SaveData data, int _index)
    {
        string saveName = $"Save{_index}{Extension}";
        string jsonData = "";

        if (!System.IO.File.Exists(SaveFolder + saveName))
        {
            Debug.Log($"[SAVEMANAGER] File you tried to load does not exist. Index: {_index}");
            return false;
        }
        try
        {
            jsonData = System.IO.File.ReadAllText(SaveFolder + saveName);
        }
        catch (Exception e)
        {
            Debug.Log($"[SAVEMANAGER] {e}");
            return false;
        }
        JsonUtility.FromJsonOverwrite(jsonData, data);
        m_SavesList[_index] = data;
        return true;
    }

    public static void NewSave(SaveData data, int _index)
    {
        if (m_SavesList[_index] != null)
        {
            Debug.Log("purged");
        }
        // set data
    }

    public static void DeleteSave(int _index)
    {
        m_SavesList[_index] = null;
        string saveName = $"Save{_index}{Extension}";
        System.IO.File.Delete(SaveFolder + saveName);
    }
}
