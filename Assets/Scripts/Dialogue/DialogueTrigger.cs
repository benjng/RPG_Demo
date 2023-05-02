using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue() // On button clicked, trigger this
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
