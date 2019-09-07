using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    string ID { get; }
    string Name { get; }

    bool IsHost { get; }

    Inventory PlayerInventory { get; }

    string Balance { get; }
}