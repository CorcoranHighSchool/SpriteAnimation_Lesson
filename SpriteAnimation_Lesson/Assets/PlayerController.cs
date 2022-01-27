using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //Speed of the character
    private float m_speed = 10.0f;
    private float m_jumpForce = 5.0f;
    private bool isOnGround = true;
    private Vector2 m_movement = Vector2.zero;

    //Components
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //We are going to the right
        if(horizontalInput > 0)
        {
            m_spriteRenderer.flipX = false;
        }else if(horizontalInput < 0) // facing left
        {
            m_spriteRenderer.flipX = true;
        }

        float moveSpeed = Mathf.Abs(horizontalInput);
        m_animator.SetFloat("Speed", moveSpeed);
        float jump = Input.GetAxis("Jump");
        m_movement = new Vector2(horizontalInput, jump);
    }

    private void FixedUpdate()
    {
        m_animator.SetFloat("yVelocity", m_rigidbody.velocity.y);
        Vector2 direction = Vector2.zero;
        direction.x = Mathf.Clamp(m_movement.x * m_speed - m_rigidbody.velocity.x, -m_speed, m_speed);
        bool jump = m_movement.y > 0.0f;

        m_rigidbody.AddForce(direction, ForceMode2D.Force);
        if(jump && isOnGround)
        {
            m_animator.SetBool("isJumping", true);
            m_rigidbody.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = collision.gameObject.CompareTag("Floor");
        if (isOnGround)
        {
            m_animator.SetTrigger("Grounded");
        }
    }
}
