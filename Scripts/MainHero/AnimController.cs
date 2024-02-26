using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public bool isJump;
    float speed;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
       
    }
    private void Update()
    {
        isJump = GetComponent<PlayerMovement>().isGrounded;
        speed = rb.velocity.x;
        float verticalSpeed = rb.velocity.y;
        if (!isJump)
        {
            animator.SetBool("IsJump", true);
            animator.SetFloat("moveSpeed", speed);
            animator.SetFloat("VerticalSpeed", verticalSpeed);
        }
        else
        {
            if (speed < 0) speed = speed * -1f;
            animator.SetFloat("moveSpeed", speed);
            animator.SetFloat("VerticalSpeed", verticalSpeed);
            animator.SetBool("IsJump", false);
           
        }
        FlipCheracter();
    }
    void FlipCheracter()
    {
        if (Input.GetKeyDown(KeyCode.A)) { sprite.flipX = true; }
        else if (Input.GetKeyDown(KeyCode.D)) { sprite.flipX = false; }
    }
}
