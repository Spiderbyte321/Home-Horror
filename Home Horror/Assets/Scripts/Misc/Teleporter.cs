using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private ScreenFader fader;
    [SerializeField] private GameObject player;

    // These are the spawnpoints *this teleporter* can choose from
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
        if (!other.CompareTag("Player") || isTeleporting)
            return;

        StartCoroutine(TeleportRoutine());
    }

    private IEnumerator TeleportRoutine()
    {
        isTeleporting = true;

        // Prevent double triggering
        triggerCol.enabled = false;

        // Fade out
        yield return fader.FadeOut();

        // Disable controller before moving
        var controller = player.GetComponent<CharacterController>();
        controller.enabled = false;

        // Pick random spawnpoint
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Tell StaircaseManager which staircase to enable
        var owner = StaircaseManager.Instance.FindOwnerOf(randomSpawn);
        if (owner != null)
        {
            StaircaseManager.Instance.ActivateOnly(owner);
        }

        // Move player
        player.transform.position = randomSpawn.position;

        // Re-enable controller
        controller.enabled = true;

        // Basement door spawns items
        if (isBasementDoor)
        {
            ItemSpawnerManager.Instance.SpawnTodayItems();
        }

        // Fade back in
        yield return fader.FadeIn();

        // Small buffer before allowing retrigger
        yield return new WaitForSeconds(0.25f);

        triggerCol.enabled = true;
        isTeleporting = false;
    }
}
