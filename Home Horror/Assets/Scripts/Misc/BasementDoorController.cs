using System;
using System.Collections;
using UnityEngine;

public class BasementDoorController : DoorController
{
   [SerializeField] private GameObject portal;

   private bool portalEnabled;
   protected override void toggleDoorState()
   {
      base.toggleDoorState();

      portalEnabled = !portalEnabled;
      portal.SetActive(portalEnabled);
   }


   protected override void OnTriggerExit(Collider other)
   {
      base.OnTriggerExit(other);

      if(portalEnabled)
         toggleDoorState();
   }
}

