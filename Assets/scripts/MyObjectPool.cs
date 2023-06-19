using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class MyObjectPool<T>where T : MonoBehaviour, IPoolable<T>
{
    private GameObject prefab;

    public MyObjectPool(GameObject _prefab, int _size, Transform _parent)
    {
        prefab = _prefab;
        for(int i = 0; i < _size; i++)
        {
            T temp = GameObject.Instantiate(_prefab, _parent).GetComponent<T>();
            temp.Initialize(this);
            ReturnItem(temp);
        }
    }

    private Queue<T> queue = new Queue<T>();

    public T GetItem()
    {
        T temp;

        if(queue.Count == 0)
        {
            return null;
        }

        temp = queue.Dequeue();
        temp.Activate();

        return temp;
    }

    public void ReturnItem(T _item)
    {
        _item.Deactivate();
        queue.Enqueue(_item);
    }
}