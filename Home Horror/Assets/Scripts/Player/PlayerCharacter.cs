using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int maxSanity = 100;
    [SerializeField] private int currentSanity = 100;
    [SerializeField] private int sanityThreshold = 10;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;

    public int CurrentSanity => currentSanity;
    public int CurrentHealth => currentHealth;
    public int SanityThreshold => sanityThreshold;
    public int MaxSanity => maxSanity;

    public delegate void SanityUpdateAction(int currentSanity);
    public static event SanityUpdateAction OnSanityUpdateAction;

    private void OnEnable()
    {
        SanityEvent.OnSanityEvent += TakeSanityDamage;
        SanityDrainController.OnSanityDrain += TakeSanityDamage;
        DegradationController.OnRepairedAction += HealSanity;
        GameManager.OnSystemLeft += TakeSanityDamage;
        MonsterV1.OnDamagePlayerAction += TakeSanityDamage;
    }

    private void OnDisable()
    {
        SanityEvent.OnSanityEvent -= TakeSanityDamage;
        SanityDrainController.OnSanityDrain -= TakeSanityDamage;
        DegradationController.OnRepairedAction -= HealSanity;
        GameManager.OnSystemLeft -= TakeSanityDamage;
        MonsterV1.OnDamagePlayerAction -= TakeSanityDamage;
    }


    private void HealSanity(int healedAmount)
    {
        currentSanity += healedAmount;

        if (currentSanity >= maxSanity)
            currentSanity = maxSanity;
        
        OnSanityUpdateAction?.Invoke(currentSanity);
    }

    private void TakeHealthDamage(int damage)
    {
        Debug.Log($"Player took : {damage} health damage");

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Debug.Log("PLAYER DIED!");

            GameManager.TriggerPlayerDeath();
        }
    }

    private void TakeSanityDamage(int damage)
    {
        Debug.Log($"Player took : {damage} sanity damage");
        currentSanity -= damage;

        if (currentSanity < sanityThreshold)
        {
            int overflow = sanityThreshold - currentSanity;
            TakeHealthDamage(overflow);
        }

        OnSanityUpdateAction?.Invoke(currentSanity);
    }
}
