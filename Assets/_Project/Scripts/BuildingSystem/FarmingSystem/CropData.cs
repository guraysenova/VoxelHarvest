using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CropData
{
    public string id;
    public List<string> activeSeasons;
    public string yieldItemID;
    public float growthTime;
    public float yieldTimeAfterGrowth;
}