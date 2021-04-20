using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Stats")]
    public float InitialHealth;


    [Header("Debug")]
    public float CurrHealth;
    public Material TakingDamageFeedBack;

    private void Start()
    {
        CurrHealth = InitialHealth;
    }

    public void TakeDamage(float DamageValue)
    {
        CurrHealth -= DamageValue;
        Debug.Log(CurrHealth);

        if(CurrHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
