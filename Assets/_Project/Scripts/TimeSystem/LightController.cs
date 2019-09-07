using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private bool continueCoroutine = true;

    [SerializeField]
    private TextMeshProUGUI textmesh = null;

    int hour = 12;

    int minute = 0;

    string placeHolderZero;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(UpdateClock());
    }

    IEnumerator UpdateClock()
    {
        while (continueCoroutine)
        { //variable that enables you to kill routine            
            yield return new WaitForSeconds(1f);
            minute++;
            if(minute == 60)
            {
                hour++;
                minute = 0;
            }
            if(hour == 24)
            {
                hour = 0;
            }
            if(minute > 9)
            {
                placeHolderZero = "";
            }
            else
            {
                placeHolderZero = "0";
            }
            textmesh.text = hour + ":" + placeHolderZero + minute;         
        }
    }
}
