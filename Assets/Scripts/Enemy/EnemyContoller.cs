using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    [Header("Enemy Stats")]
    public Enemy enemy;

    [Header("Move variables")]
    public float moveDuration = 5f;
    public float randomMoveRange = 10f;
    public float changeDirectionInterval = 2f;

    [Header("Ranges")]
    public float returnRange = 15f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;

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
                enemy.Attack();
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

        rb.velocity = moveDirection * enemy.moveSpeed;
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
        rb.velocity = moveDirection * enemy.moveSpeed;

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
