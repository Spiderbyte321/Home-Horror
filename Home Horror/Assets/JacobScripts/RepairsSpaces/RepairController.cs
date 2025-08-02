using System;
using UnityEngine;

public class RepairController : MonoBehaviour
{

    [SerializeField] private MeshFilter Filter;
    [SerializeField]private Mesh[] Meshes;
    private int DollarCost = 0;
    private int MaterialCost = 0;
    private ParentState CurrentState;


    private void OnEnable()
    {
        GameController.OnUpdateRepairables += ProgressStates;
    }

    private void OnDisable()
    {
        GameController.OnUpdateRepairables -= ProgressStates;
    }

    

    private void ProgressStates()
    {
        switch(CurrentState)
        {
            case RepairedState:
                CurrentState = new DentedState(Meshes[1]);
                Filter.mesh = CurrentState.Mesh;
                break;
            case DentedState:
                CurrentState = new BrokenState(Meshes[2]);
                Filter.mesh = CurrentState.Mesh;
                break;
            case BrokenState:
                break;
            default:
                Repair();
                break;
        }
    }

    private void Repair()
    {
        CurrentState = new RepairedState(Meshes[0]);
        Filter.mesh = CurrentState.Mesh;
    }


}
