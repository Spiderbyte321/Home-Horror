using UnityEngine;

public class ElectricialFaultEventBinder : MonoBehaviour
{
    public ProblemStageSO flickeringStage;
    public ProblemStageSO blackoutStage;
    public ProblemStageSO fullBlackoutStage;

    public LightFlicker flickerHandler;
    public RandomBlackout blackoutHandler;
    public FullBlackout fullBlackoutHandler;

    private void OnEnable()
    {
        if (flickeringStage != null && flickerHandler != null)
        {
            flickeringStage.OnEnterStage += flickerHandler.StartFlickering;
            flickeringStage.OnExitStage += flickerHandler.StopFlickering;
        }

        if (blackoutStage != null && blackoutHandler != null)
        {
            blackoutStage.OnEnterStage += blackoutHandler.StartRandomBlackouts;
            blackoutStage.OnExitStage += blackoutHandler.StopRandomBlackouts;
        }

        if (fullBlackoutStage != null && fullBlackoutHandler != null)
        {
            fullBlackoutStage.OnEnterStage += fullBlackoutHandler.TriggerFullBlackout;
            fullBlackoutStage.OnExitStage += fullBlackoutHandler.RestoreLights;
        }
    }

    private void OnDisable()
    {
        if (flickeringStage != null && flickerHandler != null)
        {
            flickeringStage.OnEnterStage -= flickerHandler.StartFlickering;
            flickeringStage.OnExitStage -= flickerHandler.StopFlickering;
        }

        if (blackoutStage != null && blackoutHandler != null)
        {
            blackoutStage.OnEnterStage -= blackoutHandler.StartRandomBlackouts;
            blackoutStage.OnExitStage -= blackoutHandler.StopRandomBlackouts;
        }

        if (fullBlackoutStage != null && fullBlackoutHandler != null)
        {
            fullBlackoutStage.OnEnterStage -= fullBlackoutHandler.TriggerFullBlackout;
            fullBlackoutStage.OnExitStage -= fullBlackoutHandler.RestoreLights;
        }
    }
}
