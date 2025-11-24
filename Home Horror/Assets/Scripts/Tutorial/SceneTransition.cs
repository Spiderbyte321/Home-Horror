using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private ScreenFader fader;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Transitioner());
    }

    private IEnumerator Transitioner()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(fader.FadeOut());
        SceneManager.LoadScene("GameplayScene");
    }
}
