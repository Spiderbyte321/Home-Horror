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
      //spawn materials
   }
}
