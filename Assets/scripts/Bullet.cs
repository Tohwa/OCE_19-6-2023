using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable<Bullet>
{
    private MyObjectPool<Bullet> pool;

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(MyObjectPool<Bullet> _pool)
    {
        pool = _pool;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(pool != null)
        {
            pool.ReturnItem(this);
        }
        Debug.Log("test");
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
