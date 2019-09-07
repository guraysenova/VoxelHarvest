using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    JsonItemList items;

    List<Item> dataBase = new List<Item>() {};
    List<Item> typeDataBase = new List<Item>() { };

    public Item GetItemFromID(string ID)
    {
        for (int index = 0; index < dataBase.Count; index++)
        {
            if (dataBase[index] != null && dataBase[index].Id == ID)
            {
                Item newItem = dataBase[index].New();
                newItem.ItemIcon = dataBase[index].ItemIcon;
                newItem.MaxStackSize = dataBase[index].MaxStackSize;
                newItem.Id = dataBase[index].Id;
                newItem.Name = dataBase[index].Name;
                newItem.Price = dataBase[index].Price;
                newItem.Description = dataBase[index].Description;
                newItem.Recipe = dataBase[index].Recipe;
                return newItem;
            }
        }
        return null;
    }

    public Item GetItemTypeFromTypeID(string TypeID)
    {
        for (int index = 0; index < typeDataBase.Count; index++)
        {
            if (typeDataBase[index] != null && typeDataBase[index].TypeId == TypeID)
            {
                Item newItem = typeDataBase[index].New();
                return newItem;
            }
        }
        return null;
    }

    public Sprite GetItemSpriteFromID(string ID)
    {
        for (int index = 0; index < dataBase.Count; index++)
        {
            if (dataBase[index] != null && dataBase[index].Id == ID)
            {
                Sprite itemIcon = dataBase[index].ItemIcon;
                return itemIcon;
            }
        }
        return null;
    }

    public string GetItemNameFromID(string ID)
    {
        for (int index = 0; index < dataBase.Count; index++)
        {
            if (dataBase[index] != null && dataBase[index].Id == ID)
            {
                string itemName = dataBase[index].Name;
                return itemName;
            }
        }
        return null;
    }

    public string GetItemDescriptionFromID(string ID)
    {
        for (int index = 0; index < dataBase.Count; index++)
        {
            if (dataBase[index] != null && dataBase[index].Id == ID)
            {
                string itemDescription = dataBase[index].Description;
                return itemDescription;
            }
        }
        return null;
    }

    private void AddIconsToDatabase()
    {
        for (int i = 0; i < dataBase.Count; i++)
        {
            dataBase[i].ItemIcon = Resources.Load<Sprite>("Items/ItemIcons/" + dataBase[i].Id);
        }
    }

    private void Awake()
    {
        string itemJson = Resources.Load<TextAsset>("Items/JsonData/Main_Items").text;
        items = JsonConvert.DeserializeObject<JsonItemList>(itemJson);
        Debug.Log(JsonConvert.SerializeObject(items));
    }

    private void Start()
    {
        typeDataBase.Add(new HealthPotion());
        typeDataBase.Add(new NineSlotBag());
        typeDataBase.Add(new Fish());
        typeDataBase.Add(new WaterBottle());
        typeDataBase.Add(new Flower());
        typeDataBase.Add(new PotionBrewer());
        typeDataBase.Add(new Leather());
        typeDataBase.Add(new Rope());

        CreateItemDataBase();
        AddIconsToDatabase();

        gameObject.GetComponent<Craft>().SetUpCraftingScreen(dataBase);
    }

    private void CreateItemDataBase()
    {
        for (int i = 0; i < items.items.Count; i++)
        {
            JsonItem jsonItem = items.items[i];

            Item item = GetItemTypeFromTypeID(jsonItem.typeID);

            Debug.Log(jsonItem.maxStackSize);

            item.MaxStackSize = jsonItem.maxStackSize;
            item.Id = jsonItem.iD;
            item.Name = jsonItem.name;
            item.Price = jsonItem.price;
            item.Description = jsonItem.description;
            item.Recipe = jsonItem.recipe;

            dataBase.Add(item);
        }
    }
}