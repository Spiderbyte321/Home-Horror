using UnityEngine;

public class RandomSpawnArea : MonoBehaviour
{
    [Header("Item Prefabs")]
    public GameObject[] items;

    [Header("How many items to spawn")]
    public int itemCount = 3;

    [Header("Overlapping Prevention")]
    public float checkRadius = 0.5f;
    public int maxAttemptsPerItem = 15;

    [Header("Layer Masks")]
    public LayerMask groundLayers = ~0; 
    public LayerMask obstacleLayers = 0; 

    [Header("Raycast Settings")]
    public float rayHeightOffset = 1f; 
    public float rayDistance = 20f;

    [Header("Spawn Offset")]
    public float spawnHeightOffset = 0f; 
    private BoxCollider area;

    void Awake()
    {
        area = GetComponent<BoxCollider>();
    }

    void Start()
    {
        if (area == null)
        {
            Debug.LogWarning("RandomSpawnArea: No BoxCollider found on the GameObject.");
            enabled = false;
            return;
        }

        if (items == null || items.Length == 0)
        {
            Debug.LogWarning("RandomSpawnArea: No item prefabs assigned.");
            return;
        }

        SpawnItems();
    }

    void SpawnItems()
    {   
        for (int i = 0; i < itemCount; i++)
        {
            Vector3 spawnPos;

            if (FindValidSpawnPoint(out spawnPos))
            {
                GameObject prefab = items[Random.Range(0, items.Length)];
                Vector3 finalPos = spawnPos + Vector3.up * spawnHeightOffset;
                Instantiate(prefab, finalPos, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No valid spawn spot");
            }
        }
    }

    bool FindValidSpawnPoint(out Vector3 pos)
    {
        for (int attempt = 0; attempt < maxAttemptsPerItem; attempt++)
        {
            Vector3 randomXZ = GetRandomPointInAreaXZ();

            Vector3 rayOrigin = new Vector3(randomXZ.x, area.bounds.max.y + rayHeightOffset, randomXZ.z);

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, area.bounds.size.y + rayDistance, groundLayers, QueryTriggerInteraction.Ignore))
            {
                Vector3 groundPos = hit.point;

                Collider[] hits = Physics.OverlapSphere(groundPos, checkRadius, obstacleLayers, QueryTriggerInteraction.Ignore);

                if (hits.Length == 0)
                {
                    pos = groundPos;
                    return true;
                }
            }
        }

        pos = Vector3.zero;
        return false;
    }

    

    Vector3 GetRandomPointInArea()
    {
        Vector3 center = area.bounds.center;
        Vector3 size = area.bounds.size;
        float x = Random.Range(center.x - size.x * 0.5f, center.x + size.x * 0.5f);
        float z = Random.Range(center.z - size.z * 0.5f, center.z + size.z * 0.5f);

        return new Vector3(x, center.y, z);
    }

    Vector3 GetRandomPointInAreaXZ()
    {
        Vector3 center = area.bounds.center;
        Vector3 size = area.bounds.size;

        float x = Random.Range(center.x - size.x * 0.5f, center.x + size.x * 0.5f);
        float z = Random.Range(center.z - size.z * 0.5f, center.z + size.z * 0.5f);

        return new Vector3(x, center.y, z);
    }
}
