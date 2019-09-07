using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public List<SlotView> inventory = new List<SlotView>();

    public void SynchInventory(Inventory inv)
    {
        for (int i = 0; i < inv.Slots.Count ; i++)
        {
            inv.Slots[i].icon = inventory[i].slot.icon;
            inv.Slots[i].amountText = inventory[i].slot.amountText;
            inventory[i].slot = inv.Slots[i];
            if (inv.Slots[i].ItemInSlot != null)
            {
                inv.Slots[i].SetAmount();
                inv.Slots[i].SetIconColorAlpha();
            }
        }
    }
}
