using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipView : MonoBehaviour, IInvToolTip
{
    string itemName;
    string description;

    int price;

    //Sprite itemIcon = null;

    [SerializeField]
    Text itemNameText = null;
    [SerializeField]
    Text descriptionText = null;
    [SerializeField]
    Text priceText = null;
    [SerializeField]
    Image itemIconImage = null;
    [SerializeField]
    GameObject recipeGO = null;
    [SerializeField]
    GameObject recipeItemsGO = null;
    [SerializeField]
    GameObject ingredientPrefab = null;
    [SerializeField]
    GameObject objectPoolGO = null;
    [SerializeField]
    Text requiredMachineText = null;

    GameObject itemDataBaseGO = null;

    List<GameObject> existingIngredients = new List<GameObject>();

    private void Start()
    {
        itemDataBaseGO = GameObject.Find("ItemDataBase");
    }

    public string Description
    {
        set
        {
            descriptionText.text = value;
        }
    }

    public int Price
    {
        set
        {
            priceText.text = "Price : " + value.ToString();
        }
    }

    public Sprite ItemIcon
    {
        set
        {
            itemIconImage.sprite = value;
            SetIconColorAlpha();
        }
    }

    public string Name
    {
        set
        {
            itemNameText.text = value;
        }
    }

    public void SetIconColorAlpha()
    {
        if (itemIconImage.sprite == null)
        {
            itemIconImage.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            itemIconImage.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void SetToolTip(string myName , int myPrice , string myDescription , Sprite myIcon)
    {
        recipeGO.SetActive(false);
        descriptionText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(250f, 257f);
        Description = myDescription;
        Name = myName;
        Price = myPrice;
        ItemIcon = myIcon;
    }

    public void SetToolTip(string myName, int myPrice, string myDescription, Sprite myIcon , Recipe recipe)
    {
        recipeGO.SetActive(true);
        descriptionText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(250f, 134f);
        Description = myDescription;
        Name = myName;
        Price = myPrice;
        ItemIcon = myIcon;

        if(existingIngredients.Count > recipe.ingredients.Count)
        {
            for (int i = 0; i < existingIngredients.Count - recipe.ingredients.Count; i++)
            {
                existingIngredients[existingIngredients.Count - 1].transform.SetParent(objectPoolGO.transform);
            }
        }
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            if(existingIngredients.Count >= i+1)
            {
                if(existingIngredients[i].transform.parent != recipeItemsGO.transform)
                {
                    existingIngredients[i].transform.SetParent(recipeItemsGO.transform);
                }
                existingIngredients[i].GetComponentInChildren<Image>().sprite = itemDataBaseGO.GetComponent<ItemDataBase>().GetItemSpriteFromID(recipe.ingredients[i].itemID);
                existingIngredients[i].GetComponentInChildren<Text>().text = "x" + recipe.ingredients[i].amount.ToString();
            }
            else if(existingIngredients.Count < i+1)
            {
                GameObject ingredient = Instantiate(ingredientPrefab, recipeItemsGO.transform);
                existingIngredients.Add(ingredient);
                ingredient.GetComponentInChildren<Image>().sprite = itemDataBaseGO.GetComponent<ItemDataBase>().GetItemSpriteFromID(recipe.ingredients[i].itemID);
                ingredient.GetComponentInChildren<Text>().text = "x" + recipe.ingredients[i].amount.ToString();
            }
        }
        if (recipe.requiredMachineID != null)
        {
            requiredMachineText.gameObject.SetActive(true);
            requiredMachineText.text = "Required Machine:\n" + itemDataBaseGO.GetComponent<ItemDataBase>().GetItemNameFromID(recipe.requiredMachineID);
        }
        else
        {
            requiredMachineText.text = "";
        }
    }
}