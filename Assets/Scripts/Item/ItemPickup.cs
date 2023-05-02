using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact(); // inherit

        PickUp(); // override
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name );

        // Add to inventory
        bool wasPickedUp = Inventory.instance.AddItem(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
