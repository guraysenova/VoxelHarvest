using System.Collections.Generic;

[System.Serializable]
public class Recipe
{
    public List<Ingredient> ingredients;

    public string requiredMachineID;

    public float timeToCraft;
}