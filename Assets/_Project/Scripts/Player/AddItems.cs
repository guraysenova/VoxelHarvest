using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItems : MonoBehaviour {

    [SerializeField]
    GameObject chest = null;

    [SerializeField]
    GameObject vendor = null;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Advanced_Health_Potion"), 5);
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Basic_Health_Potion"), 5);
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Health_Potion"), 2);
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Fish"), 10);
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Rope"), 10);
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Leather"), 10);
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Flower"), 10);
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Water_Bottle"), 10);
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Crop_Fruit"), 10);
            gameObject.GetComponent<Player>().UpdateItemDictionary();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            gameObject.GetComponent<Player>().PlayerInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Nine_Slot_Bag"), 2);
            gameObject.GetComponent<Player>().UpdateItemDictionary();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            chest.GetComponent<Chest>().chestInventory.AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Health_Potion"), 2);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            vendor.GetComponent<VendorInventory>().AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Health_Potion"), 2);
            vendor.GetComponent<VendorInventory>().AddItem(GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>().GetItemFromID("Nine_Slot_Bag"), 2);
        }
    }
}
