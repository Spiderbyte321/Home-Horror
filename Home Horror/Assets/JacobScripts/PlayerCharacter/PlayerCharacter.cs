using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int maxSanity = 100;
    [SerializeField] private int currentSanity = 90;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 90;
    [SerializeField] private int SanityThreshold=40;
    private int MoneyOwned = 0;
    private Dictionary<string, int> PlayerMaterials = new Dictionary<string, int>();

    public int CurrentSanity => currentSanity;
    public int CurrentHealth => currentHealth;


    public delegate void SanityStatusAction();

    public static event SanityStatusAction OnSanityAction;

    public delegate void HealthUpdateAction(int currentHealth);

    public static event HealthUpdateAction OnHealthAction;


    private void OnEnable()
    {
        //Subscribe to event to heal
        //Sub to increase material and money
    }

    private void OnDisable()
    {
        //unsub
        //unsub
    }
    

    private void HealHealth(int AHealedAmount)
    {
        currentHealth += AHealedAmount;
        
        if(CurrentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        
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

    public void TakeHealthDamage(int ADamage)
    {
        currentHealth -= ADamage;
        
        OnHealthAction?.Invoke(currentHealth);
    }

    public void TakeSanityDamage(int ADamage)//Update to support more complex behaviour
    {
        currentSanity -= ADamage;

        if (currentSanity < SanityThreshold)
        {
            OnSanityAction?.Invoke();
        }
    }

    private void IncreaseMoney(int AAmount)
    {
        MoneyOwned += AAmount;
    }

    private void GainMaterials(Material AMaterial)
    {
        if(!PlayerMaterials.TryAdd(AMaterial.Name, AMaterial.Amount))
        {
            PlayerMaterials[AMaterial.Name] += AMaterial.Amount;
        }
    }
}
