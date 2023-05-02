using UnityEngine;

// For interactable objects/items/entity
public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact() 
    {
        // virtual means overridable from other scripts, such as enemy/item scripts
        // This method is meant to be overwritten
        Debug.Log("interacting with " + interactionTransform.name);
    }

    // Constantly checking if itself is being focused and interacted
    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            // Check proximity between player and itself
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true; // Make sure to interact once only
            }
        }
    }

    // Focused behavior
    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false; // Allow next interaction
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
