using System;
using TMPro;
using UnityEngine;

public class RepairController : Interactable//component that the state machine lives on
{

    [SerializeField] private MeshFilter Filter;
    [SerializeField]private Mesh[] Meshes;
    [SerializeField] private TextMeshPro Prompt;
    private int dollarCost = 0;
    private int materialCost = 0;
     private ParentState CurrentState;
    
    public int DollarCost => dollarCost;
    public int MaterialCost => materialCost;
    
    private void OnEnable()//Just for showing it works not using events in final game
    {
        GameController.OnUpdateRepairables += ProgressStates;
    }

    private void OnDisable()
    {
        GameController.OnUpdateRepairables -= ProgressStates;
    }

    private void ProgressStates()// want to rework to follow the open closed principal
    {// raaaaaaaghhhh!
        switch(CurrentState)
        {
            case RepairedState:
                CurrentState = new DentedState(Meshes[1]);
                Filter.mesh = CurrentState.Mesh;
                dollarCost = CurrentState.MoneyCost;
                materialCost = CurrentState.MaterialCost;
                break;
            case DentedState:
                CurrentState = new BrokenState(Meshes[2]);
                Filter.mesh = CurrentState.Mesh;
                dollarCost = CurrentState.MoneyCost;
                materialCost = CurrentState.MaterialCost;
                break;
            case BrokenState:
                dollarCost = CurrentState.MoneyCost;
                materialCost = CurrentState.MaterialCost;
                break;
            default:
                Repair();
                break;
        }
    }

    private void Repair()//Created assuming that validation happens in the player controller
    {
        CurrentState = new RepairedState(Meshes[0]);
        Filter.mesh = CurrentState.Mesh;
        dollarCost = CurrentState.MoneyCost;
        materialCost = CurrentState.MaterialCost;
    }

    private void OnTriggerEnter(Collider other)
    {
        Prompt.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Prompt.gameObject.SetActive(false);
    }

    public override void Interact()
    {
        Repair();
    }
}
