using UnityEngine;
using System.Collections;

public class BasementPortalTeleporter : MonoBehaviour
{
    [SerializeField] private ScreenFader fader;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool isBasementDoor = false;

    private bool isTeleporting = false;
    private Collider triggerCol;

    private void Awake()
    {
        triggerCol = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isTeleporting) return;

        StartCoroutine(TeleportRoutine());
    }

    private IEnumerator TeleportRoutine()
    {
        isTeleporting = true;
        triggerCol.enabled = false;

        yield return fader.FadeOut();

        var controller = player.GetComponent<CharacterController>();
        controller.enabled = false;

        // Pick random point to teleport to
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // enable ALL staircases again
StaircaseManager.Instance.ActivateAll();

// Move player AFTER staircases are reactivated
player.transform.position = randomSpawn.position;

// Re-enable controller
controller.enabled = true;

// Fade in
yield return fader.FadeIn();

// Delay
yield return new WaitForSeconds(0.25f);

//  Re-enable trigger AFTER everything is active again
triggerCol.enabled = true;
isTeleporting = false;

    }
}
