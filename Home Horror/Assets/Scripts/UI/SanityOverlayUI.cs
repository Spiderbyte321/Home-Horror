using UnityEngine;
using UnityEngine.UI;

public class SanityOverlayUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image overlayImage;

    [Header("Sanity Settings")]
    [SerializeField] private float maxOpacity = 0.8f; // opacity when sanity = 0
    
    private float currentAlpha = 0f;
    [SerializeField] private float fadeSpeed = 2f;

    private PlayerCharacter player;

    private void OnEnable()
    {
        PlayerCharacter.OnSanityUpdateAction += UpdateSanityOverlay;
    }

    private void OnDisable()
    {
        PlayerCharacter.OnSanityUpdateAction -= UpdateSanityOverlay;
    }

    private void Start()
    {
        player = FindFirstObjectByType<PlayerCharacter>();
        UpdateSanityOverlay(player.CurrentSanity);
    }

    private void UpdateSanityOverlay(int sanity)
    {
        float sanityPercent = Mathf.Clamp01(1f - sanity / 100f);
        float targetAlpha = sanityPercent * maxOpacity;

        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);

        Color c = overlayImage.color;
        c.a = currentAlpha;
        overlayImage.color = c;
    }
}