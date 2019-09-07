using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour , IRelation
{
    [SerializeField]
    string NPCId = null;

    [SerializeField]
    List<NPCQuest> NPCQuests = null;

    NPCQuest currentAvailableQuest = null;

    [SerializeField]
    int relationLevel = 0;

    public int dialogueTriggered = 0;

    List<string> currentDialogue = new List<string>();

    [SerializeField]
    TextMeshProUGUI dialogueBubble = null;

    [SerializeField]
    TextMeshProUGUI questIndicator = null;

    public int RelationLevel
    {
        get
        {
            return relationLevel;
        }
        set
        {
            relationLevel = value;
        }
    }

    private void Start()
    {
        SetupQuests();

        SetupQuestIndicator();
    }

    private void SetupQuestIndicator()
    {
        if(currentAvailableQuest == null)
        {
            questIndicator.text = "";
        }
        else if(currentAvailableQuest.questProgress == QuestProgress.Available)
        {
            questIndicator.text = "?";
            questIndicator.color = new Color(1f, 0.6f, 0f);
        }
        else if (currentAvailableQuest.questProgress == QuestProgress.InProgress)
        {
            questIndicator.text = "!";
            questIndicator.color = new Color(0.9f, 0.8f, 0.8f);
        }
        else if (currentAvailableQuest.questProgress == QuestProgress.Completed)
        {
            questIndicator.text = "!";
            questIndicator.color = new Color(1f, 0.6f, 0f);
        }
        else
        {
            questIndicator.text = "";
        } 
    }

    private void SetupQuests()
    {
        NPCQuests = new List<NPCQuest>() {};

        string questsJson = Resources.Load<TextAsset>("Quests/NPC_" + NPCId).text;

        QuestObject newQuest = JsonConvert.DeserializeObject<QuestObject>(questsJson);

        for (int i = 0; i < newQuest.Quests.Count; i++)
        {
            NPCQuest npcQuest = new NPCQuest();
            npcQuest.quest = newQuest.Quests[i];
            npcQuest.npcId = NPCId;

            // get the quest progress from the saves.
            npcQuest.questProgress = QuestProgress.Available;

            NPCQuests.Add(npcQuest);
        }
        SetupDialogue();
    }

    private void SetupDialogue()
    {
        for (int i = 0; i < NPCQuests.Count; i++)
        {
            if(NPCQuests[i].questProgress == QuestProgress.Available)
            {
                foreach (string item in NPCQuests[i].quest.StartDialogue)
                {
                    currentDialogue.Add(item);
                }
                currentAvailableQuest = NPCQuests[i];
                return;
            }
            else if(NPCQuests[i].questProgress == QuestProgress.InProgress)
            {
                foreach (string item in NPCQuests[i].quest.StartDialogue)
                {
                    currentDialogue.Add(item);
                }
                currentAvailableQuest = null;
                return;
            }
            else if(NPCQuests[i].questProgress == QuestProgress.UnAvailable)
            {
                currentDialogue = null; // get generic dialogue
                currentAvailableQuest = null;
                return;
            }
            else if(NPCQuests[i].questProgress == QuestProgress.Finished && i == NPCQuests.Count - 1)
            {
                currentDialogue = null; // get generic dialogue
                currentAvailableQuest = null;
                return;
            }
            else if(NPCQuests[i].questProgress == QuestProgress.Completed)
            {
                foreach (string item in NPCQuests[i].quest.EndDialogue)
                {
                    currentDialogue.Add(item);
                }
                currentAvailableQuest = null;
                return;
            }
        }
    }

    public void TriggerDialogue(Player player)
    {
        if(dialogueTriggered != 0)
        {
            currentDialogue.Remove(currentDialogue[0]);
        }
        if (currentDialogue.Count == 0 || currentDialogue.Count == 1)
        {
            dialogueTriggered = 0;
            if (currentAvailableQuest != null)
            {
                player.TriggerQuest(currentAvailableQuest);
                currentAvailableQuest.questProgress = QuestProgress.InProgress;
                SetupQuestIndicator();
            }
            SetupDialogue();
        }
        dialogueTriggered++;
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(currentDialogue, dialogueBubble , gameObject);
    }
}