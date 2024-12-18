using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public float damage;
    public float moveSpeed;
    public float lifespan;
    private Rigidbody rb;

    public void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Destroy ourselves when our lifespan is up
        Destroy(gameObject, lifespan);
    }

    public void Update()
    {
        // Move forward at our movespeed (per second, not per framedraw)
        rb.velocity = transform.forward * moveSpeed;       
    }

    public void OnTriggerEnter(Collider other)
    {
        // Get the Health component off the other object
        Health otherHealth = other.GetComponent<Health>(); 

        // If it has one, tell it to take damage
        if(otherHealth != null )
        {
            otherHealth.TakeDamage(damage);
        }

        // Destroy the projectile
        Destroy(gameObject);
    }
}
