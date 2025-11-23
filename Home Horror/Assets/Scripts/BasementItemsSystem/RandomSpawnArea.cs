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
        // Try many times to find a good spot
        for (int i = 0; i < 50; i++)
        {
            Vector3 randomXZ = GetRandomPointXZ();

            // Start raycast from FAR ABOVE to ensure ground hit
            Vector3 rayStart = new Vector3(randomXZ.x, area.bounds.max.y + 10f, randomXZ.z);

            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 100f))
            {
                Vector3 ground = hit.point;

                // small scatter to avoid identical positions
                ground += new Vector3(
                    Random.Range(-0.2f, 0.2f),
                    0f,
                    Random.Range(-0.2f, 0.2f)
                );

                // ensure not clipping into walls/objects
                if (!Physics.CheckSphere(ground, checkRadius, collisionMask))
                {
                    pos = ground;
                    return true;
                }
            }
        }

        // failed
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
