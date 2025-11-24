using UnityEngine;

public class StaircaseManager : MonoBehaviour
{
    public static StaircaseManager Instance;

    [SerializeField] private StaircaseGroup[] staircases;

    private void Awake()
    {
        Instance = this;
    }

    // Enable ONLY the staircase the player teleported to
    public void ActivateOnly(StaircaseGroup target)
    {
        foreach (var s in staircases)
            s.gameObject.SetActive(s == target);
    }

    // Enable ALL staircasess
    public void ActivateAll()
    {
        foreach (var s in staircases)
            s.gameObject.SetActive(true);
    }

    // Find which staircase owns the spawnpoint we teleported to
    public StaircaseGroup FindOwnerOf(Transform spawn)
    {
        foreach (var s in staircases)
        {
            if (s.spawnPoint == spawn)
                return s;
        }

        return null;
    }
}
