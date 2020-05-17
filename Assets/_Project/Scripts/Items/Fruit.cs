using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Item
{
    public override string TypeId
    {
        get
        {
            return "Fruit";
        }
    }

    public override Item New()
    {
        return new Fruit();
    }

    public override void Use()
    {

    }
}
