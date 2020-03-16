using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private float skyBoxRotationSpeed = 0.9f;
    
    private static readonly int Rotation = Shader.PropertyToID("_Rotation");

    
    

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox.SetFloat(Rotation, Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat(Rotation, Time.time * skyBoxRotationSpeed);
    }

    
}
