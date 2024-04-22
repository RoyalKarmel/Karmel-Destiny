using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
        // transform.Translate(movement);
        rb.velocity = movement;

        // Rotate character
        if (moveHorizontal < 0)
            spriteRenderer.flipX = false;
        if (moveHorizontal > 0)
            spriteRenderer.flipX = true;
    }
}
