using UnityEngine;

public class ConcreteUpgradedMonsterSpawner : AbstractMonsterSpawner
{
    [SerializeField] private GameObject[] monsterSpawns;
    public override void SpawnMonster(Transform SpawnOrigin)
    {
        foreach (GameObject spawn in monsterSpawns)
        {
            float chosenSpawnPoint = Random.Range(0f, 1f);
            
            
            if(chosenSpawnPoint<0.8f)
                continue; 
            
            if(spawn.activeSelf)
                continue;
            
           
            spawn.SetActive(true);
            return;
        }
    }
}
