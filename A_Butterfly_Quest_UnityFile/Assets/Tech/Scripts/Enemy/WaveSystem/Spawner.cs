using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyObj;

    public GameObject InstantiateEnemy()
    {
        GameObject tempEnemy;
        tempEnemy = Instantiate(EnemyObj, transform.position, Quaternion.identity);
        return tempEnemy;
    }
}
