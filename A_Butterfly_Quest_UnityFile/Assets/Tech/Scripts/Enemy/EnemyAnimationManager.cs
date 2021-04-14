using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private EnemyAIv2 EnemyScpt;

    private void Start()
    {
        EnemyScpt = gameObject.GetComponent<EnemyAIv2>();
    }



}
