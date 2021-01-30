using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyPooler : MonoBehaviour
{
    public static ButterflyPooler SharedInstance;

    public List<GameObject> pooledButterfly;
    public GameObject ButterflyToPool;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledButterfly = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(ButterflyToPool);
            obj.SetActive(false);
            pooledButterfly.Add(obj);
        }
    }

    //Retourne le premier objet inactif disponible
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledButterfly.Count; i++)
        {
            if (!pooledButterfly[i].activeInHierarchy)
            {
                return pooledButterfly[i];
            }
        }
        return null;
    }
}
