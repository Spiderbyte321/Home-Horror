using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private ScreenFader fader;

    private bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isTeleporting || !other.CompareTag("Player")) return;
        isTeleporting = true;

        StartCoroutine(TeleportRoutine());
    }
private IEnumerator TeleportRoutine()
{
    yield return fader.FadeOut();

    // Load the new scene
    AsyncOperation load = SceneManager.LoadSceneAsync("TeleportTest", LoadSceneMode.Single);
    while (!load.isDone)
        yield return null;

    // Make sure the player exists in DontDestroyOnLoad
    GameObject player = GameObject.FindWithTag("Player");

    // Find spawn point in the new scene
    GameObject spawnObject = GameObject.Find("SpawnPoint");

    if (spawnObject != null)
    {
        player.transform.position = spawnObject.transform.position;
        player.transform.rotation = spawnObject.transform.rotation;
    }
    else
    {
        Debug.LogWarning("SpawnPoint not found in scene!");
    }

    yield return fader.FadeIn();
}

}
