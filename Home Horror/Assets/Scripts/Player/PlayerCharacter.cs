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

    public int CurrentSanity => currentSanity;
    public int CurrentHealth => currentHealth;

    public int SanityThreshold => sanityThreshold;

    public delegate void SanityUpdateAction(int currentSanity);

    public static event SanityUpdateAction OnSanityUpdateAction;
    


    private void OnEnable()
    {
        SanityEvent.OnSanityEvent += TakeSanityDamage;
        SanityDrainController.OnSanityDrain += TakeSanityDamage;
        DegradationController.OnRepairedAction += HealSanity;
        GameManager.OnSystemLeft += TakeSanityDamage;
    }

    private void OnDisable()
    {
        SanityEvent.OnSanityEvent -= TakeSanityDamage;
        SanityDrainController.OnSanityDrain -= TakeSanityDamage;
        DegradationController.OnRepairedAction -= HealSanity;
        GameManager.OnSystemLeft -= TakeSanityDamage;
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
        Debug.Log($"Player took : {ADamage} health damage");
        currentHealth -= ADamage;
    }

    private void TakeSanityDamage(int ADamage) 
    {
        Debug.Log($"PLayer took : {ADamage} sanity damage");
        currentSanity -= ADamage;
        
        if(currentSanity < sanityThreshold)
            TakeHealthDamage(sanityThreshold-currentSanity);
        
        OnSanityUpdateAction?.Invoke(currentSanity);
    }
    
}
