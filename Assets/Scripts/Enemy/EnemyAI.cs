using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float nextWaypointDistance = 3f;
    public Transform enemyGFX;

    [Header("Patrol Settings")]
    public EnemyPatrol patrol = new EnemyPatrol();
    private Transform[] patrolPoints;
    private int patrolIndex = 0;

    [Header("Detection Settings")]
    public EnemyDetection detection = new EnemyDetection();

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    // Components
    Transform target;
    EnemyStats stats;
    EnemyCombat combat;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        stats = GetComponent<EnemyStats>();
        combat = GetComponent<EnemyCombat>();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        patrol.Initialize(transform);
        patrolPoints = patrol.GeneratePatrolPoints();

        detection.Initialize(transform);

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    #region Patrol

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (detection.PlayerInSight())
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            else
            {
                if (patrolPoints.Length > 0)
                    seeker.StartPath(
                        rb.position,
                        patrolPoints[patrolIndex].position,
                        OnPathComplete
                    );
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    #endregion

    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            if (!detection.PlayerInSight())
            {
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            }
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Enemy movement
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * stats.speed.GetValue();

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        RotateEnemy(force);
    }

    #region Utils
    // Rotate enemy
    void RotateEnemy(Vector2 force)
    {
        if (force.x >= 0.01f)
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        else if (force.x <= -0.01f)
            enemyGFX.localScale = new Vector3(1f, 1f, -1f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detection.detectionRange);
    }
    #endregion
}
