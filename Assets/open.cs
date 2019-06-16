using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class open : MonoBehaviour
{
    Random _random = new Random();
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.rotation.Set(0, _random.Next(), 0, 0);
    }
}