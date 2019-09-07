using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    GameObject npcGO;

	// Use this for initialization
	void Start ()
    {
        sentences = new Queue<string>();
	}

    public void StartDialogue(string[] dialogue , TextMeshProUGUI dialogueText , GameObject npcGameObject)
    {
        npcGO = npcGameObject;
        sentences.Clear();
        foreach(string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        // call it when player presses something amirite
        DisplayNextSentence(dialogueText);
    }

    private void DisplayNextSentence(TextMeshProUGUI dialogueText)
    {
        dialogueText.gameObject.transform.parent.gameObject.SetActive(true);
        if(sentences.Count == 0)
        {
            EndDialogue(dialogueText);
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        // show the sentence
        StartCoroutine(TypeSentence(sentence , dialogueText));
    }

    private void EndDialogue(TextMeshProUGUI dialogueText)
    {
        dialogueText.text = "";
        dialogueText.gameObject.transform.parent.gameObject.SetActive(false);
    }

    IEnumerator TypeSentence(string sentence , TextMeshProUGUI dialogueText)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
            yield return null;
        }
        StartCoroutine(InactivityCountDown(dialogueText));
    }
    IEnumerator InactivityCountDown(TextMeshProUGUI dialogueText)
    {
        yield return new WaitForSecondsRealtime(5f);
        dialogueText.text = "";
        dialogueText.gameObject.transform.parent.gameObject.SetActive(false);
        npcGO.GetComponent<NPC>().dialogueTriggered = 0;
    }
}