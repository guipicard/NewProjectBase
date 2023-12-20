using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static bool isInit { get; private set; }
    public static bool isClosed {get; private set;}
    public static event Action onInitialized;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<T>();
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
            isClosed = false;

            Initilazed();

            DontDestroyOnLoad(gameObject);

        }
    }

    private void Initilazed()
    {
        onInitialized?.Invoke();
        isInit = true;

        onInitialized = null;
    }

    protected virtual void OnDestroy()
    {
        isClosed = true;
        isInit = false;
    }
}
