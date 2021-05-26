using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RonceBehaviour : MonoBehaviour
{
    public float KnockBackStrenght;
    private GameObject playerOb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Character3D>())
        {
            playerOb = collision.gameObject;

            Character3D PlayerScpt;
            PlayerScpt = collision.gameObject.GetComponent<Character3D>();
            Vector3 dir = playerOb.transform.position - transform.position;
            dir.y = 0.2f;
            PlayerScpt.InitKnockBack();
            PlayerScpt.m_rb.AddForce(dir.normalized * KnockBackStrenght, ForceMode.Impulse);

            HealthSystem m_healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            m_healthSystem.TakeDamage(1);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("qqlchose touche les ronces");
        if (collision.gameObject.GetComponent<ButterflyBullet>())
        {
            Debug.Log("balle touche ronce");
            ButterflyBullet m_bulletScp = collision.gameObject.GetComponent<ButterflyBullet>();
            if (m_bulletScp.Type == 2)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
