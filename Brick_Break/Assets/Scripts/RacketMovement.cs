using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;

    public float paddleSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            direction = Vector2.left;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) //use an else if because we don't want to get both inputs in the same frame
            direction = Vector2.right;
        else
            direction = Vector2.zero;

    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
            rb.AddForce(direction * paddleSpeed);
    }
}
