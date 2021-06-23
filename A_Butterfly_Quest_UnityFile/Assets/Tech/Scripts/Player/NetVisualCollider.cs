using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetVisualCollider : MonoBehaviour
{
    public static NetVisualCollider m_instance;

    public List<GameObject> ButterflyInCollider = new List<GameObject>();
    public List<GameObject> ButterflyExitCollider = new List<GameObject>();

    [HideInInspector]
    public bool colliderOn;

    private void Awake()
    {
        m_instance = this;
    }

    private void Update()
    {
        if (gameObject.GetComponent<Collider>().enabled)
        {
            colliderOn = true;
        }
        else
        {
            colliderOn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Butterfly")
        {
            other.transform.GetChild(0).transform.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Butterfly")
        {
            other.transform.GetChild(0).transform.gameObject.GetComponent<ParticleSystem>().Clear();
            other.transform.GetChild(0).transform.gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }



}
