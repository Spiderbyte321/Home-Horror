using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DegradationController : Interactable
{
    [SerializeField] private string[] stageAudioKeys;
    [SerializeField] private SFXController sfxController;

    public delegate void OnProblemRepaired(int SanityHealAmount);

    public static event OnProblemRepaired OnRepairedAction;
    
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
                    sfxController.stopSFX();
                    return true;
                }
                break;

            case RepairMethod.Material:
                if (inventory.HasMaterial(currentStage.materialType, currentStage.materialAmount))
                {
                    inventory.UseMaterial(currentStage.materialType, currentStage.materialAmount);
                    Interact();
                    sfxController.stopSFX();
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
        OnRepairedAction?.Invoke(30);
        
    }

    private void SetStage(int index)
    {
        if (index >= 0 && index < stageData.Count)
        {
            if (currentStage != null)
            {
                currentStage.Exit(); // Call OnExitStage
            }

            if(index < stageAudioKeys.Length)
            {
                sfxController.playLoopedSFX(stageAudioKeys[index]);
            }
            
            currentStage = stageData[index];
            currentStage.Enter(); // Call OnEnterStage
        }
    }
}
