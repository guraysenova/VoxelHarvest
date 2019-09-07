using System.Collections;
using System.Collections.Generic;

public interface IContainer
{
    List<string> Owners { get; set; }

    List<Slot> Slots { get; set; }

    int AddItem(Item item , int amount);
    int AddItem(Slot addedSlot , Item item , int amount);

    int RemoveItem(Slot removedSlot , int amount , Slot addedSlot);
    int RemoveItem(Item item , int amount);

    void SwapItems(Slot addedSlot , Slot removedSlot);
}