using UnityEngine;
using UnityEngine.UI;

public class SanityOverlayUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image overlayImage;

    [Header("Sanity Settings")]
    [SerializeField] private float maxOpacity = 0.8f; // opacity when sanity = 0

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
        // sanityPercent = 1 when sanity=0, 0 when sanity=100
        float sanityPercent = Mathf.Clamp01(1f - (sanity / 100f));

        float newAlpha = sanityPercent * maxOpacity;

        Color c = overlayImage.color;
        c.a = newAlpha;
        overlayImage.color = c;
    }
}