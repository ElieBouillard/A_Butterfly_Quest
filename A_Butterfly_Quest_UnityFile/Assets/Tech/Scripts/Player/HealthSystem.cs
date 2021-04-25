using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public enum CharacterType { Player, Enemy}
    [Header("Type")]
    public CharacterType m_CharacterType;

    [Header("Stats")]
    public float InitialHealth;

    [Header("Links")]
    private RespawnSystem m_repsawnSystem; 

    [Header("Debug")]
    public float CurrHealth;
    public Material TakingDamageFeedBack;

    private void Start()
    {
        m_repsawnSystem = gameObject.GetComponent<RespawnSystem>();
        CurrHealth = InitialHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown("k"))
        {
            if(m_CharacterType == CharacterType.Player)
            {
                Death();
            }
        }
    }

    public void TakeDamage(float DamageValue)
    {
        CurrHealth -= DamageValue;

        if(CurrHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        if(m_CharacterType == CharacterType.Enemy)
        {
            gameObject.SetActive(false);
        }
        else if(m_CharacterType == CharacterType.Player)
        {
            gameObject.transform.position = m_repsawnSystem.currRespawnPoint;
        }
    }
}
