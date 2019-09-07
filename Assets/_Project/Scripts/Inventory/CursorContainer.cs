using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class CursorContainer : MonoBehaviour , IContainer
{
    [SerializeField]
    List<string> owners = null;

    [SerializeField]
    List<Slot> slots = null;

    [SerializeField]
    GameObject cursorContainerGO = null;

    [SerializeField]
    GameObject toolTipGO = null;

    [SerializeField]
    GameObject invPanel = null;


    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    [SerializeField]
    GameObject canvas = null;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = canvas.GetComponent<EventSystem>();
    }

    void Update()
    {
        if (invPanel.activeInHierarchy)
        {
            cursorContainerGO.transform.position = Input.mousePosition;

            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);
            bool allEmpty = true;
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<SlotView>() != null)
                {
                    if (result.gameObject.GetComponent<SlotView>().slot.ItemInSlot != null)
                    {
                        Item item = result.gameObject.GetComponent<SlotView>().slot.ItemInSlot;
                        allEmpty = false;
                        toolTipGO.SetActive(true);
                        toolTipGO.GetComponent<ToolTipView>().SetToolTip(item.Name , item.Price , item.Description , item.ItemIcon);
                    }
                }
                else if(result.gameObject.GetComponent<VendorSlotView>() != null)
                {
                    if (result.gameObject.GetComponent<VendorSlotView>().slot.ItemInSlot != null)
                    {
                        Item item = result.gameObject.GetComponent<VendorSlotView>().slot.ItemInSlot;
                        allEmpty = false;
                        toolTipGO.SetActive(true);
                        toolTipGO.GetComponent<ToolTipView>().SetToolTip(item.Name, item.Price, item.Description, item.ItemIcon);
                    }
                }
                else if(result.gameObject.GetComponent<CraftingSlotView>() != null)
                {
                    if (result.gameObject.GetComponent<CraftingSlotView>().slot.ItemInSlot != null)
                    {
                        Item item = result.gameObject.GetComponent<CraftingSlotView>().slot.ItemInSlot;
                        allEmpty = false;
                        toolTipGO.SetActive(true);
                        toolTipGO.GetComponent<ToolTipView>().SetToolTip(item.Name, item.Price, item.Description, item.ItemIcon , item.Recipe);
                    }
                }
                if (allEmpty)
                {
                    toolTipGO.SetActive(false);
                }
            }
            allEmpty = true;

            //Check if the left Mouse button is clicked
            if (Input.GetMouseButtonUp(0))
            {
                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.GetComponent<SlotView>() != null)
                    {
                        SwapItems(result.gameObject.GetComponent<SlotView>().slot, slots[0]);
                    }
                    else if(result.gameObject.GetComponent<CraftingSlotView>() != null)
                    {
                        bool canCraft = false;

                        Recipe recipe = result.gameObject.GetComponent<CraftingSlotView>().slot.ItemInSlot.Recipe;

                        gameObject.GetComponent<Player>().UpdateItemDictionary();

                        Dictionary<string, int> items = gameObject.GetComponent<Player>().items;

                        for (int i = 0; i < recipe.ingredients.Count; i++)
                        {
                            if(items.ContainsKey(recipe.ingredients[i].itemID) && items[recipe.ingredients[i].itemID] >= recipe.ingredients[i].amount)
                            {
                                canCraft = true;
                            }
                            else
                            {
                                canCraft = false;
                                break;
                            }
                        }
                        if (canCraft == true)
                        {
                            ItemDataBase dataBase = GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>();
                            Item item = result.gameObject.GetComponent<CraftingSlotView>().slot.ItemInSlot;

                            if(slots[0].ItemInSlot != null)
                            {
                                if(slots[0].ItemInSlot.Id == item.Id && slots[0].ItemSize < item.MaxStackSize)
                                {
                                    AddItem(dataBase.GetItemFromID(item.Id), 1);
                                    for (int i = 0; i < recipe.ingredients.Count; i++)
                                    {
                                        gameObject.GetComponent<Player>().PlayerInventory.RemoveItem(dataBase.GetItemFromID(recipe.ingredients[i].itemID), recipe.ingredients[i].amount);
                                    }
                                }
                                else
                                {
                                    if(gameObject.GetComponent<Player>().PlayerInventory.AddItem(dataBase.GetItemFromID(item.Id), 1) > 0)
                                    {
                                        for (int i = 0; i < recipe.ingredients.Count; i++)
                                        {
                                            gameObject.GetComponent<Player>().PlayerInventory.RemoveItem(dataBase.GetItemFromID(recipe.ingredients[i].itemID), recipe.ingredients[i].amount);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AddItem(dataBase.GetItemFromID(item.Id), 1);
                                for (int i = 0; i < recipe.ingredients.Count; i++)
                                {
                                    gameObject.GetComponent<Player>().PlayerInventory.RemoveItem(dataBase.GetItemFromID(recipe.ingredients[i].itemID), recipe.ingredients[i].amount);
                                }
                            }
                            gameObject.GetComponent<Player>().UpdateItemDictionary();
                        }
                    }
                }
            }
        }
        else
        {
            toolTipGO.SetActive(false);
        }
    }

    public List<string> Owners
    {
        get
        {
            return owners;
        }

        set
        {
            owners = value;
        }
    }

    public List<Slot> Slots
    {
        get
        {
            return slots;
        }
        set
        {
            slots = value;
        }
    }

    public int AddItem(Item item, int amount)
    {
        int amountLeft = amount;
        for (int slotIndex = 0; slotIndex < slots.Count; slotIndex++)
        {
            if (slots[slotIndex].ItemInSlot == null)
            {
                slots[slotIndex].ItemInSlot = item;
                slots[slotIndex].ItemSprite = item.ItemIcon;

                int stacksOfItem = ((amount + item.MaxStackSize - 1) / item.MaxStackSize);
                if (stacksOfItem == 1)
                {
                    slots[slotIndex].ItemSize = amount;
                    amountLeft = 0;
                    return amountLeft;
                }
                else
                {
                    slots[slotIndex].ItemSize = item.MaxStackSize;
                    return AddItem(item, amount - item.MaxStackSize);
                }
            }
            else if (slots[slotIndex].ItemInSlot.Id == item.Id && slots[slotIndex].ItemSize == item.MaxStackSize)
            {

            }
            else if (slots[slotIndex].ItemInSlot.Id == item.Id && slots[slotIndex].ItemSize < item.MaxStackSize)
            {
                int totalSize = slots[slotIndex].ItemSize + amount;

                int stacksOfItem = ((totalSize + item.MaxStackSize - 1) / item.MaxStackSize);
                if (stacksOfItem == 1)
                {
                    slots[slotIndex].ItemSize = totalSize;
                    amountLeft = 0;
                    return amountLeft;
                }
                else
                {
                    slots[slotIndex].ItemSize = item.MaxStackSize;
                    return AddItem(item, totalSize - item.MaxStackSize);
                }
            }
            else if (slots[slotIndex].ItemInSlot != item)
            {

            }
        }
        return amountLeft;
    }

    public int AddItem(Slot addedSlot, Item item, int amount)
    {
        int amountLeft = amount;
        if (addedSlot.ItemInSlot == null)
        {
            addedSlot.ItemInSlot = item;
            addedSlot.ItemSprite = item.ItemIcon;

            int stacksOfItem = ((amount + item.MaxStackSize - 1) / item.MaxStackSize);
            if (stacksOfItem == 1)
            {
                addedSlot.ItemSize = amount;
                amountLeft = 0;
            }
            else
            {
                addedSlot.ItemSize = item.MaxStackSize;
                amountLeft = amount - item.MaxStackSize;
            }
        }
        else if (addedSlot.ItemInSlot.Id == item.Id && addedSlot.ItemSize == item.MaxStackSize)
        {

        }
        else if (addedSlot.ItemInSlot.Id == item.Id && addedSlot.ItemSize < item.MaxStackSize)
        {
            int totalSize = addedSlot.ItemSize + amount;

            int stacksOfItem = ((totalSize + item.MaxStackSize - 1) / item.MaxStackSize);
            if (stacksOfItem == 1)
            {
                addedSlot.ItemSize = totalSize;
                amountLeft = 0;
            }
            else
            {
                addedSlot.ItemSize = item.MaxStackSize;
                amountLeft = totalSize - item.MaxStackSize;
            }
        }
        else if (addedSlot.ItemInSlot != item)
        {

        }
        return amountLeft;
    }

    public int RemoveItem(Slot removedSlot, int amount, Slot addedSlot)
    {
        int removedAmount = amount;

        if (removedSlot.ItemInSlot == null)
        {
            removedAmount = 0;
            return removedAmount;
        }
        else if (removedSlot.ItemInSlot.Id != addedSlot.ItemInSlot.Id && addedSlot.ItemInSlot != null)
        {
            removedAmount = 0;
            return removedAmount;
        }
        else if (removedSlot.ItemInSlot.Id == addedSlot.ItemInSlot.Id && addedSlot.ItemInSlot != null)
        {
            int totalAmount = removedSlot.ItemSize + addedSlot.ItemSize;

            int stacksOfItem = ((totalAmount + removedSlot.ItemInSlot.MaxStackSize - 1) / removedSlot.ItemInSlot.MaxStackSize);

            if (stacksOfItem == 1)
            {
                addedSlot.ItemSize = totalAmount;
                removedSlot.ItemInSlot = null;
                removedSlot.ItemSize = 0;
                removedSlot.ItemSprite = null;
                return removedAmount;
            }
            else
            {
                int oldSize = addedSlot.ItemSize;
                addedSlot.ItemSize = removedSlot.ItemInSlot.MaxStackSize;
                removedAmount = removedSlot.ItemInSlot.MaxStackSize - oldSize;
                return removedAmount;
            }
        }
        else if (removedSlot.ItemInSlot != null && addedSlot.ItemInSlot == null)
        {
            addedSlot.ItemInSlot = removedSlot.ItemInSlot;
            addedSlot.ItemSprite = removedSlot.ItemSprite;
            addedSlot.ItemSize = removedSlot.ItemSize;

            removedSlot.ItemInSlot = null;
            removedSlot.ItemSize = 0;
            removedSlot.ItemSprite = null;

            return removedAmount;
        }
        removedAmount = 0;
        return removedAmount;
    }

    public int RemoveItem(Item item, int amount)
    {
        int removedAmount = 0;

        for (int slotIndex = 0; slotIndex < slots.Count; slotIndex++)
        {
            if (slots[slotIndex].ItemInSlot.Id == item.Id)
            {
                int stacksOfItem = ((amount + item.MaxStackSize - 1) / item.MaxStackSize);
                if (stacksOfItem == 1)
                {
                    if (slots[slotIndex].ItemSize == amount)
                    {
                        slots[slotIndex].ItemSize = 0;
                        slots[slotIndex].ItemInSlot = null;
                        slots[slotIndex].ItemSprite = null;
                        return removedAmount;
                    }
                    else if (slots[slotIndex].ItemSize > amount)
                    {
                        slots[slotIndex].ItemSize -= amount;
                        removedAmount = amount;
                        return removedAmount;
                    }
                    else
                    {
                        int myRemovedAmount = slots[slotIndex].ItemSize;
                        slots[slotIndex].ItemSize = 0;
                        slots[slotIndex].ItemInSlot = null;
                        slots[slotIndex].ItemSprite = null;
                        return myRemovedAmount + RemoveItem(item, amount - myRemovedAmount);
                    }
                }
                else
                {
                    int myRemovedAmount = slots[slotIndex].ItemSize;
                    slots[slotIndex].ItemSize = 0;
                    slots[slotIndex].ItemInSlot = null;
                    slots[slotIndex].ItemSprite = null;
                    return myRemovedAmount + RemoveItem(item, amount - myRemovedAmount);
                }
            }
        }
        return removedAmount;
    }

    public virtual void SwapItems(Slot addedSlot, Slot removedSlot)
    {
        if (addedSlot.ItemInSlot != null && removedSlot.ItemInSlot != null && removedSlot.ItemInSlot.Id == addedSlot.ItemInSlot.Id)
        {
            int totalSize = removedSlot.ItemSize + addedSlot.ItemSize;

            int stacksOfItem = ((totalSize + removedSlot.ItemInSlot.MaxStackSize - 1) / removedSlot.ItemInSlot.MaxStackSize);

            if (stacksOfItem == 1)
            {
                addedSlot.ItemSize = totalSize;

                removedSlot.ItemInSlot = null;
                removedSlot.ItemSize = 0;
                removedSlot.ItemSprite = null;
            }
            else
            {
                addedSlot.ItemSize = addedSlot.ItemInSlot.MaxStackSize;

                removedSlot.ItemSize = totalSize - addedSlot.ItemInSlot.MaxStackSize;
            }
        }
        else
        {
            Item myItem = addedSlot.ItemInSlot;
            Sprite mySprite = addedSlot.ItemSprite;
            int myItemSize = addedSlot.ItemSize;

            addedSlot.ItemInSlot = removedSlot.ItemInSlot;
            addedSlot.ItemSize = removedSlot.ItemSize;
            addedSlot.ItemSprite = removedSlot.ItemSprite;

            removedSlot.ItemInSlot = myItem;
            removedSlot.ItemSize = myItemSize;
            removedSlot.ItemSprite = mySprite;
        }
    }
}
