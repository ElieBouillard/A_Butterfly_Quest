using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RonceBehaviour : MonoBehaviour
{
    public float KnockBackStrenght;
    private GameObject playerOb;
    private AudioSource m_audioSource;
    public AudioClip hitPlayerSound;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

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
            m_audioSource.PlayOneShot(hitPlayerSound);
        }
    }
}
