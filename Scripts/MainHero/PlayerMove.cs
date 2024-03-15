using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool IsGround = false;

    [SerializeField] private float modifySpeed;
    [SerializeField] private float forceJump;
    public float MoveSpeed;
    private Animator animator;
    private SpriteRenderer sprite;

    private Rigidbody2D rb;
    void Start()
    {
        animator = GameObject.Find("DayanaRender").GetComponent<Animator>();
        sprite = GameObject.Find("DayanaRender").GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        SimpleMove();
        SimpleJump();
    }



    private float currentVelocity;
    void SimpleJump()
    {
        if (IsGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                rb.AddForce(Vector2.up * forceJump);
                Debug.Log("Abobik");
            }
        }
    }


    void SimpleMove()
    {
        MoveSpeed = Input.GetAxis("Horizontal") * modifySpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(MoveSpeed, 0);
        float MoveSpeed2Anim = MoveSpeed;

        if (MoveSpeed < 0)
        {
            MoveSpeed2Anim = MoveSpeed * -1f;
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        animator.SetFloat("moveSpeed", MoveSpeed2Anim);
    }















    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")){
            IsGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            IsGround = false;
        }
    }
}
