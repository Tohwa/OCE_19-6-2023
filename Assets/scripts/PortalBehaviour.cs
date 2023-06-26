using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject portalPrefab;

    private Vector3 drawVector;
    private Vector3[] corners = new Vector3[]
    {
        new Vector3(0.5f,1f,0f),
        new Vector3(-0.5f,1,0f),
        new Vector3(0.5f,-1f,0f),
        new Vector3(-0.5f,-1f,0f)
    };

    public void PlacePortal(LayerMask _targetLayer, Collision _wallCollision, Transform _pivot)
    {
        for(int i = 0; i<4; i++)
        {
            RaycastHit hit;
            Vector3 RaycastPosition = _pivot.TransformPoint(corners[i]);
            drawVector = RaycastPosition;
            if(!Physics.CheckSphere(RaycastPosition, 0.05f, _targetLayer))
            {
                return;
            }Debug.Log(RaycastPosition);
        }
        Instantiate(portalPrefab, _pivot.position, _wallCollision.transform.rotation);
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(drawVector, 10f);
    }

}
