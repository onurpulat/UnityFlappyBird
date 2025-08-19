using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Behaviour
{
    private T _prefab;

    private Transform _parent;
    private int _maxSize;
    private bool _canExpand;

    private Queue<T> _pool;

    public ObjectPool(T prefab, Transform parent, int initialSize = 10, int maxSize = 100, bool canExpand = true)
    {
        _prefab = prefab;
        _parent = parent;
        _maxSize = maxSize;
        _canExpand = canExpand;

        _pool = new Queue<T>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateInstance();
        }
    }

    private void CreateInstance()
    {
        if (!_canExpand && _pool.Count >= _maxSize)
        {
            Debug.LogWarning("Object pool has reached its maximum size.");
        }

        T instance = Object.Instantiate(_prefab, _parent);
        ReturnToPool(instance);
    }

    public T GetFromPool()
    {
        if (_canExpand && _pool.Count == 0)
        {
            CreateInstance();
        }

        if (_pool.Count > 0)
        {
            T instance = _pool.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            Debug.LogWarning("Object pool is empty. Consider expanding the pool.");
            return null;
        }
    }

    public void ReturnToPool(T instance)
    {
        instance.gameObject.SetActive(false);

        if (_pool.Count < _maxSize)
        {
            _pool.Enqueue(instance);
        }
        else
        {
            Debug.LogWarning("Object pool has reached its maximum size. Destroying the object.");
            Object.Destroy(instance.gameObject);
        }
    }
}
