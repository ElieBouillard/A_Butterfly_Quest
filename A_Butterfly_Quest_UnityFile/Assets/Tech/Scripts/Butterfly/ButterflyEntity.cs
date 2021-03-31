using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyEntity
{
    public float ButterflySpeed = 0;
    public float Damage = 0;
    public enum m_ButterflyType { Classic, Illusion, Vent }
    public m_ButterflyType ButterflyType;

    public ButterflyEntity(float butterflySpeed, float damage, m_ButterflyType butterflyType)
    {
        ButterflySpeed = butterflySpeed;
        Damage = damage; 
        ButterflyType = butterflyType;
    }
}
