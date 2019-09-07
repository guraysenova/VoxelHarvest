using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    bool stop = false;

    [SerializeField]
    GameObject fishingSliderGO = null;

    int startVal = 0;
    int endVal = 100;

    float lerpTime = 1f;
    float currentLerpTime;


    public void StartFishingGame()
    {

    }

    protected void Update()
    {
        //reset when we press spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stop = !stop;
        }
        if (stop)
        {
            return;
        }

        float perc;

        if(fishingSliderGO.GetComponent<Slider>().value == 100)
        {
            startVal = 100;
            endVal = 0;

            currentLerpTime = 0f;
            perc = 0f;
        }
        else if(fishingSliderGO.GetComponent<Slider>().value == 0)
        {
            startVal = 0;
            endVal = 100;

            currentLerpTime = 0f;
            perc = 0f;
        }


        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //lerp!
        perc = currentLerpTime / lerpTime;

        fishingSliderGO.GetComponent<Slider>().value = Mathf.Lerp(startVal, endVal, perc);

    }

}
