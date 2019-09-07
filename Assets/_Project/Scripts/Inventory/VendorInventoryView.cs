using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorInventoryView : MonoBehaviour
{
    public List<Slot> inventory = new List<Slot>();

    [SerializeField]
    GameObject vendorRow = null;

    [SerializeField]
    GameObject vendorItem = null;

    public void SynchInventory(VendorInventory inv)
    {
        inventory = new List<Slot>();

        // make it so whenever we open the vendor it reuses the old slots and deletes the excess slots gameobjects.

        GameObject spawnedVendorRow;
        GameObject spawnedVendorItem;
        spawnedVendorRow = null;
        for (int slot = 0; slot < inv.Slots.Count; slot++)
        {
            if (inventory.Count == 0 || inventory.Count - 1 < slot)
            {
                inventory.Add(new Slot());
            }
            if (slot % 2 == 0)
            {
                spawnedVendorRow = Instantiate(vendorRow, gameObject.transform);
                spawnedVendorRow.transform.SetAsLastSibling();
            }
            spawnedVendorItem = Instantiate(vendorItem, spawnedVendorRow.transform);
            spawnedVendorItem.transform.SetAsLastSibling();
            inventory[slot] = spawnedVendorItem.GetComponent<VendorSlotView>().slot;
            /////
            inv.Slots[slot].icon = inventory[slot].icon;
            inv.Slots[slot].amountText = inventory[slot].amountText;
            inventory[slot].ItemInSlot = inv.Slots[slot].ItemInSlot;
            inventory[slot] = inv.Slots[slot];
            if (inv.Slots[slot].ItemInSlot != null)
            {
                inv.Slots[slot].SetAmount();
                inv.Slots[slot].SetIconColorAlpha();
            }
        }
    }
}
