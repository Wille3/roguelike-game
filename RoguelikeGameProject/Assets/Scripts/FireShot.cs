using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour
{
    [HideInInspector] public float FireVelocity;

    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        Destroy(gameObject, 4f);
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.up * FireVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
