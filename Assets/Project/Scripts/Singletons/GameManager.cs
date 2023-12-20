using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public SaveData m_SaveData;
    public Action m_OnSave;
    public Action m_OnLoad;
    public int m_SaveIndex;

    protected override void Awake()
    {
        base.Awake();  // just in case
    }

    private void Save(SaveData data, int _index)
    {
        m_SaveIndex = _index;  
        m_OnSave?.Invoke();
        // data to set
        bool success = SaveManager.Save(data, m_SaveIndex);
        if (success)
        {
            Debug.Log("Save Succeed ! !");
        }
        else
        {
            Debug.Log("Save Failed.");
        }
    }

    private void Load(SaveData data, int _index)
    {
        bool success = SaveManager.Load(data, _index);
        if (success)
        {
            m_OnLoad?.Invoke();
            // set by data
            Debug.Log("Load Succeed ! !");
        }
        else
        {
            Debug.Log("Load Failed.");
        }
    }

    public SaveData NewGame(int _index)
    {
        
        m_SaveIndex = _index;
        SaveManager.NewSave(m_SaveData, _index);
        Save(m_SaveData, _index);
        StartCoroutine(LoadScene(_index));
        return m_SaveData;
    }

    public int GetIndex()
    {
        return m_SaveIndex;
    }

    public void QuickSave()
    {
        Save(m_SaveData, m_SaveIndex);
    }

    public void QuickLoad()
    {
        Load(m_SaveData, m_SaveIndex);
    }

    public IEnumerator LoadScene(int _index)
    {
        m_SaveIndex = _index;
        string currentScene = SceneManager.GetActiveScene().name;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_SaveData.mapName, LoadSceneMode.Additive);
        bool done = true;
        while (done)
        {
            if (asyncLoad.isDone)
            {
                Load(m_SaveData, m_SaveIndex);
                SceneManager.UnloadSceneAsync(currentScene);
                done = false;
            }
            else
            {
                yield return null;
            }
        }
    }

    public bool HasSave(int _index)
    {
        if (SaveManager.HasData(_index) == null)
        {
            return false;
        }

        return true;
    }

    public void DeleteSave(int _index)
    {
        SaveManager.DeleteSave(_index);
    }
}