using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField]
    CropData cropData = new CropData();
    [SerializeField]
    float time;
    [SerializeField]
    bool fullyGrown , canYield , allYieldSlotsFull;

    private void Start()
    {
        SetupData();
    }

    private void Update()
    {
        // do season check
        time += Time.deltaTime;
        if(fullyGrown == false)
        {
            SetModelToShow();
        }
        else if(!allYieldSlotsFull)
        {
            DoYield();
        }
    }

    void SetModelToShow()
    {
        if(time >= cropData.growthTime)
        {
            fullyGrown = true;
            canYield = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            return;
        }
        for (int i = 2; i < transform.childCount + 1; i++)
        {
            if(time >= ((cropData.growthTime / (float)transform.childCount) * ((float)i-1)) && time <= ((cropData.growthTime / (float)transform.childCount) * ((float)i + 1)))
            {
                for (int j = 0; j < transform.childCount; j++)
                {
                    transform.GetChild(j).gameObject.SetActive(i-1 == j);
                }
            }
        }
    }
    void DoYield()
    {
        if (canYield)
        {
            for (int i = 1; i < transform.GetChild(transform.childCount - 1).childCount; i++)
            {
                if(!transform.GetChild(transform.childCount - 1).GetChild(i).GetChild(0).gameObject.activeInHierarchy && canYield)
                {
                    canYield = false;
                    transform.GetChild(transform.childCount - 1).GetChild(i).GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(transform.childCount - 1).GetChild(i).GetChild(0).gameObject.GetComponent<YieldGrowthController>().SetTimer(cropData.yieldGrowthTime);
                    if (i == transform.GetChild(transform.childCount - 1).childCount - 1)
                    {
                        allYieldSlotsFull = true;
                    }
                }
            }
        }
        else if(fullyGrown)
        {
            if(time >= cropData.growthTime + cropData.yieldGrowthTime && (time - cropData.growthTime) % cropData.yieldGrowthTime < 0.1f)
            {
                canYield = true;
            }
        }
    }

    public CropData GetData()
    {
        return cropData;
    }

    void SetupData()
    {
        cropData.id = "crop";
        cropData.growthTime = 30f;
        cropData.activeSeasons = new List<string> { "Summer", "Spring" };
        cropData.yieldItemID = "Crop_Fruit";
        cropData.yieldGrowthTime = 30f;
    }
}