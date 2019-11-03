using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HourController : MonoBehaviour
{
    [SerializeField]
    private bool continueCycle = true;

    [SerializeField]
    private TextMeshProUGUI textmesh = null;

    int hour = 21;

    float minute = 0;

    string placeHolderZeroMinutes;
    string placeHolderZeroHours;

    public static HourController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        if (continueCycle)
        {          
            minute += Time.deltaTime * 10;
            if (minute >= 60.0f)
            {
                hour++;
                minute = 0f;
            }
            if (hour == 24)
            {
                hour = 0;
                placeHolderZeroHours = "0";
            }
            if(hour != 0)
            {
                placeHolderZeroHours = "";
            }
            if ((int)minute > 9)
            {
                placeHolderZeroMinutes = "";
            }
            else
            {
                placeHolderZeroMinutes = "0";
            }
            textmesh.text = placeHolderZeroHours + hour + ":" + placeHolderZeroMinutes + (int)minute;
            SkyDomeColorController.instance.CheckHour(hour, (int)minute);
        }
    }
}