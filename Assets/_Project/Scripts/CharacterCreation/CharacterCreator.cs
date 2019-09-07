using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreator : MonoBehaviour {

    [SerializeField]
    GameObject characterShirt = null;
    [SerializeField]
    GameObject characterHair = null;
    [SerializeField]
    GameObject characterHead = null;
    [SerializeField]
    GameObject handR = null;
    [SerializeField]
    GameObject handL = null;

    public void UpdateCharacter(Color myColor , string colorName)
    {
        if (colorName == "Eye")
        {
            characterHead.GetComponent<Renderer>().materials[4].color = myColor;
            characterHead.GetComponent<Renderer>().materials[5].color = new Color( (myColor.r +0.07f) , (myColor.g + 0.07f), (myColor.b + 0.07f));
        }
        else if (colorName == "Hair")
        {
            characterHair.GetComponent<Renderer>().material.color = myColor;
        }
        else if (colorName == "Skin")
        {
            characterHead.GetComponent<Renderer>().materials[0].color = myColor;
            if(handL != null || handR != null)
            {
                handR.GetComponent<Renderer>().material.color = myColor;
                handL.GetComponent<Renderer>().material.color = myColor;
            }
        }
        else if (colorName == "ShirtP")
        {
            characterShirt.GetComponent<Renderer>().materials[0].color = myColor;
        }
        else if (colorName == "ShirtS")
        {
            characterShirt.GetComponent<Renderer>().materials[1].color = myColor;
        }
    }


}
