using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBrewer : Item
{
    public override string TypeId
    {
        get
        {
            return "Potion_Brewer";
        }
    }

    public override Item New()
    {
        return new PotionBrewer();
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}