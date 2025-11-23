using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private Image healthFill;
    [SerializeField] private float criticalThreshold = 0.3f; 
    [SerializeField] private float pulseSpeed = 4f;
    [SerializeField] private float pulseScale = 1.15f;

    private Vector3 originalScale;
    private bool isPulsating = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        UpdateBar();

        float healthPercent = (float)player.CurrentHealth / 100f;

        if (healthPercent <= criticalThreshold)
        {
            if (!isPulsating)
            {
                isPulsating = true;
                StartCoroutine(Pulsate());
            }
        }
        else
        {
            isPulsating = false;
            StopAllCoroutines();
            transform.localScale = originalScale;
        }
    }

    private void UpdateBar()
    {
        float t = (float)player.CurrentHealth / 100f;
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, t, Time.deltaTime * 10f);
    }

    private System.Collections.IEnumerator Pulsate()
    {
        while (isPulsating)
        {
            // scale up
            yield return ScaleTo(pulseScale);

            // scale down
            yield return ScaleTo(1f);
        }
    }

    private System.Collections.IEnumerator ScaleTo(float targetScale)
    {
        Vector3 target = originalScale * targetScale;

        while (Vector3.Distance(transform.localScale, target) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale, 
                target, 
                Time.deltaTime * pulseSpeed
            );
            yield return null;
        }
    }
}