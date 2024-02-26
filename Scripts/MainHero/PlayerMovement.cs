using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ��������
    public int extraJumps = 1; // �������������� ������
    public float jumpForce = 10f; // ���� ������
    public Transform groundCheck; // ����� ��� �������� ���������� �� �����
    public LayerMask groundLayer; // ����, ������������ �����
    public float groundCheckRadius = 0.1f; // ������ �������� ���������� �� �����

    private Rigidbody2D rb;
    public bool isGrounded;
    public int remainingJumps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingJumps = extraJumps;
    }

    void Update()
    {
        // ��������� ���������� �� �����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // �������� ������� ������ ��� ����������
        float moveInput = Input.GetAxis("Horizontal");

        // ���������� ������ �� �����������
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // ��� ������� �� ������ � ���������� �� ����� ��� �������� �������������� ������, ��������� ������
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (isGrounded || remainingJumps > 0)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        // ������ ��������� ������������ �������� ��� ������
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        if (!isGrounded)
        {
            remainingJumps--;
        }
        else
        {
            remainingJumps = extraJumps;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            remainingJumps = extraJumps;
        }
    }
}
