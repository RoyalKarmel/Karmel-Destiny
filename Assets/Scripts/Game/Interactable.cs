using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2f;

    Transform player;

    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= radius)
        {
            if (Input.GetButtonDown("Interact"))
                Interact();
        }
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
