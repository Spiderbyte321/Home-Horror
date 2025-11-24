using System;
using UnityEngine;

public class DayEndController : Interactable
{
    [SerializeField] private GameObject prompt;

    public delegate void BedIntereactAction();

    public static event BedIntereactAction OnBedInteracted;


    public override void Interact()
    {
        Debug.Log("Interacted with bed");
        OnBedInteracted?.Invoke();
    }

    private void Awake()
    {
        prompt.SetActive(false);
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
