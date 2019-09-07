using UnityEngine;

public abstract class Item : IItem
{
    public abstract string TypeId { get; }

    public string Id { get; set; }

    public int MaxStackSize { get; set; }

    public Sprite ItemIcon { get; set; }

    public GameObject PrefabGO { get; set; }

    public Recipe Recipe { get; set; }

    public abstract void Use();

    public abstract Item New();


    public string Name { get; set; }            //      These values are gonna be changed on get.
                                                //      
    public string Description { get; set; }     //      Price will be recalculated with influence.
                                                //      
    public int Price { get; set; }              //      Name and description will be changed for localisation.
}