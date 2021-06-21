using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HealthSystem>())
        {
            HealthSystem m_health = other.gameObject.GetComponent<HealthSystem>();
            m_health.TakeDamage(1);
        }
    }
}
