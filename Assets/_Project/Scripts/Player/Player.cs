using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IPlayer , IHaveHealth
{
    [SerializeField]
    private string playerName = null;
    [SerializeField]
    private string id = null;
    [SerializeField]
    private string balance = null;

    [SerializeField]
    private float health = 100f;

    [SerializeField]
    private bool isHost = false;

    [SerializeField]
    private Inventory playerInventory = null;

    public GameObject userInventory = null;
    public GameObject healthGO = null;

    [SerializeField]
    List<NPCQuest> activeQuests = new List<NPCQuest>();

    [SerializeField]
    List<NPCQuest> finishedQuests = new List<NPCQuest>();

    public Dictionary<string, int> items = new Dictionary<string, int>();

    private void Start()
    {
        userInventory.GetComponent<InventoryView>().SynchInventory(playerInventory);
        Health = 100f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            UpdateItemDictionary();  // fix this
        }
    }

    public void TriggerQuest(NPCQuest quest)
    {
        activeQuests.Add(quest);
    }

    public void UpdateItemDictionary()
    {
        items = new Dictionary<string, int>();
        // we got all the items from inventory and created a dictionary with them. now all we gotta do is to check for entries in this dictionary and match them to update our progress.
        for (int i = 0; i < playerInventory.Slots.Count; i++)
        {
            try
            {
                if (playerInventory.Slots[i].ItemInSlot != null)
                {
                    items.Add(playerInventory.Slots[i].ItemInSlot.Id, playerInventory.Slots[i].ItemSize);
                }
            }
            catch (ArgumentException)
            {
                items[playerInventory.Slots[i].ItemInSlot.Id] += playerInventory.Slots[i].ItemSize;
            }
        }
        GameObject.Find("ItemDataBase").GetComponent<Craft>().UpdateCraftableStatus(items);
    }

    public void UpdateQuestProgress()
    {
        UpdateItemDictionary();
        for (int i = 0; i < activeQuests.Count; i++)
        {
            bool isQuestDone = true;
            for (int y = 0; y < activeQuests[i].quest.Tasks.Count; y++)
            {
                for (int z = 0; z < activeQuests[i].quest.Tasks[y].TaskGoals.Count; z++)
                {
                    if (items.ContainsKey(activeQuests[i].quest.Tasks[y].TaskGoals[z].goalID) && 
                        items[activeQuests[i].quest.Tasks[y].TaskGoals[z].goalID] == activeQuests[i].quest.Tasks[y].TaskGoals[z].amount) // possible bug here.
                    {
                        isQuestDone = true;
                    }
                    else
                    {
                        isQuestDone = false; // possible bug
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

    public void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;
        if(Health <= 0f)
        {
            HealthBelowZero();
        }
        healthGO.GetComponent<Image>().fillAmount = Health / 100;
    }

    public void HealthBelowZero()
    {
        Health = 0;
        Debug.Log("you ded");
    }

    public string ID
    {
        get
        {
            return id;
        }
    }

    public string Name
    {
        get
        {
            return playerName;
        }
        private set
        {
            playerName = value;
        }
    }

    public string Balance
    {
        get
        {
            return balance;
        }
    }

    public bool IsHost
    {
        get
        {
            return isHost;
        }
    }

    public Inventory PlayerInventory
    {
        get
        {
            return playerInventory;
        }
    }

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }
}