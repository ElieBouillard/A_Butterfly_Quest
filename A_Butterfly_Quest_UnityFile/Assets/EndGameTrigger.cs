using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AnimationManager.m_instance.canPlayStepSound = false;
            UIManager.instance.EngGameScreen.SetActive(true);
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
