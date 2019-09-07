using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    public override string TypeId
    {
        get
        {
            return "Health_Potion";
        }
    }

    public override Item New()
    {
        return new HealthPotion();
    }

    public override void Use()
    {
        //use the item
    }
}