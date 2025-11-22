using UnityEngine;

public class FlySwarmEventBinder : MonoBehaviour
{
    public FlySwarmHandler swarmHandler;
    public FlySwarmStageSO[] swarmStages;

    private void OnEnable()
    {
        foreach (var stage in swarmStages)
        {
            stage.OnEnterStage += () => swarmHandler.SetSwarm(stage.swarmDensity, stage.swarmRadius, stage.swarmChaos);
            stage.OnExitStage += swarmHandler.DisableSwarm;
        }
    }

    private void OnDisable()
    {
        foreach (var stage in swarmStages)
        {
            stage.OnEnterStage -= () => swarmHandler.SetSwarm(stage.swarmDensity, stage.swarmRadius, stage.swarmChaos);
            stage.OnExitStage -= swarmHandler.DisableSwarm;
        }
    }
}