using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreatorUI : MonoBehaviour
{
    Color[] colorArray = { Color.black , Color.blue , Color.green , Color.magenta , Color.yellow };

    Color[] skinColorArray = { new Color(1f, 0.858f, 0.67f) , new Color(0.94f, 0.76f, 0.49f), new Color(0.878f, 0.67f, 0.41f), new Color(0.77f, 0.525f, 0.258f), new Color(0.55f, 0.333f, 0.14f) };


    [Header("Color Images")]
    public RawImage eyeColorImage;
    [Space(10)]
    public Image skinColorImage;
    [Space(10)]
    public RawImage shirtPColorImage;
    public RawImage shirtSColorImage;
    [Space(10)]
    public RawImage hairColorImage;

    Color eyeColor = new Color(0,0,0);
    Color skinColor = new Color(0,0,0);
    Color shirtPColor = new Color(0,0,0);   // Shirt Primary Color
    Color shirtSColor = new Color(0,0,0);   // Shirt Secondary Color
    Color hairColor = new Color(0,0,0);

    int eyeColorIndex = 0;
    int skinColorIndex = 0;
    int shirtPColorIndex = 0;
    int shirtSColorIndex = 0;
    int hairColorIndex = 0;

    [Space(10)]
    [Header("Buttons")]
    public GameObject nextEyeColorButton;
    public GameObject prevEyeColorButton;

    [Space(10)]
    public GameObject nextSkinColorButton;
    public GameObject prevSkinColorButton;

    [Space(10)]
    public GameObject nextShirtPColorButton;
    public GameObject prevShirtPColorButton;

    [Space(10)]
    public GameObject nextShirtSColorButton;
    public GameObject prevShirtSColorButton;

    [Space(10)]
    public GameObject nextHairColorButton;
    public GameObject prevHairColorButton;




    private void Start()
    {
        eyeColor = colorArray[eyeColorIndex];
        skinColor = skinColorArray[skinColorIndex];
        shirtPColor = colorArray[shirtPColorIndex];
        shirtSColor = colorArray[shirtSColorIndex];
        hairColor = colorArray[hairColorIndex];

        eyeColorImage.color = eyeColor;
        skinColorImage.color = skinColor;
        shirtPColorImage.color = shirtPColor;
        shirtSColorImage.color = shirtSColor;
        hairColorImage.color = hairColor;
    }

    public void NextColor(string colorName)
    {
        if(colorName == "Eye")
        {
            if(eyeColorIndex == 4)
            {
                eyeColorIndex = 0;
            }
            else
            {
                eyeColorIndex++;
            }
            eyeColor = colorArray[eyeColorIndex];
            eyeColorImage.color = eyeColor;
            gameObject.GetComponent<CharacterCreator>().UpdateCharacter(eyeColor, colorName);
        }
        else if (colorName == "Hair")
        {
            if (hairColorIndex == 4)
            {
                hairColorIndex = 0;
            }
            else
            {
                hairColorIndex++;
            }
            hairColor = colorArray[hairColorIndex];
            hairColorImage.color = hairColor;
            gameObject.GetComponent<CharacterCreator>().UpdateCharacter(hairColor, colorName);
        }
        else if (colorName == "Skin")
        {
            if (skinColorIndex == 4)
            {
                skinColorIndex = 0;
            }
            else
            {
                skinColorIndex++;
            }
            skinColor = skinColorArray[skinColorIndex];
            skinColorImage.color = skinColor;
            gameObject.GetComponent<CharacterCreator>().UpdateCharacter(skinColor, colorName);
        }
        else if (colorName == "ShirtP")
        {
            if (shirtPColorIndex == 4)
            {
                shirtPColorIndex = 0;
            }
            else
            {
                shirtPColorIndex++;
            }
            shirtPColor = colorArray[shirtPColorIndex];
            shirtPColorImage.color = shirtPColor;
            gameObject.GetComponent<CharacterCreator>().UpdateCharacter(shirtPColor, colorName);
        }
        else if (colorName == "ShirtS")
        {
            if (shirtSColorIndex == 4)
            {
                shirtSColorIndex = 0;
            }
            else
            {
                shirtSColorIndex++;
            }
            shirtSColor = colorArray[shirtSColorIndex];
            shirtSColorImage.color = shirtSColor;
            gameObject.GetComponent<CharacterCreator>().UpdateCharacter(shirtSColor, colorName);
        }

        
    }
}
