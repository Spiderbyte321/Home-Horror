using UnityEngine;

[CreateAssetMenu(fileName = "FlySwarmStage", menuName = "Degradation/Fly Swarm Stage")]
public class FlySwarmStageSO : ProblemStageSO
{
    public float swarmDensity = 20f;
    public float swarmRadius = 0.3f;
    public float swarmChaos = 0.5f;
}