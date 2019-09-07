using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChatCommand : MonoBehaviour
{
    [SerializeField]
    InputField textField = null;

    [SerializeField]
    GameObject contentGO = null;

    [SerializeField]
    GameObject textPrefab = null;

    [SerializeField]
    GameObject sliderGO = null;


    public void ChatEntry()
    {
        if (textField.text == null || textField.text == "")
        {
            return;
        }

        string[] words = textField.text.Split(' ');

        string myText = textField.text;

        textField.text = "";

        if (words[0].Equals("/give", StringComparison.InvariantCultureIgnoreCase))
        {
            if (words.Length != 4)
            {
                SendChatMessage("Correct format is \"/give username itemID amount(0-100)\" ");
                return;
            }

            int amount = 0;
            try
            {
                amount = Convert.ToInt32(words[3]);
            }
            catch
            {
                SendChatMessage("Correct format is \"/give username itemID amount(0-100)\" ");
                return;
            }

            GiveItem(words[1], words[2], amount);
        }
        else
        {
            SendChatMessage(myText);
        }
    }

    private void GiveItem(string userName, string itemId, int amount)
    {
        SendChatMessage("Give " + userName + ", item " + itemId + ", amount " + amount);
        // find the item and give it to userName
    }

    private void SendChatMessage(string text)
    {
        DateTime time = DateTime.Now;
        GameObject textItem = Instantiate(textPrefab, contentGO.transform);
        textItem.GetComponent<Text>().text = time.ToShortTimeString() + " - " + text;
        sliderGO.GetComponent<Scrollbar>().value = 0f;
    }
}