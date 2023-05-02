using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Canvas DialogueCanvas;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Dialogue currentDialogue;
    private Queue<string> sentences;

    void Start()
    {
        DialogueCanvas.enabled = false;
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        currentDialogue = dialogue;
        DialogueCanvas.enabled = true;
        nameText.text = currentDialogue.name;

        sentences.Clear();

        if (currentDialogue.unlocked)
        {
            foreach (string un_sentence in currentDialogue.unlocked_sentences)
            {
                sentences.Enqueue(un_sentence);
            }
        } else
        {
            foreach (string sentence in currentDialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        sentences.Clear();
        DialogueCanvas.enabled = false;
        if (currentDialogue.unlocked)
        {
            // Item/tasks cases
            if (currentDialogue.name == "Unknown Box")
            {
                // Trigger any animation (e.g. open box)
                Debug.Log("Box opening");
                return;
            }
        }
    }
}
