using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterflyInventory : MonoBehaviour
{
    public static ButterflyInventory Instance;

    [Header("Debug Info")]
    public int ButterflyInInventoryValue;
    public int ButterflyIllusionInInventoryValue;
    public int ButterflyOtherInInventoryValue;
    public int ButterflyInTravelValue;
    public int ButterflyToReloadValue;

    public List<List<ButterflyEntity>> ButterflyInInventory = new List<List<ButterflyEntity>>(3);

    public List<ButterflyEntity> ButterflyInTravel = new List<ButterflyEntity>();
    public List<ButterflyEntity> ButterflyToReload = new List<ButterflyEntity>();

    private float clock;
    private bool Reloading = false;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        for (int i = 0; i <= ButterflyTypeSelection.Instance.MaxButterflyType; i++)
        {
            ButterflyInInventory.Add(new List<ButterflyEntity>());
        }
    }

    public void CatchButterfly(int butterflyNumbre,ButterflyEntity currButterfly)
    {
        for (int i = 0; i < butterflyNumbre; i++)
        {
            ButterflyInInventory[(int)currButterfly.ButterflyType].Add(currButterfly);
        }        
    }

    public void ShootedButterfly(ButterflyEntity currButterfly)
    {
        ButterflyInTravel.Add(currButterfly);
        ButterflyInInventory[(int)currButterfly.ButterflyType].Remove(currButterfly);
    }

    public void AddToReloadList(ButterflyEntity currButterfly)
    {
        ButterflyToReload.Add(currButterfly);
        ButterflyInTravel.Remove(currButterfly);
    }

    public void StartReload()
    {
        Reloading = true;
        Debug.Log("Reloading...");
        clock = 2f;
    }

    public void Reload()
    {
        if ( ButterflyToReload.Count > 0)
        {
            for (int i = 0; i < ButterflyToReload.Count; i++)
            { 
                ButterflyInInventory[(int)ButterflyToReload[i].ButterflyType].Add(ButterflyToReload[i]);
            }
            ButterflyToReload.Clear();
            Debug.Log("Reloading success !");
        }
        else
        {
            Debug.Log("No butterfly to reload !");
        }
        Reloading = false;
    }

    private void Update()
    {
        //Debug tailles des listes
        ButterflyInInventoryValue = ButterflyInInventory[0].Count;
        ButterflyIllusionInInventoryValue = ButterflyInInventory[1].Count;
        ButterflyOtherInInventoryValue = ButterflyInInventory[2].Count;
        ButterflyInTravelValue = ButterflyInTravel.Count;
        ButterflyToReloadValue = ButterflyToReload.Count;

        //Clock reload
        if (Reloading)
        {
            clock -= Time.deltaTime;
            if(clock < 0)
            {
                Reload();
            }
        }
    }
}
