using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterflyInventory : MonoBehaviour
{
    public static ButterflyInventory Instance;

    [Header("Debug Info")]
    public int ButterflyInInventoryValue;
    public int ButterflyToReloadValue;

    public List<ButterflyEntity> ButterflyInInventory = new List<ButterflyEntity>();
    public List<ButterflyEntity> ButterflyToReload = new List<ButterflyEntity>();

    //Test number of bullets
    public Text numbreButterfly;

    public void Awake()
    {
        Instance = this;
    }

    public void CatchButterfly(int butterflyNumbre,float butterflySpeed, float damage, float ticFirePerSec, bool illusion)
    {
        for (int i = 0; i < butterflyNumbre; i++)
        {
            ButterflyEntity catchedButterfly = new ButterflyEntity(butterflySpeed, damage, ticFirePerSec, illusion);
            ButterflyInInventory.Add(catchedButterfly);
        }        
    }

    public void ShootedButterfly()
    {
        ButterflyInInventory.RemoveAt(0);
    }

    public void AddToReloadList(float butterflySpeed, float damage, float ticFirePerSec, bool illusion)
    {
        ButterflyEntity butterflyToAdd = new ButterflyEntity(butterflySpeed, damage, ticFirePerSec, illusion);
        ButterflyToReload.Add(butterflyToAdd);
    }

    public void Reload()
    {
        if ( ButterflyToReload.Count > 0)
        {
            for (int i = 0; i < ButterflyToReload.Count; i++)
            {
                ButterflyInInventory.Add(ButterflyToReload[i]);
            }
            ButterflyToReload.Clear();
        }
        else
        {
            Debug.Log("No butterfly to reload !");
        }
    }

    private void Update()
    {
        //Debug tailles des listes
        ButterflyInInventoryValue = ButterflyInInventory.Count;
        ButterflyToReloadValue = ButterflyToReload.Count;

        //Nombre de papillon à tirer
        numbreButterfly.text = ButterflyInInventory.Count.ToString();
    }
}
