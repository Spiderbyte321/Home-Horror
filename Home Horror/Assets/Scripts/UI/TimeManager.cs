using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    public int startHour = 18; // Starting hour: 18:00pm
    public int endHour = 23;   // End of day at 23:00pm
    public float secondsPerHour = 10f; // 1 in-game hour = 10 real seconds

    [Header("UI References")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;

    private int currentHour;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in scene!");
            enabled = false;
            return;
        }

        StartNewDay();
    }

    public void StartNewDay()
    {
        currentHour = startHour;
        UpdateUI();

        StartCoroutine(TimeTick());
    }

    private IEnumerator TimeTick()
    {
        while (currentHour < endHour)
        {
            yield return new WaitForSeconds(secondsPerHour);

            currentHour++;
            UpdateUI();
        }

        // When the loop ends, the time is 23:00 - trigger end of day
        gameManager.EndDay();

        // Wait briefly before starting the next day to prevent race conditions
        yield return new WaitForSeconds(1f);
        StartNewDay();
    }

    private void UpdateUI()
    {
        if (dayText != null)
            dayText.text = "Day " + gameManager.currentDay;

        if (timeText != null)
            timeText.text = currentHour.ToString("00") + ":00pm";
    }
}
