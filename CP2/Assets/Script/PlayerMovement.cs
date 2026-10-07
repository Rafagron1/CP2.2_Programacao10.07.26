using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed =4.0f;
    [SerializeField] private float acceleration = 50.0f;
    [SerializeField] private float desacceleration = 50.0f;

    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float fall = 2.5f;
    [SerializeField] private float low = 2f;

    [SerializeField] private Transform check;
    private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private float moveInput;
    private bool isGrounded;
    private bool jumpRequested;
 
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(check.position, checkRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
        if (moveInput > 0) { transform.localScale = new Vector2(1.0f, 1.0f); }
        if (moveInput < 0) { transform.localScale = new Vector2(-1.0f,1.0f); }
        ManipularGravidade();
        animator.SetInteger("pMove", (int)moveInput);
        animator.SetBool("pGrounded", isGrounded); 
    }
    void FixedUpdate()
    {
        float targetSpeed = moveInput * speed;
        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : desacceleration;
        float movement = speedDif * accelerationRate;
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }
    }

    private void ManipularGravidade()
    {
        if (rb.linearVelocityY < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        }
        else if(rb.linearVelocityY > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (low - 1) * Time.deltaTime;
        }
    }
}
