using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    private Plane _plane;
    
    private Camera _mainCamera;

    private const int ForceMultiplier = 40;

    public int bulletForce;

    public Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        _plane = new Plane(Vector3.back, transform.position);
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void FixedUpdate()
    {
        Movement();
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

        if (_plane.Raycast(ray, out enter))
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

    }
}
