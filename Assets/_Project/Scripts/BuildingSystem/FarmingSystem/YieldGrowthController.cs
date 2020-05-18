using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldGrowthController : MonoBehaviour
{
    bool fullyGrown;
    float time, timeToFullGrowth;

    private void Update()
    {
        if (!fullyGrown)
        {
            time += Time.deltaTime;
            SetModelToShow();
        }
    }

    public bool IsFullyGrown()
    {
        return fullyGrown;
    }

    public void SetTimer(float timeVal)
    {
        timeToFullGrowth = timeVal;
    }

    public void Harvest()
    {
        if (fullyGrown)
        {
            // give item
        }
        time = 0;
    }

    void SetModelToShow()
    {
        if (time >= timeToFullGrowth)
        {
            fullyGrown = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            return;
        }
        for (int i = 2; i < transform.childCount; i++)
        {
            if (time >= ((timeToFullGrowth / (float)transform.childCount - 1) * ((float)i - 1)) && time <= ((timeToFullGrowth / (float)transform.childCount) * ((float)i + 1)))
            {
                for (int j = 0; j < transform.childCount; j++)
                {
                    transform.GetChild(j).gameObject.SetActive(i - 1 == j);
                }
            }
        }
    }
}
