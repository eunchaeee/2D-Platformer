using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 6f;

    private float inputX;
    private float inputY;
    private bool isFinished;

    [SerializeField] private bool isOnLadder;

    void Update()
    {
        if (isFinished) return;
        
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        Debug.Log($"{inputX}, {inputY}");

        
    }

    private void FixedUpdate()
    {
        if (isFinished)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (isOnLadder && inputY != 0)
        {
            rb.gravityScale = 0f;
            rb.MovePosition(new Vector2(rb.position.x, rb.position.y + inputY * moveSpeed * Time.fixedDeltaTime));
            return;
        }

        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);
    }

    public void Finish()
    {
        isFinished = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "ladder")
        {
            isOnLadder = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ladder")
        {
            isOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ladder")
        {
            isOnLadder = false;
        }
    }
}
