using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable<T>where T : MonoBehaviour, IPoolable<T>
{
    public void Initialize(MyObjectPool<T> pool);

    public void Deactivate();

    public void Activate();

}
