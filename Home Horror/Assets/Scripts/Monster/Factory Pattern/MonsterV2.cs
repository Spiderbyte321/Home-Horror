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
    [SerializeField] private float monsterHealThreshold = 60f;
    private bool revealed;

    private void OnEnable()
    {
        PlayerCharacter.OnSanityUpdateAction += checkSanity;
    }

    private void OnDisable()
    {
        PlayerCharacter.OnSanityUpdateAction -= checkSanity;
    }

    private void checkSanity(int playerSanity)
    {
        if (playerSanity < monsterHealThreshold)
            return;
        
        StopCoroutine(wander());
        StopCoroutine(taunt());
        gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        if(revealed)
            return;

        revealed = true;
        StartCoroutine(wander());
        //StartCoroutine(taunt());
    }

    private IEnumerator wander()
    {
        while (true)
        {
            yield return null;
            Vector3 wanderPoint = Random.insideUnitSphere * 5;
            Vector3 moveVector = new Vector3(wanderPoint.x, agent.transform.position.y, wanderPoint.z);
                        
            if(agent.remainingDistance < 0.001f)
            {
               agent.SetDestination(moveVector);             
            }
        }
    }

    private IEnumerator taunt()//possible
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
