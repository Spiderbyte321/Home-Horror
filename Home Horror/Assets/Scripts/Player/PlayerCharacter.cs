using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int maxSanity = 100;
    [SerializeField] private int currentSanity = 90;
    [SerializeField] private int sanityThreshold = 40;
    
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 90;

    [SerializeField] private SubtitleManager subtitleManager;

    public int CurrentSanity => currentSanity;
    public int CurrentHealth => currentHealth;

    public delegate void SanityUpdateAction(int curreSanity);

    public static event SanityUpdateAction OnSanityUpdateAction;
    
    public delegate void HealthUpdateAction(int currentHealth);

    public static event HealthUpdateAction OnHealthAction;
    


    private void OnEnable()
    {
        //Subscribe to event to heal
        //Sub to increase material and money

        SanityEvent.OnSanityEvent += TakeSanityDamage;
    }

    private void OnDisable()
    {
        SanityEvent.OnSanityEvent -= TakeSanityDamage;
    }
    

    private void HealHealth(int AHealedAmount)
    {
        currentHealth += AHealedAmount;
        
        if(CurrentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        subtitleManager.PlaySubtitle("NotBad");
        OnHealthAction?.Invoke(currentHealth);
    }

    private void HealSanity(int AHealedAmount)
    {
        currentSanity += AHealedAmount;

        if(CurrentSanity >= maxSanity)
        {
            currentSanity = maxSanity;
        }
    }

    private void TakeHealthDamage(int ADamage)
    {
        Debug.Log($"PLayer took : {ADamage} health damage");
        currentHealth -= ADamage;
        
        OnHealthAction?.Invoke(currentHealth);
        subtitleManager.PlayRandomHurtSound();
    }

    private void TakeSanityDamage(int ADamage) 
    {
        Debug.Log($"PLayer took : {ADamage} sanity damage");
        currentSanity -= ADamage;
        OnSanityUpdateAction?.Invoke(currentSanity);
        subtitleManager.PlayRandomHurtSound(); //Remove when adding the link to health damage
    }
}
