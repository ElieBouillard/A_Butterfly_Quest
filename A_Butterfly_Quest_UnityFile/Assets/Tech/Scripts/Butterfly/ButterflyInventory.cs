using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterflyInventory : MonoBehaviour
{
    public static ButterflyInventory Instance;

    [Header("Debug Info")]
    public int ButterflyInInventoryValue;
    public int ButterflyInTravelValue;
    public int ButterflyToReloadValue;

    public List<ButterflyEntity> ButterflyBasicInInventory = new List<ButterflyEntity>();
    public List<ButterflyEntity> ButterflyIllusionInInventory = new List<ButterflyEntity>();
    public List<ButterflyEntity> ButterflyLightInInventory = new List<ButterflyEntity>();

    public List<ButterflyEntity> ButterflyInTravel = new List<ButterflyEntity>();
    public List<ButterflyEntity> ButterflyToReload = new List<ButterflyEntity>();

    private float clock;
    private bool reloading = false;

    public void Awake()
    {
        Instance = this;
    }

    public void CatchButterfly(int butterflyNumbre,ButterflyEntity currButterfly)
    {
        for (int i = 0; i < butterflyNumbre; i++)
        {
            ButterflyBasicInInventory.Add(currButterfly);
        }        
    }

    public void ShootedButterfly(ButterflyEntity currButterfly)
    {
        ButterflyInTravel.Add(currButterfly);
        ButterflyBasicInInventory.Remove(currButterfly);
    }

    public void AddToReloadList(ButterflyEntity currButterfly)
    {
        ButterflyToReload.Add(currButterfly);
        ButterflyInTravel.Remove(currButterfly);
    }

    public void StartReload()
    {
        reloading = true;
        Debug.Log("Reloading...");
        clock = 2f;
    }

    public void Reload()
    {
        if ( ButterflyToReload.Count > 0)
        {
            for (int i = 0; i < ButterflyToReload.Count; i++)
            {
                ButterflyBasicInInventory.Add(ButterflyToReload[i]);
            }
            ButterflyToReload.Clear();
            Debug.Log("Reloading success !");
        }
        else
        {
            Debug.Log("No butterfly to reload !");
        }
        reloading = false;
    }

    private void Update()
    {
        //Debug tailles des listes
        ButterflyInInventoryValue = ButterflyBasicInInventory.Count;
        ButterflyInTravelValue = ButterflyInTravel.Count;
        ButterflyToReloadValue = ButterflyToReload.Count;

        //Clock reload
        if (reloading)
        {
            clock -= Time.deltaTime;
            if(clock < 0)
            {
                Reload();
            }
        }
    }
}
