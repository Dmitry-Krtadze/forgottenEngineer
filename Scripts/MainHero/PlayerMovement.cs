using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения
    public int extraJumps = 1; // Дополнительные прыжки
    public float jumpForce = 10f; // Сила прыжка
    public Transform groundCheck; // Точка для проверки нахождения на земле
    public LayerMask groundLayer; // Слой, обозначающий землю
    public float groundCheckRadius = 0.1f; // Радиус проверки нахождения на земле

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
        // Проверяем нахождение на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Получаем входные данные для управления
        float moveInput = Input.GetAxis("Horizontal");

        // Перемещаем игрока по горизонтали
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // При нажатии на прыжок и нахождении на земле или остались дополнительные прыжки, выполняем прыжок
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
        // Придаём персонажу вертикальную скорость для прыжка
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
