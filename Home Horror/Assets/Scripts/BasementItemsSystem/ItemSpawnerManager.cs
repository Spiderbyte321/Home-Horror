using UnityEngine;
using System.Collections.Generic;

public class ItemSpawnerManager : MonoBehaviour
{
    public static ItemSpawnerManager Instance;
    [SerializeField] private LayerMask groundMask;

    [Header("Items to spawn")]
    public GameObject[] itemPrefabs;

    [Header("Daily spawn count")]
    public int baseItemCount = 6;

    private List<GameObject> spawnedItems = new();
    private RandomSpawnArea[] areas;

    private bool isSpawning = false;

    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogWarning("[ItemSpawnerManager] Duplicate instance found. Destroying.", this);
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        areas = FindObjectsOfType<RandomSpawnArea>();

        if (areas == null || areas.Length == 0)
            Debug.LogError("[ItemSpawnerManager] No RandomSpawnAreas found!");
    }

    public void SpawnTodayItems()
    {
        if (isSpawning) return;
        isSpawning = true;

        ClearOldItems();

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
               // Start slightly above the spawn area but INSIDE the house
Vector3 rayStart = pos + Vector3.up * 0.5f;

if (Physics.Raycast(pos + Vector3.up * 2f, Vector3.down, out RaycastHit hit, 10f, groundMask))
{
    // Reject walls or anything not flat enough to be a floor
    if (hit.normal.y < 0.95f)
        continue;

    pos = hit.point; // Valid floor
}
else
{
    continue;
}



                GameObject prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
                GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

                spawnedItems.Add(obj);
                spawned++;
            }
        }

        // Final sanity correction
        while (spawnedItems.Count > itemsToSpawn)
        {
            Destroy(spawnedItems[^1]);
            spawnedItems.RemoveAt(spawnedItems.Count - 1);
        }

        isSpawning = false;
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
