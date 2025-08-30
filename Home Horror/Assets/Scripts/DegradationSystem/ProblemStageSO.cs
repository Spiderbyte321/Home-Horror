using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProblemStage", menuName = "Degradation/Problem Stage")]
public class ProblemStageSO : ScriptableObject
{
    public int moneyCost;
    public int materialCost;

    public event Action OnEnterStage;
    public event Action OnExitStage;

    public void Enter()
    {
        OnEnterStage?.Invoke();
    }

    public void Exit()
    {
        OnExitStage?.Invoke();
    }
}
