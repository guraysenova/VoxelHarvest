using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Item
{
    public override string TypeId
    {
        get
        {
            return "Fish";
        }
    }

    public override Item New()
    {
        return new Fish();
    }

    public override void Use()
    {

    }
}
