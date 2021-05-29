using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCollider : MonoBehaviour
{

    private List<Collider> currButterflyCatch = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Butterfly")
        {
            if (!currButterflyCatch.Contains(other))
            {
                currButterflyCatch.Add(other);
            }
            //other.gameObject.SetActive(false);
        }
    }

    public List<GameObject> GetButterflyAndClearList()
    {
        List<GameObject> ListToGive = new List<GameObject>();
        for (int i = 0; i < currButterflyCatch.Count; i++)
        {
            ListToGive.Add(currButterflyCatch[i].gameObject);
        }
        currButterflyCatch.Clear();
        return ListToGive;
    }
}
