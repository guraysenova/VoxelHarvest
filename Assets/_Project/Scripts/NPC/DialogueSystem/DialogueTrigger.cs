using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue(List<string> npcDialogues , TextMeshProUGUI dialogueText , GameObject npcGameObject)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(npcDialogues.ToArray(), dialogueText , npcGameObject);
    }
}