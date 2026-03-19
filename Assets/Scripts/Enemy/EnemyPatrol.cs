using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // ─── Settings ────────────────────────────────────────────────────────
    [Header("Patrol")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform leftEdge;
    [SerializeField] Transform rightEdge;

    [Header("Ground/Wall Detection")]
    [SerializeField] Transform groundDetect;
    [SerializeField] float detectRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    [Header("Health")]
    [SerializeField] int maxHealth = 3;

    // ─── State ───────────────────────────────────────────────────────────
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    bool movingRight = true;
    bool isAlive = true;
    float leftBound;
    float rightBound;
    int currentHealth;

    // ─── Lifecycle ───────────────────────────────────────────────────────
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        leftBound = leftEdge.position.x;
        rightBound = rightEdge.position.x;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!isAlive) return;
        CheckEdges();
    }

    void FixedUpdate()
    {
        if (!isAlive) return;
        Patrol();
    }

    // ─── Patrol Logic ────────────────────────────────────────────────────
    void Patrol()
    {
        float direction = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        spriteRenderer.flipX = !movingRight;
    }

    void CheckEdges()
    {
        if (movingRight && transform.position.x >= rightBound)
        {
            TurnAround();
            return;
        }
        else if (!movingRight && transform.position.x <= leftBound)
        {
            TurnAround();
            return;
        }

        bool groundAhead = Physics2D.OverlapCircle(groundDetect.position, detectRadius, groundLayer);
        if (!groundAhead)
            TurnAround();
    }

    void TurnAround()
    {
        movingRight = !movingRight;
    }

    // ─── Damage ──────────────────────────────────────────────────────────
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    // ─── Death ───────────────────────────────────────────────────────────
    public void Die()
    {
        isAlive = false;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("die");
        Destroy(gameObject, 0.8f);
    }
}