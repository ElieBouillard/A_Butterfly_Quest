﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public enum CharacterType { Player, Enemy}
    [Header("Type")]
    public CharacterType m_CharacterType;

    [Header("Stats")]
    public float InitialHealth;
    public int numOfHearts;

    [Header("Links")]
    private RespawnSystem m_repsawnSystem;
    private Image[] hearts;
    private Sprite fullHearth;
    private Sprite emptyHearth;

    [Header("Debug")]
    public float CurrHealth;
    public Material TakingDamageFeedBack;

    private bool canHit;
    private float clockCanHit;

    private void Start()
    {
        m_repsawnSystem = gameObject.GetComponent<RespawnSystem>();
        CurrHealth = InitialHealth;
        fullHearth = UIManager.instance.fullHearth;
        emptyHearth = UIManager.instance.emptyHearth;
        canHit = true;
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

        HUDHealthUpdate();

        if(m_CharacterType == CharacterType.Player)
        {
            if(clockCanHit > 0)
            {
                clockCanHit -= Time.deltaTime;
                canHit = false;
            }
            else
            {
                canHit = true;
            }
        }
    }

    private void HUDHealthUpdate()
    {
        hearts = UIManager.instance.hearts;

        if (CurrHealth > numOfHearts)
        {
            CurrHealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < CurrHealth)
            {
                hearts[i].sprite = fullHearth;
            }
            else
            {
                hearts[i].sprite = emptyHearth;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(float DamageValue)
    {
        if(m_CharacterType == CharacterType.Player)
        {
            if (canHit)
            {
                CurrHealth -= DamageValue;
                clockCanHit = 0.5f;
            }
            
        }
        else
        {
            CurrHealth -= DamageValue;
        }


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
