using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] Item itemToUnlock;
    [SerializeField] Dialogue dialogue;

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") return;
        dialogue.unlocked = Inventory.instance.CheckIfHasItem(itemToUnlock);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
