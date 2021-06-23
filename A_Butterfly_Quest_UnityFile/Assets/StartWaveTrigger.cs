using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaveTrigger : MonoBehaviour
{
    public static StartWaveTrigger m_instance;

    public bool canStartWave;

    private void Awake()
    {
        m_instance = this;
    }

    private void Start()
    {
        canStartWave = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canStartWave)
        {
            if(other.tag == "Player")
            {
                WavesManager.instance.StartWaveSystem();
            }
            canStartWave = false;
        }
    }
}
