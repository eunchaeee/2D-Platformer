using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 6f;

    private float inputX;
    private bool isFinished;

    void Update()
    {
        if (isFinished) return;
        inputX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (isFinished)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);
    }

    public void Finish()
    {
        isFinished = true;
    }
}
