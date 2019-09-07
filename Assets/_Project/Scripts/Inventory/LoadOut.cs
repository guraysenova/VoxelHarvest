using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOut : MonoBehaviour, IContainer
{
    [SerializeField]
    List<string> owners;

    [SerializeField]
    List<Slot> slots;

    public List<string> Owners
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }

    public List<Slot> Slots
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }

    public int AddItem(Item item, int amount)
    {
        throw new System.NotImplementedException();
    }

    public int AddItem(Slot addedSlot, Item item, int amount)
    {
        throw new System.NotImplementedException();
    }

    public int RemoveItem(Slot removedSlot, int amount , Slot addedSlot)
    {
        throw new System.NotImplementedException();
    }

    public int RemoveItem(Item item, int amount)
    {
        throw new System.NotImplementedException();
    }

    public void SwapItems(Slot addedSlot, Slot removedSlot)
    {
        throw new System.NotImplementedException();
    }
}
