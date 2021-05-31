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
    public int ButterflyTempeteInInventoryValue;
    public int ButterflyInTravelValue;
    public int ButterflyToReloadValue;

    public List<List<ButterflyBehaviourV2>> ButterflyInInventory = new List<List<ButterflyBehaviourV2>>(3);

    public List<ButterflyBehaviourV2> ButterflyInTravel = new List<ButterflyBehaviourV2>();
    public List<ButterflyBehaviourV2> ButterflyToReload = new List<ButterflyBehaviourV2>();

    private float _clock;
    private bool _reloading = false;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        for (int i = 0; i <= ButterflyTypeSelection.Instance.MaxButterflyType; i++)
        {
            ButterflyInInventory.Add(new List<ButterflyBehaviourV2>());
        }
    }

    ButterflyBehaviourV2 currButterfly;
    public void SetButterflyToLauncherPos(int index)
    {
        if(ButterflyInInventory[index].Count > 0)
        {
            ButterflyInInventory[index][0].SetButterFlyToLauncherPos();    
        }
    }

    public void SetButterflyToPlayerCluster(int index)
    {
        if (ButterflyInInventory[index].Count > 0)
        {
            ButterflyInInventory[index][0].SetCatched();
        }
    }

    public void CatchButterfly(List<ButterflyBehaviourV2> currButterfly)
    {
        for (int i = 0; i < currButterfly.Count; i++)
        {
            ButterflyInInventory[(int)currButterfly[i].m_ButterflyType].Add(currButterfly[i]);
            currButterfly[i].SetCatched();
        }
    }

    public void ShootedButterfly(ButterflyBehaviourV2 currButterfly)
    {
        ButterflyInInventory[(int)currButterfly.m_ButterflyType].Remove(currButterfly);
        currButterfly.gameObject.SetActive(false);
    }

    public void AddToReloadList(ButterflyBehaviourV2 currButterfly)
    {
        ButterflyToReload.Add(currButterfly);
    }

    public bool ReceptacleGiveButterfly(int ButterflyType)
    {
        if (ButterflyInInventoryValue > 0)
        {
            ButterflyInInventory[ButterflyType].RemoveAt(ButterflyInInventoryValue - 1);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartReload()
    {
        _reloading = true;
        Debug.Log("Reloading...");
        _clock = 1f;
    }

    public void Reload()
    {
        if (ButterflyToReload.Count > 0)
        {
            int reloadValue = ButterflyToReload.Count;
            for (int i = 0; i < ButterflyToReload.Count; i++)
            {
                ButterflyInInventory[(int)ButterflyToReload[i].m_ButterflyType].Add(ButterflyToReload[i]);
                ButterflyToReload[i].SetCatched();
                ButterflyToReload[i].gameObject.SetActive(true);

            }
            ButterflyToReload.Clear();
            Debug.Log("Reloading success with " + reloadValue + " butterfly !");
        }
        else
        {
            Debug.Log("No butterfly to reload !");
        }
        _reloading = false;
    }

    public ButterflyBehaviourV2 GetFirstButterfly(int index)
    {
        return ButterflyInInventory[index][0];
    }

    bool check = false;
    int lastSelection  = 0;
    private void Update()
    {
        //Debug tailles des listes
        ButterflyInInventoryValue = ButterflyInInventory[0].Count;
        ButterflyIllusionInInventoryValue = ButterflyInInventory[1].Count;
        ButterflyTempeteInInventoryValue = ButterflyInInventory[2].Count;
        ButterflyInTravelValue = ButterflyInTravel.Count;
        ButterflyToReloadValue = ButterflyToReload.Count;

        
        //Deplacement Papillon Au Launcher
        if (Shoot.Instance.Aiming)
        {
            if(ButterflyTypeSelection.Instance.SelectionTypeValue != lastSelection)
            {
                SetButterflyToPlayerCluster(lastSelection);
                SetButterflyToLauncherPos(ButterflyTypeSelection.Instance.SelectionTypeValue);
                lastSelection = ButterflyTypeSelection.Instance.SelectionTypeValue;
            }
            else
            {

                SetButterflyToLauncherPos(ButterflyTypeSelection.Instance.SelectionTypeValue);
                check = true;
            }

        }
        else
        {
            if(check == true)
            {
                SetButterflyToPlayerCluster(ButterflyTypeSelection.Instance.SelectionTypeValue);
                check = false;
            }

        }

        //Clock reload
        if (_reloading)
        {
            _clock -= Time.deltaTime;
            if (_clock < 0)
            {
                Reload();
            }
        }
    }
}
