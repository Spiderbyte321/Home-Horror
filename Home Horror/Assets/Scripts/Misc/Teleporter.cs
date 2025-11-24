using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
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

        // Disable trigger so it can't fire twice
        triggerCol.enabled = false;

        yield return fader.FadeOut();

        var controller = player.GetComponent<CharacterController>();
        controller.enabled = false;

        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        player.transform.position = randomSpawn.position;

        controller.enabled = true;

        // Only spawn items when this teleporter is the basement door
        if (isBasementDoor)
        {
            ItemSpawnerManager.Instance.SpawnTodayItems();
        }

        yield return fader.FadeIn();

        // Small delay before enabling trigger again
        yield return new WaitForSeconds(0.25f);

        triggerCol.enabled = true;
        isTeleporting = false;
    }
}
