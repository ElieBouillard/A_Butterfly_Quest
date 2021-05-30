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
        }
    }

    public List<ButterflyBehaviourV2> GetButterflyAndClearList()
    {
        List<ButterflyBehaviourV2> ListToGive = new List<ButterflyBehaviourV2>();
        for (int i = 0; i < currButterflyCatch.Count; i++)
        {
            ListToGive.Add(currButterflyCatch[i].gameObject.GetComponent<ButterflyBehaviourV2>());
        }
        currButterflyCatch.Clear();
        return ListToGive;
    }
}
