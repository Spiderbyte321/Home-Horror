using System;
using System.Collections;
using UnityEngine;

public class MonsterV1 : AbstractMonster
{
    
    
    private bool revealed;

    public delegate void damagePLayer(int damageDealt);

    public static event damagePLayer OnDamagePlayerAction;
    private void OnBecameVisible()
    {
        if(revealed)
            return;

        revealed = true;
        SFXManager.instance.playglobalJumpScare("scare2");
        StartCoroutine(disapearRoutine());
    }

    private IEnumerator disapearRoutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void damagePlayer()
    {
        OnDamagePlayerAction?.Invoke(10);
    }
}
