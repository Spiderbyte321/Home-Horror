using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private ScreenFader fader;

    private bool isTeleporting = false;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject spawnPoint;

    private void OnTriggerEnter(Collider other)
{
    Debug.Log("Triggered by: " + other.name);
    if (isTeleporting || !other.CompareTag("Player")) return;

    Debug.Log("Player detected! Starting teleport.");
    isTeleporting = true;
    StartCoroutine(TeleportRoutine());
}

private IEnumerator TeleportRoutine()
{
    yield return fader.FadeOut();
    Debug.Log("FadeOut finished!");

   var controller = player.GetComponent<CharacterController>();
controller.enabled = false;
player.transform.position = spawnPoint.transform.position;
controller.enabled = true;
    Debug.Log("Teleported to spawn point: " + spawnPoint.transform.position);
    Debug.Log("Player new position: " + player.transform.position);

    yield return fader.FadeIn();
}

}
