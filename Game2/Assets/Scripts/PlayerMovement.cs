using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float jumpForce;
    public float JumpForce => jumpForce;
    Rigidbody2D rb;
    [SerializeField] float speed;
    bool isGrounded = true;
    float directionX;
    Animator animator;
    SpriteRenderer sprite;
    InputSystem inputActions;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("RigidBody is null!");
        }
    }

    private void Awake()
    {
        inputActions = new InputSystem();

    }



    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
        inputActions.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;
        Jump();
    }

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        directionX = 0;
    }

    private void OnMovePerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        directionX = ctx.ReadValue<Vector2>().x;

    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        inputActions.Player.Jump.performed -= OnJumpPerformed;
    }

    // Update is called once per frame
    void Update()
    {


    }
    

    private void FixedUpdate()
    {
        rb.linearVelocity = new(directionX * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }



    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
    }
}
