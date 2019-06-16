using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class kick : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;
    private readonly Random _random = new Random();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.isKinematic = false;
        // rb.detectCollisions = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
//        rb.MovePosition(transform.position + transform.forward * Time.deltaTime);
//        rb.velocity.Set(1, 1, 1);
        rb.AddForce(_random.Next(-1, 1), _random.Next(-1, 1), _random.Next(-1, 1), ForceMode.Impulse);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 100, 20), rb.position.ToString());
    }
}