using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    public int currentDay = 1;

    [Header("Degradation Systems")]
    public List<DegradationController> degradationSystems;

    [Header("Random Problem Timing")]
    public float minProblemDelay = 5f;
    public float maxProblemDelay = 20f;

    private Coroutine dailyProblemRoutine;
    
    [Header("Economy")]
    public int dailyMoneyReward = 100;
    
    [Header("Win Condition")]
    public int moneyGoal = 1000;

    private bool gameEnded = false;
    
    private GameUI gameUI;

    [Header("Player Stats")]
    private PlayerCharacter playerCharacter;

    [Header("Monster Stats")] [SerializeField]
    private float MonsterSpawnThreshold =0.5f;


    public delegate void NightBeganAction(int playerStatusAmount);

    public static event NightBeganAction OnNightBegan;

    public delegate void SystemleftAction(int playerSanityDamage);

    public static event SystemleftAction OnSystemLeft;

    [SerializeField] private AbstractMonsterSpawner monsterV1Spawner;


    private void OnEnable()
    {
        PlayerCharacter.OnSanityUpdateAction += HandlePlayerSanityAction;
    }

    private void OnDisable()
    {
        PlayerCharacter.OnSanityUpdateAction -= HandlePlayerSanityAction;
    }

    private void Start()
    {
        gameUI = FindFirstObjectByType<GameUI>();
        playerCharacter = FindFirstObjectByType<PlayerCharacter>();
        StartNewDay();
    }

    public void EndDay()
    {
        Debug.Log($"Ending Day {currentDay}");
        
        // Degrade any problems that weren’t fixed
        foreach (var system in degradationSystems)
        {
            system.HandleDayEnd(system.IsProblemActive()); // Only degrade active problems

            if (system.IsProblemActive())
            {
                OnSystemLeft?.Invoke(5);
            }
        }

        if (dailyProblemRoutine != null)
            StopCoroutine(dailyProblemRoutine);
        
        currentDay++;

        StartNewDay();
    }

    private void StartNewDay()
    {
        if (gameEnded) return;
        
        // Give player money
        PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddMoney(dailyMoneyReward);
            
            if (inventory.Money >= moneyGoal)
            {
                EndGame();
                return;
            }
        }
        
        OnNightBegan?.Invoke(playerCharacter.CurrentSanity);
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
    
    private void EndGame()
    {
        gameEnded = true;
        Debug.Log("You reached your savings goal! The game has ended.");
        
        gameUI.ShowWinScreen();
    }


    private void HandlePlayerSanityAction(int playerSanity)
    {
        float RanNumber = Random.Range(0, 2 * MonsterSpawnThreshold);

        if (RanNumber > MonsterSpawnThreshold)
        {
            Debug.Log("spawning Monster");
           monsterV1Spawner.SpawnMonster(playerCharacter.transform);
        }
    }
}
