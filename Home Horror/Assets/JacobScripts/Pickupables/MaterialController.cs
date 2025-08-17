using System;
using JacobScripts.Pickupables;
using UnityEngine;

public class MaterialController : MonoBehaviour,Interactable//component on the object in the scene
{
  [SerializeField] private int MaterialAmount;
  [SerializeField] private string MaterialType;
  private Material material;

  public Material Material => material;
  
  public delegate void MaterialInteractAction(Material AMaterial);

  public static event MaterialInteractAction MaterialInteraction;
  


  private void Start()
  {
    material = new Material(MaterialType, MaterialAmount);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.E))
    {
      Interact();
    }
  }

  public void Interact()//assumes the playercontroller will get it through interaction to broadcast
  {
    MaterialInteraction?.Invoke(Material);
  }
  
}
