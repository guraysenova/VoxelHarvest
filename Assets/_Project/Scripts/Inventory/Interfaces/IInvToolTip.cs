using UnityEngine;

public interface IInvToolTip
{
    string Name { set; }

    string Description { set; }

    int Price { set; }

    Sprite ItemIcon { set; }
}