using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> materials = new Dictionary<string, int>();

    public int Money { get; private set; } = 0;

    public void AddMaterial(Material material)
    {
        string type = material.Name.ToLower();

        if (materials.ContainsKey(type))
        {
            materials[type] += material.Amount;
        }
        else
        {
            materials[type] = material.Amount;
        }

        Debug.Log($"Added {material.Amount} of {type}. Total: {materials[type]}");
    }

    public bool HasMaterial(string type, int amount)
    {
        return materials.ContainsKey(type.ToLower()) && materials[type.ToLower()] >= amount;
    }

    public bool UseMaterial(string type, int amount)
    {
        if (!HasMaterial(type, amount))
            return false;

        materials[type.ToLower()] -= amount;
        return true;
    }

    public int GetAmount(string type)
    {
        return materials.TryGetValue(type.ToLower(), out int amount) ? amount : 0;
    }
    
    public Dictionary<string, int> GetAllMaterials()
    {
        return new Dictionary<string, int>(materials); // Return a copy to prevent external modification
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        Debug.Log($"Added R{amount}. Current Money: R{Money}");
    }

    public bool SpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            Debug.Log($"Spent R{amount}. Remaining Money: R{Money}");
            return true;
        }

        Debug.Log("Not enough money.");
        return false;
    }
}
