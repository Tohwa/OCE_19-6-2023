using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable<Bullet>
{
    private MyObjectPool<Bullet> pool;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPivot;
    [SerializeField] private PortalBehaviour portalBehaviour;

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

        if (CompareLayer(collision.gameObject.layer, targetLayer))
        {
            PortalSpawn(collision);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void PortalSpawn(Collision _wallCollision)
    {
        //Instantiate(portalPrefab, bulletPivot.transform.position, _wallCollider.transform.rotation);
        portalBehaviour.PlacePortal(targetLayer, _wallCollision, bulletPivot.transform);
    }

    private bool CompareLayer(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
