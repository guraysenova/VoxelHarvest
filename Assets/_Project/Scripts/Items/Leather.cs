using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leather : Item
{
    public override string TypeId
    {
        get
        {
            return "Leather";
        }
    }

    public override Item New()
    {
        return new Leather();
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
