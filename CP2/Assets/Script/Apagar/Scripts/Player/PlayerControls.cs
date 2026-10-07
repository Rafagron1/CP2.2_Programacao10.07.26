using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed;


    [Header("Jump Settings")]
    [SerializeField] private float localGravity = 1.0f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private Transform sensorGround;
    [SerializeField] private Vector3 sensorSize;
    [SerializeField] private LayerMask layerGround;


    private float move;
    private float currentJumpTime;


    private Rigidbody2D rigidbody2D;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(sensorGround.position, sensorSize);
    }


    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();    
    }

    void Start()
    {
        rigidbody2D.gravityScale = localGravity;
    }


    void Update()
    {       
        Move();
        Jump();

    }

    void FixedUpdate()
    {
        OnMove();
        OnJump();
    }

    void Move()
    {
        move = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if(move > 0.0f)
        {
            transform.eulerAngles = new Vector2(0.0f, 0.0f);
        }
        else if(move < 0.0f)
        {
            transform.eulerAngles = new Vector2(0.0f, 180.0f);
        }
    }


    void OnMove()
    {
        rigidbody2D.linearVelocity = new Vector2(move, rigidbody2D.linearVelocityY);
    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded() == true)
        {
            currentJumpTime = jumpTime;
        }
        else if (Input.GetButton("Jump") && currentJumpTime > 0.0f)
        {
            currentJumpTime -= Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            currentJumpTime = 0.0f;
        }
    }


    void OnJump()
    {
        if (currentJumpTime > 0.0f)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    bool Grounded()
    {
        return Physics2D.OverlapBox(sensorGround.position, sensorSize, 0, layerGround);
    } 

    public float MoveValue()
    {
        return move;
    }

    public float RbVelocity()
    {
        return rigidbody2D.linearVelocityY;
    }

}
