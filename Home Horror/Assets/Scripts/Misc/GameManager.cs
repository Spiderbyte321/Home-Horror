using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int currentDay = 1;

    [Header("Degradation Systems")]
    public List<DegradationController> degradationSystems;

    [Header("Random Problem Timing")]
    public float minProblemDelay = 5f;
    public float maxProblemDelay = 20f;

    private Coroutine dailyProblemRoutine;

    private void Start()
    {
        StartNewDay();
    }

    public void EndDay()
    {
        Debug.Log($"Ending Day {currentDay}");

        if (dailyProblemRoutine != null)
            StopCoroutine(dailyProblemRoutine);

        // Degrade any problems that weren’t fixed
        foreach (var system in degradationSystems)
        {
            system.HandleDayEnd(system.IsProblemActive()); // Only degrade active problems
        }
        
        currentDay++;

        StartNewDay();
    }

    private void StartNewDay()
    {
        dailyProblemRoutine = StartCoroutine(RandomProblemRoutine());
    }

    private IEnumerator RandomProblemRoutine()
    {
        int problemsToStart = Mathf.Min(currentDay, degradationSystems.Count); // Increase with day

        List<DegradationController> available = new List<DegradationController>(degradationSystems);

        for (int i = 0; i < problemsToStart && available.Count > 0; i++)
        {
            float delay = Random.Range(minProblemDelay, maxProblemDelay);
            yield return new WaitForSeconds(delay);

            int index = Random.Range(0, available.Count);
            var system = available[index];
            available.RemoveAt(index);

            system.StartProblem();
        }
    }
}
