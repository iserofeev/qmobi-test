using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsterodSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    [EasyButtons.Button]
    public void Spawn()
    {
        var asteroid = Instantiate(asteroidPrefab);
        
        
    }
}
