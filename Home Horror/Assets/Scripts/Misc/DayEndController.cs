using System;
using UnityEngine;

public class DayEndController : Interactable
{
    [SerializeField] private GameObject prompt;

    public delegate void BedIntereactAction();

    public static event BedIntereactAction OnBedInteracted;


    public override void Interact()
    {
        OnBedInteracted?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        prompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        prompt.SetActive(false);
    }
}
