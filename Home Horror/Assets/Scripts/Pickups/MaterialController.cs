using TMPro;
using UnityEngine;

public class MaterialController : Interactable
{
  [Header("Material Properties")]
  [SerializeField] private string materialType;
  [SerializeField] private int materialAmount = 1;
  
  public string MaterialType => materialType;
  public int MaterialAmount => materialAmount;
  
  public override void Interact()
  {
    PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();

    if (inventory != null)
    {
      Material material = new Material(materialType, materialAmount);
      inventory.AddMaterial(material);

      Destroy(gameObject); // Remove the pickup from the world
    }
    else
    {
      Debug.LogWarning("PlayerInventory not found!");
    }
  }
}
