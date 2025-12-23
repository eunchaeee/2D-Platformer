using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerLadder : MonoBehaviour
{
    public float climbSpeed = 5f;

    private Rigidbody2D rb;
    private float gravityScale;

    [SerializeField] private bool isNearLadder;
    [SerializeField] private bool isClimbing;

    private Ladder ladder;

    private float vertical;

    public bool IsClimbing => isClimbing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }

    private void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        if (isNearLadder && vertical != 0)
            isClimbing = true;
        

        if (!isNearLadder)
            isClimbing = false;
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0;
            float targetX = ladder.transform.position.x;
            float newX = Mathf.Lerp(rb.position.x, targetX, 0.2f);
            
            rb.MovePosition(new Vector2(newX, rb.position.y + vertical * climbSpeed * Time.fixedDeltaTime));
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
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
