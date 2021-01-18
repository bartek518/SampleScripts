using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private float jumpForce;
    [SerializeField] private float maxMovementSpeed;
    private float time;
    [SerializeField] private float acceleration;
    [SerializeField] private float baseSpeed;
    private bool facingSideRight = true;
    public Animator animator;
    float currentHorizontalSpeed = 0;

    private LayerMask ground;
    private Rigidbody2D m_rigidbody;
    private BoxCollider2D m_collider;
    private GameObject playerBody;
    private double jumpCooldown;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<BoxCollider2D>();
        playerBody = transform.Find("Body").gameObject;
        jumpCooldown = 0.1;

        ground = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        currentHorizontalSpeed = Input.GetAxisRaw("Horizontal") * maxMovementSpeed;
        animator.SetFloat("Speed", Mathf.Abs(currentHorizontalSpeed));

        if ((m_rigidbody.velocity.x >= 0 && facingSideRight == false) || (m_rigidbody.velocity.x < 0 && facingSideRight))
        {
            Flip();
        }
    }

    public void SetCustomValues(float jumpForce, float maxMovementSpeed, float acceleration, float baseSpeed)
    {
        this.jumpForce = jumpForce;
        this.maxMovementSpeed = maxMovementSpeed;
        this.acceleration = acceleration;
        this.baseSpeed = baseSpeed;
    }

    public void Jump()
    {
        if (canJump())
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, 1f * jumpForce);
        }
    }

    private bool canJump()
    {   
        bool hasCooldown = (Time.time - time < jumpCooldown);
        time = Time.time;

        return isGrounded() && hasCooldown == false;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(m_collider.bounds.center, m_collider.bounds.size, 0f, Vector2.down, 0.1f, ground);
        return raycast.collider != null; 
    }

    public void Move(bool moveRight)
    {
        int movementDirection = moveRight ? 1 : -1;

        m_rigidbody.velocity += new Vector2((baseSpeed + maxMovementSpeed * acceleration) * movementDirection, 0);
        m_rigidbody.velocity = new Vector2(Mathf.Clamp(m_rigidbody.velocity.x, -maxMovementSpeed, maxMovementSpeed), m_rigidbody.velocity.y);

    }

    public void StopMoving()
    {
        // slows down player character untill it stops moving (used if no input detected)
        if(m_rigidbody.velocity.x != 0)
        {
            m_rigidbody.velocity += new Vector2(-m_rigidbody.velocity.x * 0.05f, 0);
        }
    }

    private void Flip()
    {
        facingSideRight = !facingSideRight;

        Vector3 scale = playerBody.transform.localScale;
        scale.x *= -1;
        playerBody.transform.localScale = scale;
    }
}
