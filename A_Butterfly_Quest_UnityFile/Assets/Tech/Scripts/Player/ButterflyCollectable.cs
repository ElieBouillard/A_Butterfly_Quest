using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyCollectable : MonoBehaviour
{
    [Header("ButterflyStats")]
    public int ButterflyNumber;
    public float ButterflySpeed;
    public float Damage;
    public float TicFirePerSec;
    public bool Illusion;

    private void OnTriggerEnter(Collider other)
    {
        ButterflyInventory PlayerInventory = other.GetComponent<ButterflyInventory>();
        if(PlayerInventory != null)
        {
            PlayerInventory.CatchButterfly(ButterflyNumber, ButterflySpeed, Damage, TicFirePerSec, Illusion);
        }
    }
}
