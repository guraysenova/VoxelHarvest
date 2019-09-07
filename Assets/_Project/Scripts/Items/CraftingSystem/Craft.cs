using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{
    [SerializeField]
    GameObject craftingSlotPrefab = null;
    [SerializeField]
    GameObject craftingScreenContentGO = null;
    [SerializeField]
    List<CraftingSlotView> craftableObjectSlots = new List<CraftingSlotView>(); 

    public void SetUpCraftingScreen(List<Item> dataBase)
    {
        for (int i = 0; i < dataBase.Count; i++)
        {
            if(dataBase[i].Recipe != null) // set up a button with sprite of an item if it has a recipe.
            {
                GameObject craftableItem = Instantiate(craftingSlotPrefab, craftingScreenContentGO.transform);
                /*Item newItem = dataBase[i].New();
                newItem.Recipe = dataBase[i].Recipe;
                newItem.Price = dataBase[i].Price;
                newItem.Id = dataBase[i].Id;
                newItem.Name = dataBase[i].Name;
                newItem.ItemIcon = dataBase[i].ItemIcon;
                newItem.Description = dataBase[i].Description;
                newItem.MaxStackSize = dataBase[i].MaxStackSize;*/
                craftableObjectSlots.Add(craftableItem.GetComponent<CraftingSlotView>());
                craftableItem.GetComponent<CraftingSlotView>().slot.ItemInSlot = dataBase[i];
                craftableItem.GetComponent<Image>().sprite = dataBase[i].ItemIcon;
            }
            else
            {
                Debug.Log("null");
            }
        }
        GameObject.Find("Character").GetComponent<Player>().UpdateItemDictionary();
    }

    public void UpdateCraftableStatus(Dictionary<string , int> items)
    {
        //gameObject.GetComponent<Player>().UpdateItemDictionary();

        for (int i = 0; i < craftableObjectSlots.Count; i++)
        {
            bool canCraft = false;
            Recipe recipe = craftableObjectSlots[i].slot.ItemInSlot.Recipe;
            for (int y = 0; y < recipe.ingredients.Count; y++)
            {
                if (items.ContainsKey(recipe.ingredients[y].itemID) && items[recipe.ingredients[y].itemID] >= recipe.ingredients[y].amount)
                {
                    canCraft = true;
                }
                else
                {
                    canCraft = false;
                    break;
                }
            }
            if (!canCraft)
            {
                craftableObjectSlots[i].gameObject.GetComponent<Image>().color = new Color(1,1,1,0.5f);
            }
            else
            {
                craftableObjectSlots[i].gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }



        for (int i = 0; i < craftableObjectSlots.Count; i++)
        {

        }
    }

    public bool IsCraftable()
    {
        
        return false;
    }
}