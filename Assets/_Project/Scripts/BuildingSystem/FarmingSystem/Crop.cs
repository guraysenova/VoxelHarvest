using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField]
    CropData cropData = new CropData();

    private void Start()
    {
        
    }

    void SetUpData()
    {
        cropData.id = "crop";
        cropData.growthTime = 30f;
        cropData.activeSeasons = new List<string> { "Summer", "Spring" };
        cropData.yieldItemID = "Crop_Fruit";
    }
}