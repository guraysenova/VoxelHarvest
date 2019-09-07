using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineSlotBag : Item , IBag
{
    public override string TypeId
    {
        get
        {
            return "Nine_Slot_Bag";
        }
    }

    public int Slots
    {
        get
        {
            return 9;
        }
    }

    public EquipmentType GetEquipmentType()
    {
        return EquipmentType.Backpack;
    }

    public override Item New()
    {
        return new NineSlotBag();
    }

    public override void Use()
    {
        Wear();
    }

    public void Wear()
    {
        //Wear item
    }
}
