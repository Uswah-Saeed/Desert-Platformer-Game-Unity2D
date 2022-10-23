using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderMovement : MonoBehaviour
{

    private float vertical;
    private float speed = 8f;

    private bool isLadder;
    private bool isClimbing;
    private Animator animator;

    [SerializeField] private Rigidbody2D rb;
    public void Start()
    {

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
            animator.SetBool("isClimb", true);
        }

    }
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 2f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("climber"))
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("climber"))
        {
            isLadder = false;
            isClimbing = false;
            animator.SetBool("isClimb", false);
        }


    }
}
