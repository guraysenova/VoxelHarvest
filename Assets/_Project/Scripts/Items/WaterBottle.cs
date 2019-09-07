using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBottle : Item
{
    public override string TypeId
    {
        get
        {
            return "Water_Bottle";
        }
    }

    public override Item New()
    {
        return new WaterBottle();
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
