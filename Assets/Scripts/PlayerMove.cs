using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    //speed
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float jumpPower = 70f;
    
    //ground check
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    
    // rigid body setting
    private float gravityScale;
    
    // Inputs
    private float horizontal;
    private float vertical;
    
    // status
    private bool endGame;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isNearLadder;
    [SerializeField] private bool isClimbing;
    [SerializeField] private bool isJump;

    //ladder
    private Ladder ladder;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }


    void Update()
    {
        if (endGame) return;
        
        horizontal = Input.GetAxisRaw("Horizontal");        
        vertical = Input.GetAxisRaw("Vertical");

        // Facing Direction
        if (horizontal != 0)
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);
        
        // gravity setting
        if (isNearLadder)
            rb.gravityScale = 0;
        else
            rb.gravityScale = gravityScale;
        
        // judge climbing
        if (isNearLadder && vertical != 0)
            isClimbing = true;
        else if (isClimbing && isGrounded)
            isClimbing = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (endGame)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // judge isGrounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        
        // jump
        if (isJump && isGrounded && !isClimbing)
        {
            Jump();
            isJump = false;
        }
        
        // Player Move (Climb / not Climb)
        if (isClimbing)
        {
            float newX = ladder.transform.position.x;
            float newY = Mathf.Clamp(rb.position.y + vertical * climbSpeed * Time.fixedDeltaTime, 
                ladder.bottom.position.y, ladder.top.position.y);
            rb.MovePosition(new Vector2(newX, newY));
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        }
    }

    private void Jump()
    {
        //rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
    }
    
    public void Finish()
    {
        endGame = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ladder")
        {
            isNearLadder = true;
            ladder = other.GetComponent<Ladder>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "ladder")
        {
            isNearLadder = false;
            ladder = null;
        }
    }

}
