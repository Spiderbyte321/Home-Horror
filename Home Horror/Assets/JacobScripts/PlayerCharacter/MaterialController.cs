using System;
using UnityEngine;

public class MaterialController : MonoBehaviour//component on the object in the scene
{
  [SerializeField] private int MaterialAmount;
  [SerializeField] private string MaterialType;
  private Material material;

  public Material Material => material;
  


  private void Start()
  {
    material = new Material(MaterialType, MaterialAmount);
  }
}
