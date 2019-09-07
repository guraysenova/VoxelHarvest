using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public void QuestAccepted(NPCQuest quest, Player player)
    {
        quest.questProgress = QuestProgress.InProgress;
    }
    public void UpdateProgress(List<NPCQuest> activeQuests , Player player)
    {
        // we got all the items from inventory and created a dictionary with them. now all we gotta do is to check for entries in this dictionary and match them to update our progress.
        Dictionary<string, int> items = new Dictionary<string, int>();
        for (int i = 0; i < player.PlayerInventory.Slots.Count; i++)
        {
            try
            {
                if (player.PlayerInventory.Slots[i].ItemInSlot != null)
                {
                    items.Add(player.PlayerInventory.Slots[i].ItemInSlot.Id, player.PlayerInventory.Slots[i].ItemSize);
                }
            }
            catch (ArgumentException)
            {
                items[player.PlayerInventory.Slots[i].ItemInSlot.Id] += player.PlayerInventory.Slots[i].ItemSize;
            }
        }

        for (int i = 0; i < activeQuests.Count; i++)
        {
            bool isQuestDone = false;
            for (int y = 0; y < activeQuests[i].quest.Tasks.Count; y++)
            {
                for (int z = 0; z < activeQuests[i].quest.Tasks[y].TaskGoals.Count; z++)
                {
                    if (items.ContainsKey(activeQuests[i].quest.Tasks[y].TaskGoals[z].goalID) &&
                        items[activeQuests[i].quest.Tasks[y].TaskGoals[z].goalID] == activeQuests[i].quest.Tasks[y].TaskGoals[z].amount)
                    {
                        isQuestDone = true;
                    }
                    else
                    {
                        isQuestDone = false;
                        break;
                    }
                }
                if(isQuestDone == false)
                {
                    break;
                }
            }

            if (isQuestDone == true)
            {
                activeQuests[i].questProgress = QuestProgress.Finished;
            }
        }
    }
    public void QuestCompleted(NPCQuest quest , Player player)
    {
        quest.questProgress = QuestProgress.Completed;
    }
    public void QuestFinished(NPCQuest quest, Player player)
    {
        quest.questProgress = QuestProgress.Finished;
    }

    void GiveReward(NPCQuest quest, Player player)
    {
        Item item = GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID(quest.quest.Rewards[0].rewardID);
        player.PlayerInventory.AddItem(item, quest.quest.Rewards[0].amount);
    }
}