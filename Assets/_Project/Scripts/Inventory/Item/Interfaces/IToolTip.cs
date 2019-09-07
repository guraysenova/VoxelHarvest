using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IToolTip
{
    string Name { get; }

    string Description { get; }

    int Price { get; }

    Sprite ItemIcon { get; }
}