using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Item itemToUnlock;
    [SerializeField] GameObject chest;
    [SerializeField] Mesh chestOpenMesh;
    [SerializeField] GameObject cubeToReward;

    void Start()
    {
        DialogueManager.OnUnlockChestEvent += UnlockChest;
    }

    public void TriggerDialogue() // On button clicked
    {
        // If Inventory has item, unlock next dialogue
        dialogue.unlocked = Inventory.instance.CheckIfHasItem(itemToUnlock);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void UnlockChest()
    {
        chest.GetComponent<MeshFilter>().mesh = chestOpenMesh;
        Instantiate(cubeToReward, chest.transform.position + new Vector3(0,3,0), new Quaternion(), chest.transform);
    }
}
