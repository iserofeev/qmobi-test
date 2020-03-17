using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    private Camera _mainCamera;

    private const int ForceMultiplier = 50;

    public int bulletForce;

    private Vector3 initPos = new Vector3(0, 0, 70);

    public Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = initPos;
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameOver)
        {
            CheckWorldConstrains();
            Fire();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.isGameOver)
        {
            Movement();

        }
    }

    private void Movement()
    {
        //Position
        var vertical = Input.GetAxis("Vertical") * ForceMultiplier;
        var horizontal = Input.GetAxis("Horizontal") * ForceMultiplier;
        
        var force = new Vector3(horizontal, vertical, 0);
        
        _rigidbody.AddForce(force, ForceMode.Force);
        
        
        //Rotation
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        float enter = 0.0f;

        if (GameManager.PlayerPlane.Raycast(ray, out enter))
        {
            //Get the point that is clicked
            Vector3 hitPoint = ray.GetPoint(enter);

            transform.LookAt(hitPoint, transform.up);
        }
        
    }

    private void Fire()
    {
        if (!Input.GetButtonDown("Fire1")) return;
        
        var bulletInstance = Instantiate(bullet);
        bulletInstance.transform.position = transform.position;

        bulletInstance.transform.rotation = transform.rotation;
        
        var bulletRb = bulletInstance.GetComponent<Rigidbody>();
        
        bulletRb.AddForce(transform.forward*bulletForce, ForceMode.VelocityChange);
        
        _rigidbody.AddForce(transform.forward*(-6), ForceMode.Impulse);

    }


    private void CheckWorldConstrains()
    {
        var transformPosition = transform.position;

        var outOfWorld = transformPosition.x > GameManager.maxWorldPoint.x ||
                transformPosition.x < GameManager.minWorldPoint.x ||
                transformPosition.y > GameManager.maxWorldPoint.y ||
                transformPosition.y < GameManager.minWorldPoint.y;
        if (outOfWorld)
        {
            GameManager.Instance.PlayerDamage();
            transform.position = initPos;
        }
    }
}
