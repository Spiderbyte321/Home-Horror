using System;
using System.Collections.Generic;
using UnityEngine;

public class DegradationController : MonoBehaviour
{
    public List<ProblemStageSO> stageData;

    private int currentStageIndex = 0;
    private bool repairedToday = false;
    
    private ProblemStageSO currentStage;
    
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

    /* public void RepairSystem(PlayerInventory inventory)
    {
        if (currentStage == null) return;

        if (inventory.Money >= currentStage.moneyCost || inventory.Materials >= currentStage.materialCost)
        {
            inventory.Money -= currentStage.moneyCost;
            inventory.Materials -= currentStage.materialCost;
            
            currentStage.Exit();
            
            currentStage == null;
            repairedToday = true;
        }
        else
        {
            Debug.Log("Not enough money");
        }
    } */

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
