using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Canvas DialogueCanvas;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Dialogue currentDialogue;
    private Queue<string> sentences;

    public delegate void OnUnlockChestEventHandler();
    public static event OnUnlockChestEventHandler OnUnlockChestEvent;

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

        if (currentDialogue.unlocked){
            foreach (string un_sentence in currentDialogue.unlocked_sentences){
                sentences.Enqueue(un_sentence);
            }
        } else {
            foreach (string sentence in currentDialogue.sentences){
                sentences.Enqueue(sentence);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0){
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
        if (!currentDialogue.unlocked) return;
        // Item/tasks cases
        switch (currentDialogue.name)
        {
            case "Unknown Box":
                OnUnlockChestEvent.Invoke();
                break;
            case "???":
                // End game case
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }
}
