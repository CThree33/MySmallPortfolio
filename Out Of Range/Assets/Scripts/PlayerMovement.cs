using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;
    public float speed = 15f;
    Vector3 velocity;
    private Vector3 move;
    public FloatingJoystick joystick;
    public bool mobile;
    public GameObject Background;

    public int Coins;
    public float gravity = 9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public int DashCooldown = 5;
    [SerializeField] private float timer;
    bool isGrounded;
    bool isDashing = false;
    public float jumpHeight = 8;

    InputAction jump;
    InputAction movement;
    InputAction dash;
    InputAction reload;
    InputAction pause;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        jump = new InputAction("Jump", binding:"<keyboard>/space");
        dash = new InputAction("Dash", binding: "<keyboard>/leftShift");
        pause = new InputAction("Pause", binding: "<keyboard>/p");

        jump.AddBinding("<Gamepad>/a");

        movement = new InputAction("Controller", binding: "<Gamepad>/leftstick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<keyboard>/w")
            .With("Down", "<keyboard>/s")
            .With("Left", "<keyboard>/a")
            .With("Right", "<keyboard>/d");

        movement.Enable();
        jump.Enable();
        dash.Enable();
        pause.Enable();
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(5);
        isDashing = false;
        timer = 0;
    }

    void Update()
    {
        float x;
        float z;
        if (!mobile)
        {
            x = movement.ReadValue<Vector2>().x;
            z = movement.ReadValue<Vector2>().y;
        }
        else
        {
            x = joystick.Horizontal;
            z = joystick.Vertical;
        }

        animator.SetFloat("Speed", Mathf.Abs(x) + Mathf.Abs(z));

        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (timer < DashCooldown)
        {
            timer += Time.deltaTime;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundMask);
        Debug.Log(isGrounded);

        if (isDashing)
        {
            speed = 36;
        }
        else
        {
            speed = 12;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (Mathf.Approximately(pause.ReadValue<float>(), 1))
        {
            PauseGame();
        }

        if (isGrounded)
        {
            if (Mathf.Approximately(jump.ReadValue<float>(), 1))
            {
                Jump();
            }
            if (Mathf.Approximately(dash.ReadValue<float>(), 1) && !isDashing && timer >= DashCooldown)
            {
                StartCoroutine(Dash());
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void PauseGame()
    {
        Background.SetActive(true);
        Time.timeScale = 0;
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            Coins++;
            Destroy(other.gameObject);
        }
    }
}
