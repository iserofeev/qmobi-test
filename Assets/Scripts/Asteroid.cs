using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    private Rigidbody rb;

    public bool isBig;
    
    public List<Color> colors = new List<Color>();
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddTorque(Random.insideUnitSphere, ForceMode.VelocityChange);
        
        var x = Random.Range(GameManager.minWorldPoint.x*1.1f, GameManager.maxWorldPoint.x*0.9f); 
        var y = Random.Range(GameManager.minWorldPoint.y*1.1f, GameManager.maxWorldPoint.y*0.9f);
        
        var target = new Vector3(x, y, 70);
        var force =  target - transform.position;
        
        rb.AddForce(force.normalized*8, ForceMode.VelocityChange);
        
        SetRandomColor();
    }

    public void Damage()
    {
        if (isBig)
        {
            GameManager.Instance.AddPlayerPoints(200);
            SpawnSubAsteroids();
        }
        else
        {
            GameManager.Instance.AddPlayerPoints(100);

        }
        Destroy(gameObject);
    }

    private void SpawnSubAsteroids()
    {

        for (int i = 0; i < 2; i++)
        {
            var subAsteroid = Instantiate(this);
            subAsteroid.isBig = false;
            subAsteroid.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            var SubRb = subAsteroid.GetComponent<Rigidbody>();
            SubRb.AddForce(Random.insideUnitSphere, ForceMode.VelocityChange);
        }
     


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            Destroy(gameObject);
            GameManager.Instance.PlayerDamage();
        }
    }

    public void SetRandomColor()
    {
        var color = colors[Random.Range(0, colors.Count)];

        var meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.SetVector("_EmissionColor", color * 10f);
        
    }
}
