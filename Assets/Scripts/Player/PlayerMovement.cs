using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats playerStats;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // Rotate character
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (cursorPosition.x > transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * playerStats.speed.GetValue();
        rb.velocity = movement;
    }

    // Take damage on collision
    void OnTrigger2DEnter(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collider.GetComponent<EnemyStats>();

            playerStats.TakeDamage(enemyStats.damage.GetValue());
        }
    }
}
