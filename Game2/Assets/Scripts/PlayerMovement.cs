using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float jumpForce =14f;
    Rigidbody2D rb;
    public float speed = 5f;
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
            // בדיקה אם נוגע מלמעלה
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.normal.y > 0.7f) // נגיעה מלמעלה
                {
                    isGrounded = true;
                }
            }
        }
    }



    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
    }

    public IEnumerator ApplyJumpBoost(float multiplier, float duration)
    {
        Debug.Log("you got Jump Boost!");
        jumpForce *= multiplier;
        yield return new WaitForSeconds(duration);
        jumpForce /= multiplier;
    }

    public IEnumerator ApplySpeedBoost(float multiplier, float duration)
    {
        Debug.Log("you got Speed Boost!");
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed /= multiplier;
    }
}
