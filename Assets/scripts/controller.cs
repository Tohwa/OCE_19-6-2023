using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class controller : MonoBehaviour
{
    #region Fields
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _target;
    //[SerializeField] private Animator _animator;
    private MyObjectPool<Bullet> bulletPool;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private int poolSize = 1;
    private bool fire = false;

    private float prefX;
    private float prefZ;

    private float deltax;
    private float deltaz;

    private Vector2 orbitInput;
    private Vector2 movementInput;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        bulletPool = new MyObjectPool<Bullet>(_bulletPrefab, poolSize,this.transform);
    }

    private void Update()
    {
        // Bewegung
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y) * movementSpeed * Time.deltaTime;
        _rb.MovePosition(transform.position + transform.TransformDirection(movement));

        /*
        prefX = _target.transform.position.x;
        prefZ = _target.transform.position.z;

        deltax = prefX -= _target.transform.position.x;
        deltaz = prefZ -= _target.transform.position.z;

        if (deltax != 0 || deltaz != 0)
        {
            _animator.SetFloat("speed", 1);
        }
        */

        if(fire)
        {
            Bullet temp = bulletPool.GetItem();
            if (temp is not null)
            temp.transform.position = _target.transform.position;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void TurnCharacter(InputAction.CallbackContext ctx)
    {
        orbitInput = ctx.ReadValue<Vector2>();
    }

    public void ShootBullet(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            fire = true;
        }
        else if(ctx.canceled)
        {
            fire = false;
        }
    }
}
