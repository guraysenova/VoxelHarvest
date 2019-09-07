using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Item
{
    public override string TypeId
    {
        get
        {
            return "Flower";
        }
    }

    public override Item New()
    {
        return new Flower();
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}