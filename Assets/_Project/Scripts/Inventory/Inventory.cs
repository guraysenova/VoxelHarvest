using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : IContainer
{
    [SerializeField]
    List<string> owners;

    [SerializeField]
    List<Slot> slots;

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
                    return amount;
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
                    return amount;
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
        return amount - amountLeft;
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
            if (slots[slotIndex].ItemInSlot != null && slots[slotIndex].ItemInSlot.Id == item.Id)
            {
                int stacksOfItem = ((amount + item.MaxStackSize - 1) / item.MaxStackSize);
                if (stacksOfItem == 1)
                {
                    if (slots[slotIndex].ItemSize == amount)
                    {
                        slots[slotIndex].ItemSize = 0;
                        slots[slotIndex].ItemInSlot = null;
                        slots[slotIndex].ItemSprite = null;
                        removedAmount = amount;
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