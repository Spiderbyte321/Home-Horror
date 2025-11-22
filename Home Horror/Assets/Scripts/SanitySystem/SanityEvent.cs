using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityEvent : MonoBehaviour
{
   
   //triggers event used to decrease player sanity but also to trigger other effects such as loud noise or other screen effects

   [SerializeField] private int SanityDamage;
   [SerializeField] private MeshRenderer renderer;
   [SerializeField] private Collider triggerBox;
   
   public delegate void SanityEventAction(int SanityDamage);

   public static event SanityEventAction OnSanityEvent;

   private void Start()
   {
      renderer.enabled = false;
      triggerBox.enabled = true;
   }


   private void TriggerEvent()
   {
       Debug.Log($"Broadcasting with damage of: {SanityDamage}");
      OnSanityEvent?.Invoke(SanityDamage);
      renderer.enabled = true;
      triggerBox.enabled = false;

      StartCoroutine(DeactivateJumpScare());
   }

   private void OnTriggerEnter(Collider other)
   {
     Debug.Log("Triggered");
      if(!other.CompareTag("Player")) 
         return;
      
      TriggerEvent();
   }


   private IEnumerator DeactivateJumpScare()
   {
      yield return new WaitForSeconds(2);
      
      Destroy(gameObject);
   }
}
