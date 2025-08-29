using System;
using System.Transactions;
using TMPro;
using UnityEngine;

public class MaterialController : Interactable //component on the object in the scene
{
  [SerializeField] private int MaterialAmount;
  [SerializeField] private string MaterialType;
  [SerializeField] private TextMeshPro Prompt;
  private Material material;

  public Material Material => material;
  
  public delegate void MaterialInteractAction(Material AMaterial);

  public static event MaterialInteractAction MaterialInteraction;
  


  private void Start()
  {
    material = new Material(MaterialType, MaterialAmount);
  }
  
  private void OnTriggerEnter(Collider other)
  {
    if (!other.CompareTag("Player"))
    {
      return;
    }
    Prompt.gameObject.SetActive(true);
  }

  private void OnTriggerExit(Collider other)
  {
    if (!other.CompareTag("Player"))
    {
      return;
    }
    Prompt.gameObject.SetActive(false);
  }

  public override void Interact()//assumes the playercontroller will get it through interaction to broadcast
  {
    MaterialInteraction?.Invoke(Material);
  }
}
