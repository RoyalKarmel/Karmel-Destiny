using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
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

        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * player.speed;
        // transform.Translate(movement);
        rb.velocity = movement;
    }
}
