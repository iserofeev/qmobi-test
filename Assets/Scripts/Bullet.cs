using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Bullet : MonoBehaviour
{

    [SerializeField] private float lifeTime;

    private Coroutine _destrroyRoutine;
    
    
    void Awake()
    {
        _destrroyRoutine = StartCoroutine(DestroyAfter());
    }


    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (_destrroyRoutine != null) StopCoroutine(_destrroyRoutine);
    }
}
