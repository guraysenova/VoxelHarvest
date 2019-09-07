using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VendorSlotView : MonoBehaviour
{
    public Slot slot = new Slot();

    [SerializeField]
    GameObject itemName = null;

    [SerializeField]
    GameObject itemPrice = null;

    private void Start()
    {
        itemName.GetComponent<TextMeshProUGUI>().text = slot.ItemInSlot.Name;

        itemPrice.GetComponent<TextMeshProUGUI>().text = slot.ItemInSlot.Price.ToString();
    }
}
