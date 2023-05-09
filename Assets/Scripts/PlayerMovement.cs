using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movement;
    
    [SerializeField] const int SPEED = 15;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] float jumpStrength = 20;
    [SerializeField] bool isGrounded = true;

    [SerializeField] GameObject kunai;

    AudioSource kunaiClip;

    Animator animator;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        kunaiClip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;

        if (Input.GetButtonDown("Fire1"))
        {
            shootKunai();
        }
    }
    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(SPEED * movement, rigid.velocity.y);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();
        if (jumpPressed && isGrounded)
            Jump();
        else
        {
            jumpPressed = false;
            if (isGrounded)
            {
                animator.SetBool("jumpping", false);
                if (movement > 0 || movement < 0)
                {
                    animator.SetBool("running", true);
                    
                }
                else

                {   animator.SetBool("idle", true);
                    animator.SetBool("running", false);
                }
            }

        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void Jump()
    {
        animator.SetBool("jumpping", true);
        animator.SetBool("idle", false);
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector3.up * jumpStrength, ForceMode2D.Impulse);

        jumpPressed = false;
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
        // else
        //   Debug.Log(collision.gameObject.tag);
        animator.SetBool("idle", true);
    }

    private void shootKunai()
    {
        if (Time.timeScale==0) return;
        kunaiClip.Play();

        Instantiate(kunai, transform.position, transform.rotation);
    }
}
