using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessItem : MonoBehaviour
{
    [Header("Parametres")]
    [Range(0, 10f)]
    public float RangeToGiveKeyPlayer;

    LayerMask PlayerMask;
    GameObject mode;
    GameObject Player;

    private bool _allReadyGived;
    private bool canPlayMusic;

    public GameObject keyCatchVFX;
    private void Start()
    {
        PlayerMask = LayerMask.GetMask("Player");
        mode = transform.GetChild(0).gameObject;
        Player = Character3D.m_instance.gameObject;
    }

    float distToPlayer;
    private void Update() 
    {
        transform.Rotate(new Vector3(0, 0, 50f * Time.deltaTime));
        distToPlayer = (Player.transform.position - transform.position).magnitude;
        if(distToPlayer < RangeToGiveKeyPlayer)
        {
            if (!_allReadyGived)
            {
                AudioManager.instance.Play("Key");
                KeyInventory.instance.AddKeyToInvetory();
                mode.SetActive(false);
                if(keyCatchVFX != null)
                {
                    Instantiate(keyCatchVFX, transform.GetChild(0).GetChild(0).position, Quaternion.identity);  //SPAGHETTI
                }
                
                _allReadyGived = true;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RangeToGiveKeyPlayer);
    }
}
