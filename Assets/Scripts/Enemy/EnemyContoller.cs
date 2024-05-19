using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField]
    private EnemyStats enemy;
    private EnemyCombat enemyCombat;

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
        enemyCombat = GetComponent<EnemyCombat>();
    }

    void Update()
    {
        if (isPlayerDetected)
        {
            if (IsPlayerInRange())
                enemyCombat.Attack();
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

        if (timeSinceLastDirectionChange >= enemy.changeDirectionInterval)
        {
            ChangeMoveDirection();
            timeSinceLastDirectionChange = 0f;
            timeSinceLastMove = 0f;
        }

        rb.velocity = moveDirection * enemy.speed.GetValue();
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
        rb.velocity = moveDirection * enemy.speed.GetValue();

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
        if (timeSinceLastMove >= enemy.moveDuration)
            rb.velocity = Vector2.zero;
    }

    void CheckPlayerDetection()
    {
        if (Vector2.Distance(transform.position, target.position) <= enemy.detectionRange)
            isPlayerDetected = true;
    }

    void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(initialPosition, target.position);
        if (distanceToPlayer > enemy.returnRange)
            isPlayerDetected = false;
    }

    bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        return distanceToPlayer <= enemy.attackRange;
    }
    #endregion
}
