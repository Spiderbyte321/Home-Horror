using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConcreteMonsterSpawner : AbstractMonsterSpawner
{

    [SerializeField] private GameObject monsterObject;
    public override void SpawnMonster(Transform SpawnOrigin)
    {
        //Test this

        List<RaycastHit> hitData = new List<RaycastHit>();

        Vector3[] directions = { Vector3.back, Vector3.left, Vector3.forward, Vector3.right };

        for (int i = 0; i < 4; i++)
        {
            RaycastHit ray;
            Physics.Raycast(SpawnOrigin.position, directions[i], out ray);
            
            hitData.Add(ray);
        }

        List<RaycastHit> legalData = new List<RaycastHit>();

        foreach (RaycastHit data in hitData)
        {
            if(data.distance>5)
                legalData.Add(data);
        }


        int chosenDirection = Random.Range(0, legalData.Count);

        Vector3 ChosenSpawnPoint =Vector3.Lerp(SpawnOrigin.position, legalData[chosenDirection].point, 0.5f);
        
        Instantiate(monsterObject, ChosenSpawnPoint, quaternion.identity);
    }
}
