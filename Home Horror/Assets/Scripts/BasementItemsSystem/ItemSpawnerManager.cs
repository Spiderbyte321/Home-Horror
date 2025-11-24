using UnityEngine;
using System.Collections.Generic;

public class ItemSpawnerManager : MonoBehaviour
{
    public static ItemSpawnerManager Instance;

    [Header("Items to spawn")]
    public GameObject[] itemPrefabs;

    [Header("Daily spawn count")]
    public int baseItemCount = 6;
    private int currentItemCount;

    private List<GameObject> spawnedItems = new();
    private RandomSpawnArea[] areas;

    private bool isSpawning = false; // prevents double calls

    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        // Strong singleton enforcement
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogWarning("[ItemSpawnerManager] Duplicate manager found - destroying extra instance.", this);
            Destroy(gameObject);
            return;
        }

        currentItemCount = baseItemCount;
    }

    private void Start()
    {
        areas = FindObjectsOfType<RandomSpawnArea>();
        if (areas == null || areas.Length == 0)
            Debug.LogError("[ItemSpawnerManager] NO RandomSpawnAreas found in the scene!");
    }

   public void SpawnTodayItems()
{
    if (isSpawning) return;
    isSpawning = true;

    ClearOldItems();

    // Day-based count:
    int itemsToSpawn = Mathf.Max(0, baseItemCount - (gameManager.currentDay - 1));

    int spawned = 0;
    int attempts = 0;
    int maxAttempts = itemsToSpawn * 30;

    while (spawned < itemsToSpawn && attempts < maxAttempts)
    {
        attempts++;

        RandomSpawnArea area = areas[Random.Range(0, areas.Length)];

        if (area.TryGetSpawnPoint(out Vector3 pos))
        {
            GameObject prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
            spawnedItems.Add(obj);

            spawned++;
        }
    }

    // cleanup safeguards
    while (spawnedItems.Count > itemsToSpawn)
    {
        Destroy(spawnedItems[spawnedItems.Count - 1]);
        spawnedItems.RemoveAt(spawnedItems.Count - 1);
    }

    isSpawning = false;
}
    private void EnforceExactCount()
    {
        while (spawnedItems.Count > currentItemCount)
        {
            GameObject extra = spawnedItems[spawnedItems.Count - 1];
            if (extra) Destroy(extra);
            spawnedItems.RemoveAt(spawnedItems.Count - 1);
        }
    }


    private void ClearOldItems()
    {
        for (int i = spawnedItems.Count - 1; i >= 0; i--)
        {
            if (spawnedItems[i] != null)
                Destroy(spawnedItems[i]);
        }
        spawnedItems.Clear();
    }
}
