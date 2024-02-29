using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int maxHeath = 5;
    public int currentHealth;

    public Healthbar healthBar;

    public Animator animator;

    private int maxHealth = 5;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private float direction = 0f;

    [SerializeField]
    private LayerMask groundLayers;

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float jumpSpeed = 6f;

    float speed = 140f;
    bool facingRight = true;

    bool isGrounded;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Jump(bool jumping)
    {


    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        animator.SetFloat("speed", Mathf.Abs(direction));

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.y, jumpSpeed);
            
        }       

        if (direction != 0)
        {
            rb.AddForce(new Vector2(direction * speed, 0f));

        }
        if (direction > 0 && !facingRight)
        {
            Flip();

        }
        if (direction < 0 && facingRight)
        {
            Flip();

        }


    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth >= 1)
        {
            currentHealth -= 1;

        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    bool IsGrounded()
    {

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayers);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "slime")
        {
            Healthbar.TakeDamage(damage);

        }

    }
}
    