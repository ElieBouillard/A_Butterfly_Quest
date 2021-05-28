using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollision : MonoBehaviour
{
    public string SFXName;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (AudioManager.instance.sounds[0].source != null)
            {
                AudioManager.instance.Play(SFXName);
            }
        }
    }
}
