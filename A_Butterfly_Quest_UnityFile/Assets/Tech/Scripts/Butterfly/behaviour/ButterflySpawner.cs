using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    ButterflyCluster m_cluster;
    public Vector2 butterfliesAmount_MinMax = new Vector2(5,7);
    public GameObject butterfly_prefab;
    float spawnAmount;

    void Start()
    {
        m_cluster = GetComponent<ButterflyCluster>();
        spawnAmount = Random.Range(butterfliesAmount_MinMax.x, butterfliesAmount_MinMax.y);
        SpawnButterflies();
    }
    void SpawnButterflies()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject newButterfly = Instantiate(butterfly_prefab, transform.position, Quaternion.identity);
            newButterfly.GetComponent<ButterflyBehaviour>().parentCluster = m_cluster;
            float randomSize = Random.Range(.1f, .25f);
            newButterfly.transform.localScale = new Vector3(randomSize,randomSize,randomSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
