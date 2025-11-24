using UnityEngine;

public class TutorialBasementDoor : Teleporter
{
    public delegate void OnTriggeredTutorialBasementDoor();

    public static event OnTriggeredTutorialBasementDoor TutorialBasementDoorAction;

    protected override void PlayerTeleported()
    {
        TutorialBasementDoorAction?.Invoke();
    }
}
