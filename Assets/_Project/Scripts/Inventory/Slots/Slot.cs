using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot : ISlot
{
    [SerializeField]
    private int itemAmount;

    [SerializeField]
    private Item itemInSlot;

    [SerializeField]
    private Sprite itemIcon;

    public Image icon;

    public Text amountText;

    [SerializeField]
    bool canPlaceItem;

    public bool CanPlaceItem
    {
        get
        {
            return canPlaceItem;
        }
        set
        {
            canPlaceItem = value;
        }
    }

    public Sprite ItemSprite
    {
        get
        {
            if (itemInSlot != null)
            {
                return itemInSlot.ItemIcon;
            }
            else
            {
                return null;
            }
        }
        set
        {
            itemIcon = value;
            SetIconColorAlpha();
        }
    }

    public Item ItemInSlot
    {
        get
        {
            return itemInSlot;
        }
        set
        {
            if(value != null)
            {
                ItemSprite = value.ItemIcon;
            }
            else
            {
                ItemSprite = null;
            }
            itemInSlot = value;
        }
    }

    public int ItemSize
    {
        get
        {
            return itemAmount;
        }
        set
        {
            itemAmount = value;
            if(amountText!= null)
            {
                SetAmount();
            }
        }
    }

    public void SetIconColorAlpha()
    {
        if(itemIcon == null)
        {
            icon.sprite = null;
            icon.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            if (icon == null)
            {
                return;
            }
            icon.sprite = itemIcon;
            icon.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void SetAmount()
    {
        if(amountText == null)
        {
            return;
        }

        if(itemAmount <= 1)
        {
            amountText.text = "";
        }
        else
        {
            amountText.text = itemAmount.ToString();
        }
    }
}