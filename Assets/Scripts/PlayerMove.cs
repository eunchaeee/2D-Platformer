using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 6f;

    private float horizontal;
    private bool endGame;

    
    // status
    private PlayerLadder playerLadder;
    private bool isGrounded;

    private void Start()
    {
        playerLadder = GetComponent<PlayerLadder>();
    }

    void Update()
    {
        if (endGame) return;
        
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (endGame)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (playerLadder != null && playerLadder.IsClimbing)
            return;
        
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    public void Finish()
    {
        endGame = true;
    }
}
