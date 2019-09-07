using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Inventory chestInventory = new Inventory();

	void Start ()
    {
        AddLoot();
    }

    void AddLoot()
    {
        chestInventory.Slots = new List<Slot>() { new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot(), new Slot()};
    }
}