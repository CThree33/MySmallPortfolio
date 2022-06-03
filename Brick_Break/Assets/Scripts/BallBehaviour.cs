using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public int StartSpeed = 500;

    private Rigidbody2D rb;
    private BrickBehaviour brickBehaviour;
    private BrickInstantiator gameMgr;
    private Vector2 currentVelocity;

    // Start is called before the first frame update

    private void Awake()
    {
        //getting rb and the manager in awake solves synchrony issues
        rb = GetComponent<Rigidbody2D>(); 
        gameMgr = GameObject.Find("GameMgr").GetComponent<BrickInstantiator>();
    }
    void Start()
    {
        ShootBall();
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = rb.velocity;
    }

    //bounce properties and damage brick on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle") || collision.gameObject.CompareTag("Brick"))
        {

            float speed = currentVelocity.magnitude;
            Vector2 direction = Vector2.Reflect(currentVelocity.normalized + collision.contacts[0].point, collision.contacts[0].normal);

            rb.velocity = (direction.normalized * speed);

            Debug.Log(collision.gameObject.tag);

            if (collision.gameObject.CompareTag("Brick"))
            {
                //damage the brick and temporarily disable the collider so the brick doesn't get one-shot
                collision.transform.GetComponent<BrickBehaviour>().TakeDamage();
                GetComponent<CircleCollider2D>().enabled = false;
                StartCoroutine(Timer(0.1f));
                GetComponent<CircleCollider2D>().enabled = true;
                gameMgr.AddPoints();
            }

            //little game feauture, to encourage the player to go for multiple bricks at a time, touching the paddle will subtract some points
            else if (collision.gameObject.CompareTag("Paddle"))
            {
                gameMgr.SubtractPoints();
            }
        }
        if (collision.gameObject.CompareTag("Downward wall"))
        {
            Debug.Log("lose");
            gameMgr.Lose();
        }
    }

    private void ShootBall()
    {
        StartCoroutine(Timer(3f)); //wait one second before the ball is shot so the player can react
        Vector2 startDirection = new Vector2(Random.Range(-1.5f, 1.5f), -1); //random start direction downwards so we can't predict the ball's movement
        rb.AddForce(startDirection.normalized * StartSpeed); //normalize the direction vector so speed isn't affected
    }

    private IEnumerator Timer(float time)
    {
        Debug.Log("started coroutine");
        yield return new WaitForSeconds(time);
        Debug.Log("ended coroutine");
    }
}
