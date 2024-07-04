using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    private Dictionary<System.Type, object> pools;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        pools = new Dictionary<System.Type, object>();
    }

    public void CreatePool<T>(T prefab, int initialSize, Transform parent = null) where T : MonoBehaviour, IPoolable
    {
        if (!pools.ContainsKey(typeof(T)))
        {
            ObjectPool<T> pool = new ObjectPool<T>(prefab, initialSize, parent);
            pools[typeof(T)] = pool;
        }
    }

    public T GetObject<T>() where T : MonoBehaviour, IPoolable
    {
        if (pools.ContainsKey(typeof(T)))
        {
            ObjectPool<T> pool = (ObjectPool<T>)pools[typeof(T)];
            return pool.Get();
        }

        Debug.LogWarning($"Pool for type {typeof(T)} does not exist.");
        return null;
    }

    public void ReturnObject<T>(T obj) where T : MonoBehaviour, IPoolable
    {
        if (pools.ContainsKey(typeof(T)))
        {
            ObjectPool<T> pool = (ObjectPool<T>)pools[typeof(T)];
            pool.Return(obj);
        }
        else
        {
            Debug.LogWarning($"Pool for type {typeof(T)} does not exist.");
            Destroy(obj.gameObject);
        }
    }
}
