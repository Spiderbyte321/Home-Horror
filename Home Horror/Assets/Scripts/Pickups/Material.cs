using UnityEngine;

// This class is used to store what type of material the player gets and how much on an object
public class Material
{
    public string Name { get; private set; }
    public int Amount { get; private set; }

    public Material(string name, int amount)
    {
        Name = name.ToLower();
        Amount = amount;
    }
}
