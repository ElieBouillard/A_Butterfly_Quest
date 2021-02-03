using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyCollectable : MonoBehaviour
{
    [Header("ButterflyStats")]
    public int ButterflyNumber;
    public float ButterflySpeed;
    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        //Lors du ramassage de papillons
        ButterflyInventory PlayerInventory = other.GetComponent<ButterflyInventory>();
        if(PlayerInventory != null)
        {
            ButterflyEntity currButterfly = new ButterflyEntity(ButterflySpeed, Damage);
            PlayerInventory.CatchButterfly(ButterflyNumber, currButterfly);
        }
    }
}
