using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Ranges")]
    public float returnRange = 15f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;

    [Header("Move variables")]
    public float moveDuration = 5f;
    public float changeDirectionInterval = 2f;

    // Enemy classes
    private EnemyStats stats;
    private EnemyCombat combat;

    // Components
    private Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Movement Variables
    private Vector3 initialPosition;
    private bool isPlayerDetected = false;
    private Vector2 moveDirection;
    private float timeSinceLastDirectionChange = 0f;
    private float timeSinceLastMove = 0f;

    void Start()
    {
        combat = GetComponent<EnemyCombat>();
        stats = GetComponent<EnemyStats>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialPosition = transform.position;
        moveDirection = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        if (isPlayerDetected)
        {
            if (IsPlayerInRange())
                combat.Attack();
            else
                MoveTowardsPlayer();
        }
        else
            Move();
    }

    #region Move
    void Move()
    {
        timeSinceLastDirectionChange += Time.deltaTime;

        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            ChangeMoveDirection();
            timeSinceLastDirectionChange = 0f;
            timeSinceLastMove = 0f;
        }

        rb.velocity = moveDirection * stats.speed.GetValue();
        timeSinceLastMove += Time.deltaTime;

        RotateEnemy();
        CheckStopCondition();
        CheckPlayerDetection();
    }

    // Change enemy direction
    void ChangeMoveDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
    }

    // Attack
    void MoveTowardsPlayer()
    {
        moveDirection = (target.position - transform.position).normalized;
        rb.velocity = moveDirection * stats.speed.GetValue();

        RotateEnemy();
        CheckPlayerDistance();
    }
    #endregion

    #region Utils
    public void RotateEnemy()
    {
        spriteRenderer.flipX = moveDirection.x > 0;
    }

    void CheckStopCondition()
    {
        if (timeSinceLastMove >= moveDuration)
            rb.velocity = Vector2.zero;
    }

    void CheckPlayerDetection()
    {
        if (Vector2.Distance(transform.position, target.position) <= detectionRange)
            isPlayerDetected = true;
    }

    void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(initialPosition, target.position);
        if (distanceToPlayer > returnRange)
            isPlayerDetected = false;
    }

    bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        return distanceToPlayer <= attackRange;
    }
    #endregion
}
