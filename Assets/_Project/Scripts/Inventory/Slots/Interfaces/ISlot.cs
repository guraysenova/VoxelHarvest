using UnityEngine;

public interface ISlot
{
    bool CanPlaceItem { get; set; }

    int ItemSize { get; set; }

    Item ItemInSlot { get; set; }

    Sprite ItemSprite { get; }  
}