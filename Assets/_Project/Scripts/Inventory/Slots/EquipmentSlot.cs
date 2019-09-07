using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : Slot, IEquipmentSlot
{
    [SerializeField]
    EquipmentType equipmentType = new EquipmentType();

    public EquipmentType SlotEquipmentType()
    {
        return equipmentType;
    }
}
