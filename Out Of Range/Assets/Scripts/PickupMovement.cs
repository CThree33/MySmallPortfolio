using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : MonoBehaviour
{
    int quantity;
    float time;
    Vector3 startPosition = new Vector3();

    void Start()
    {
        startPosition = transform.position;
        quantity = Random.Range(5, 20);
    }


    void Update()
    {
        Movement();
    }
    void Movement()
    {
        time += Time.deltaTime;
        transform.Rotate(50f * Time.deltaTime, 0, 0, Space.Self);
    }
}
