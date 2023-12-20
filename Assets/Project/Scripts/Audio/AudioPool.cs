using System.Collections.Generic;
using UnityEngine;

public class AudioPool
{
    private Transform musicParent;
    private AudioSource musicPrefab;
    private readonly AudioSource musicSource;
    private readonly Transform parent;
    private readonly Queue<AudioSource> pool;
    private readonly AudioSource prefab;

    public AudioPool(AudioSource _prefab, AudioSource _musicPrefab, int _size, Transform _parent,
        Transform _musicParent)
    {
        parent = _parent;
        musicParent = _musicParent;
        pool = new Queue<AudioSource>();
        prefab = _prefab;
        musicPrefab = _musicPrefab;
        for (var i = 0; i < _size; i++)
        {
            var instance = Object.Instantiate(prefab, parent);
            instance.spatialize = true;
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }

        musicSource = Object.Instantiate(_musicPrefab, _musicParent);
        musicSource.gameObject.SetActive(false);
    }

    public AudioSource GetPooledObject()
    {
        var initialPoolCount = pool.Count;

        AudioSource instance = null;
        for (var i = 0; i < initialPoolCount; i++)
        {
            instance = pool.Dequeue();
            if (!instance.isPlaying)
            {
                instance.gameObject.SetActive(true);
                return instance;
            }

            pool.Enqueue(instance);
            instance = null;
        }

        if (instance == null)
        {
            instance = Object.Instantiate(prefab, parent);
            pool.Enqueue(instance);
        }

        instance.gameObject.SetActive(true);
        return instance;
    }

    public AudioSource GetMusicSource()
    {
        musicSource.gameObject.SetActive(true);
        return musicSource;
    }


    public void ReturnPooledObject(AudioSource instance)
    {
        instance.clip = null;
        instance.time = 0.0f;
        instance.gameObject.SetActive(false);
        pool.Enqueue(instance);
    }
}