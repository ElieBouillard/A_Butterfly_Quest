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
    public List<List<ButterflyBehaviourV2>> ButterflyInInventory = new List<List<ButterflyBehaviourV2>>(3);

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

    public void ShootedButterfly(ButterflyBehaviourV2 currButterfly, Vector3 HitImpact)
    {
        ButterflyInInventory[(int)currButterfly.m_ButterflyType].Remove(currButterfly);
        currButterfly.gameObject.SetActive(false);
        currButterfly.gameObject.transform.position = HitImpact;
    }

    public void ShootedButterfly(ButterflyBehaviourV2 currButterfly)
    {
        ButterflyInInventory[(int)currButterfly.m_ButterflyType].Remove(currButterfly);
        currButterfly.gameObject.SetActive(false);
    }

    public void SetButterflyToRecovery(ButterflyBehaviourV2 currButterfly)
    {
        currButterfly.gameObject.SetActive(true);
        currButterfly.SetToRecovery();
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
    }
}
