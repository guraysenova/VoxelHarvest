using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    public Item item;

    public string itemID;

    private void Start()
    {
        item = GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID(itemID);
    }
}
