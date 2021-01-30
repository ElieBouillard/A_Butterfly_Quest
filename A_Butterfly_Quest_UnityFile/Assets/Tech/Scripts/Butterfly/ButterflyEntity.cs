using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyEntity
{
    public float ButterflySpeed = 0;
    public float Damage = 0;
    public float TicFirePerSec = 0;
    public bool Illusion = false;

    public ButterflyEntity(float butterflySpeed, float damage, float ticFirePerSec, bool illusion)
    {
        ButterflySpeed = butterflySpeed;
        Damage = damage;
        TicFirePerSec = ticFirePerSec;
        Illusion = illusion;
    }
}
