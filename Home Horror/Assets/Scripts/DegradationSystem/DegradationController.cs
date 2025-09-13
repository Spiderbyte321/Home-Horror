using System;
using System.Collections.Generic;
using UnityEngine;

public class DegradationController : Interactable
{
    public List<ProblemStageSO> stageData;

    private int currentStageIndex = 0;
    private bool repairedToday = false;
    
    private ProblemStageSO currentStage;
    
    public ProblemStageSO CurrentStage => currentStage;
    
    public enum RepairMethod { Money, Material }
    
    private GameUI gameUI;

    public void Start()
    {
        gameUI = FindFirstObjectByType<GameUI>();
    }
    
    public void StartProblem()
    {
        if (currentStageIndex == 0 && currentStage == null)
        {
            SetStage(currentStageIndex);
        }
    }

    public bool IsProblemActive()
    {
        return currentStage != null;
    }

    public void HandleDayEnd(bool shouldDegrade)
    {
        if (!repairedToday && shouldDegrade && currentStageIndex < stageData.Count - 1)
        {
            currentStageIndex++;
            SetStage(currentStageIndex);
        }
        
        repairedToday = false;
    }

    public bool TryRepairSystem(PlayerInventory inventory, RepairMethod method)
    {
        if (currentStage == null)
            return false;

        switch (method)
        {
            case RepairMethod.Money:
                if (inventory.Money >= currentStage.moneyCost)
                {
                    inventory.SpendMoney(currentStage.moneyCost);
                    Interact();
                    return true;
                }
                break;

            case RepairMethod.Material:
                if (inventory.HasMaterial(currentStage.materialType, currentStage.materialAmount))
                {
                    inventory.UseMaterial(currentStage.materialType, currentStage.materialAmount);
                    Interact();
                    return true;
                }
                break;
        }

        Debug.Log("Not enough resources to repair with selected method.");
        return false;
    }

    public override void Interact()
    {
        currentStage.Exit();
        currentStage = null;
        currentStageIndex = 0;
        gameUI.HideRepairInfoPopup();
        repairedToday = true;
    }

    private void SetStage(int index)
    {
        if (index >= 0 && index < stageData.Count)
        {
            if (currentStage != null)
            {
                currentStage.Exit(); // Call OnExitStage
            }

            currentStage = stageData[index];
            currentStage.Enter(); // Call OnEnterStage
        }
    }
}
