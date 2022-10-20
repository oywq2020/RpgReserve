using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public float speed = 0.1f;

    public GameObject explodePreb;

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(explodePreb, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

   
    void FixedUpdate () {
        transform.Translate(Vector3.forward * speed);
    }
}
