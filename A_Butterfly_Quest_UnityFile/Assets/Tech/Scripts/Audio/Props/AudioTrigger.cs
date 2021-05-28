using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public string SFXName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (AudioManager.instance.sounds[0].source != null)
            {
                AudioManager.instance.Play(SFXName);
            }
        }
    }
}
