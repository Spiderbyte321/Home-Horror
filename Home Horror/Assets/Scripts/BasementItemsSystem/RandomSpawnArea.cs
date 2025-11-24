using UnityEngine;

public class RandomSpawnArea : MonoBehaviour
{
    private BoxCollider area;

    [Header("Anti-Clipping")]
    public float checkRadius = 0.25f;
    public LayerMask collisionMask;

    private void Awake()
    {
        area = GetComponent<BoxCollider>();
    }

    public bool TryGetSpawnPoint(out Vector3 pos)
{
    for (int i = 0; i < 50; i++)
    {
        Vector3 randomXZ = GetRandomPointXZ();

        // Start raycast slightly above the expected floor height
        Vector3 rayStart = randomXZ + new Vector3(0f, 1f, 0f);

        // Short raycast so it NEVER touches roof or high walls
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 2f))
        {
            // Reject walls / slopes
            if (hit.normal.y < 0.95f)
                continue;

            // Reject anything too high (interior walls top)
            if (hit.point.y > 0.5f) // adjust based on your basement
                continue;

            // Reject too low (holes, pits)
            if (hit.point.y < -0.5f)
                continue;

            // Anti-clipping
            if (!Physics.CheckSphere(hit.point, checkRadius, collisionMask))
            {
                pos = hit.point;
                return true;
            }
        }
    }

    pos = Vector3.zero;
    return false;
}


    // Random XZ inside collider footprint
    private Vector3 GetRandomPointXZ()
    {
        Vector3 c = area.bounds.center;
        Vector3 s = area.bounds.size;

        float x = Random.Range(c.x - s.x * 0.5f, c.x + s.x * 0.5f);
        float z = Random.Range(c.z - s.z * 0.5f, c.z + s.z * 0.5f);

        return new Vector3(x, 0f, z);
    }
}
