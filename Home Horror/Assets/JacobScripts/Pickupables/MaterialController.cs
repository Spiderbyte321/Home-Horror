using System;
using System.Transactions;
using JacobScripts.Pickupables;
using TMPro;
using UnityEngine;

public class MaterialController : MonoBehaviour,Interactable//component on the object in the scene
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

  public void Interact()//assumes the playercontroller will get it through interaction to broadcast
  {
    MaterialInteraction?.Invoke(Material);
  }
  
}
