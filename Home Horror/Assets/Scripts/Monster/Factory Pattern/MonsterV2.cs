using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class MonsterV2 : AbstractMonster
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private string[] tauntKeys;
    [SerializeField] private AudioSource speaker;
    private bool revealed;
    private void OnBecameVisible()
    {
        if(revealed)
            return;

        revealed = true;
        StartCoroutine(wander());
        StartCoroutine(taunt());
    }

    private IEnumerator wander()
    {
        while (true)
        {
            yield return null;
            Vector3 wanderPoint = Random.insideUnitSphere * 5;
            Vector3 moveVector = new Vector3(wanderPoint.x, agent.transform.position.y, wanderPoint.z);
                        
            if(agent.remainingDistance < 0.01f)
            {
               agent.SetDestination(moveVector);             
            }
        }
    }

    private IEnumerator taunt()
    {
        while (true)
        {
            yield return null;

            speaker.clip = SFXManager.instance.GetAudio(tauntKeys[Random.Range(0, tauntKeys.Length)]);
            speaker.Play();

            yield return new WaitForSeconds(speaker.clip.length+0.1f);
        }
    }
}
