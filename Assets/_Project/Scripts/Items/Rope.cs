using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Item
{
    public override string TypeId
    {
        get
        {
            return "Rope";
        }
    }

    public override Item New()
    {
        return new Rope();
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
