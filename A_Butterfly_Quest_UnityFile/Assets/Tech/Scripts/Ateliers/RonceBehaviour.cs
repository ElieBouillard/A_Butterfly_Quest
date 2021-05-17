using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RonceBehaviour : MonoBehaviour
{
    public float KnockBackStrenght;
    private GameObject playerOb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ButterflyBullet>())
        {
            ButterflyBullet m_bulletScp = collision.gameObject.GetComponent<ButterflyBullet>();
            if (m_bulletScp.Type == 2)
            {
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.GetComponent<Character3D>())
        {
            playerOb = collision.gameObject;

            Character3D PlayerScpt;
            PlayerScpt = collision.gameObject.GetComponent<Character3D>();

            HealthSystem m_healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            m_healthSystem.TakeDamage(1);

            Vector3 dir = playerOb.transform.position - transform.position;
            dir.y = 0.1f;
            PlayerScpt.InitKnockBack();
            PlayerScpt.m_rb.AddForce(dir.normalized * KnockBackStrenght, ForceMode.Impulse);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
