using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 6f;

    private float inputX;
    

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        Debug.Log(inputX);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);
    }
}
