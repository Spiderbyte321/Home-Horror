using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    public IEnumerator FadeOut(float speed = 1f)
    {
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime * speed;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float speed = 1f)
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime * speed;
            yield return null;
        }
    }
}
