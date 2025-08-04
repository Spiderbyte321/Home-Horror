using UnityEngine;

public class Material// This class is used to store what type of material the player gets and how much
{
    private string MaterialName;
    
    private int MaterialAmount;

    public string Name => MaterialName;

    public int Amount => MaterialAmount;


    public Material(string AMaterialName, int AMaterialAmount)
    {
        MaterialName = AMaterialName;
        MaterialAmount = AMaterialAmount;
    }
}
