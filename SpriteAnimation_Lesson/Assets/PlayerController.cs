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


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        m_movement = new Vector2(horizontalInput, jump);
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;
        direction.x = Mathf.Clamp(m_movement.x * m_speed - m_rigidbody.velocity.x, -m_speed, m_speed);
        bool jump = m_movement.y > 0.0f;

        m_rigidbody.AddForce(direction, ForceMode2D.Force);
        if(jump && isOnGround)
        {
            m_rigidbody.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = collision.gameObject.CompareTag("Floor");
    }

}
