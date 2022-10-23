using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    private float horizontal;
    private float Speed = 5f;
   /* private float jumpSpeed = 8f;   //jump*/
    private bool isFacingRight = true;
    private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    public void Start()
    {

        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");  //returns -1 , 0 , +1 depending upon the direction we are moving\
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        Flip();

        
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * 250);
            animator.SetBool("isJump", true);
        }
        if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isGlid", true);
            rb.gravityScale = 3f;
        }
       
       
           
       
        

        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("isAttack", true);
           /* void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.tag == "Player")
                {
                    Destroy(gameObject);
                }

            }
*/


        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isAttack", false);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("sdcsdcdfcv");
            animator.SetBool("isGlid", false);

        }
    }

        private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * Speed, rb.velocity.y);
    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "nextlevel")
    //    {
    //        SceneManager.LoadScene("menu");
    //    }
    //}


}