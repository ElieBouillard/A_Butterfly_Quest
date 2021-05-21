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

    private bool _allReadyGived;
    private void Start()
    {
        PlayerMask = LayerMask.GetMask("Player");
        mode = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 50f * Time.deltaTime));
        Ray ray = new Ray(transform.position, transform.position);
        if (Physics.CheckSphere(transform.position, RangeToGiveKeyPlayer, PlayerMask))
        {
            if (!_allReadyGived)
            {
                KeyInventory.instance.AddKeyToInvetory();
                mode.SetActive(false);
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
