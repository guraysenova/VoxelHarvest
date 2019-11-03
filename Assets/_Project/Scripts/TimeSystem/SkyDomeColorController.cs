using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkyDomeColorController : MonoBehaviour 
{
    [SerializeField]
    List<SeasonColorSets> seasonColorSets = new List<SeasonColorSets>();

    SeasonColorSets currentSeasonColorSet = new SeasonColorSets();

    [SerializeField]
    GameObject dome;

    public static SkyDomeColorController instance;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        currentSeasonColorSet = seasonColorSets[0];
    }

    void ChangeColor(Color targetTopColor , Color targetBottomColor)
    {
        dome.GetComponent<MeshRenderer>().material.DOColor(targetTopColor, "_ColorTop", 5f);
        dome.GetComponent<MeshRenderer>().material.DOColor(targetBottomColor, "_ColorBottom", 5f);
    }

    public void CheckHour(int hour , int minute)
    {
        string myHour;
        if (hour < 9)
        {
            myHour = "0" + hour.ToString();
        }
        else
        {
            myHour = hour.ToString();
        }
        string myMinute;
        if (minute < 9)
        {
            myMinute = "0" + minute.ToString();
        }
        else
        {
            myMinute = minute.ToString();
        }

        string time = myHour + myMinute;

        foreach (DayPartColorSet set in currentSeasonColorSet.colorSets)
        {
            if(set.dayPartStartTime == time)
            {
                ChangeColor(set.upperDomeColor, set.lowerDomeColor);
            }
        }
    }
    
}