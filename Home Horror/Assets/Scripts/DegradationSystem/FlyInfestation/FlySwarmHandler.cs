using UnityEngine;

public class FlySwarmHandler : MonoBehaviour
{
    public ParticleSystem swarmSystem;

    private ParticleSystem.EmissionModule emission;
    private ParticleSystem.ShapeModule shape;
    private ParticleSystem.NoiseModule noise;

    private void Awake()
    {
        var ps = swarmSystem;
        emission = ps.emission;
        shape = ps.shape;
        noise = ps.noise;
    }

    public void SetSwarm(float density, float radius, float chaos)
    {
        if (swarmSystem == null) return; // prevents scene-change errors

        var emission = swarmSystem.emission;
        var shape = swarmSystem.shape;
        var noise = swarmSystem.noise;

        emission.rateOverTime = density;
        shape.radius = radius;
        noise.strength = chaos;
    }

    public void DisableSwarm()
    {
        emission.rateOverTime = 0;
    }
}
