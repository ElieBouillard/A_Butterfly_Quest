using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RonceBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Character3D>())
        {
            HealthSystem m_healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            m_healthSystem.TakeDamage(m_healthSystem.CurrHealth);
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ButterflyBullet>())
        {
            ButterflyBullet m_bulletScp = other.gameObject.GetComponent<ButterflyBullet>();
            if (m_bulletScp.Type == 2)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
