using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Travel speed of projectile
    private Rigidbody rigid; // Reference to rigidbody

    void Awake()
    {
        // Get reference to Rigidbody
        rigid = GetComponent<Rigidbody>();
    }

    // Fire's this bullet in a given direction (using defined speed)
    public void Fire(Vector3 direction)
    {
        // Add force in the given direction by speed
        rigid.AddForce(direction * speed, ForceMode.Impulse);
    }
}