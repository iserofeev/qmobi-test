using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsterodSpawner : MonoBehaviour
{
    private Plane _plane;
    public Asteroid asteroidPrefab;
    
    
    [EasyButtons.Button]
    public void Spawn()
    {
        var asteroid = Instantiate(asteroidPrefab);

        float x, y;

        if (Random.Range(0, 2) == 1)
        {
            y = GameManager.maxWorldPoint.y; 
        }
        else
        {
            y = GameManager.minWorldPoint.y;
        }
        x = Random.Range(GameManager.minWorldPoint.x, GameManager.maxWorldPoint.x);

        asteroid.transform.position = new Vector3(x, y, 70);
        
        var chance = Random.Range(0, 100);
        if (chance > 30) asteroid.isBig = true;
    }
    

    IEnumerator Start()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(Random.Range(0.9f, 2f));
        }
    }
}
