using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2f;

    bool hasInteracted = false;
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= radius)
        {
            if (!hasInteracted && Input.GetButtonDown("Interact"))
            {
                Interact();
                hasInteracted = true;
            }
        }
        else
            hasInteracted = false;
    }

    public virtual void Interact()
    {
        Debug.Log("Interact");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
