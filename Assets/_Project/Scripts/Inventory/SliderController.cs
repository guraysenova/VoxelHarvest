using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    GameObject slider = null;
    [SerializeField]
    GameObject content = null;

    public void Slide(string upDown)
    {
        float mul = 1f;
        if(upDown == "Down")
        {
            mul = -1f;
        }

        int rows = 1;

        if (content.GetComponent<VendorInventoryView>())
        {
            rows = (content.GetComponent<VendorInventoryView>().inventory.Count + 1) / 2;
        }
        else
        {

        }

        float slideValue = ((1f / rows) * 2f) * mul;

        slider.GetComponent<Scrollbar>().value += slideValue;
    }
}