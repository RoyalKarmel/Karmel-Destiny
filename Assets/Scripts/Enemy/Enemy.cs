using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 3f;
    public float moveDuration = 5f;
    public float randomMoveRange = 10f;
    public float changeDirectionInterval = 2f;

    [Header("Ranges")]
    public float returnRange = 15f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;

    [Header("Stats")]
    public int damage = 10;

    private Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 initialPosition;
    private bool isPlayerDetected = false;
    private float timeSinceLastDirectionChange = 0f;
    private Vector2 moveDirection;
    private float timeSinceLastMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialPosition = transform.position;

        // Random move direction
        moveDirection = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDetected)
            Attack();
        else
            Move();
    }

    void Move()
    {
        timeSinceLastDirectionChange += Time.deltaTime;

        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            // Random move direction
            moveDirection = Random.insideUnitCircle.normalized;

            timeSinceLastDirectionChange = 0f;
            timeSinceLastMove = 0f;
        }

        rb.velocity = moveDirection * moveSpeed;

        // Rotate enemy
        if (moveDirection.x > 0)
            spriteRenderer.flipX = true;
        else if (moveDirection.x < 0)
            spriteRenderer.flipX = false;

        timeSinceLastMove += Time.deltaTime;

        // Stop enemy
        if (timeSinceLastMove >= moveDuration)
            rb.velocity = Vector2.zero;

        // Check if player is in attack range
        if (Vector2.Distance(transform.position, target.position) <= detectionRange)
            isPlayerDetected = true;
    }

    void Attack()
    {
        // Move to player
        moveDirection = (target.position - transform.position).normalized;
        rb.velocity = moveDirection * moveSpeed;

        // Rotate enemy
        if (moveDirection.x > 0)
            spriteRenderer.flipX = true;
        if (moveDirection.x < 0)
            spriteRenderer.flipX = false;

        // Check if player run away
        float distanceToPlayer = Vector2.Distance(initialPosition, target.position);
        if (distanceToPlayer > returnRange)
            isPlayerDetected = false;
    }
}

