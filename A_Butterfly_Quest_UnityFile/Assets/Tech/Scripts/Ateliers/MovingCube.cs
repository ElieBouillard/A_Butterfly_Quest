using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{

    Vector3 target;
    bool canMoove = true;
    float mooveClock;
    private void Start()
    {
        target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 10f * Time.deltaTime);

        if(mooveClock > 0)
        {
            mooveClock -= Time.deltaTime;
            canMoove = false;
        }
        else
        {
            canMoove = true;
        }

    }
    private void OnCollisionEnter(Collision hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            Character3D playerScpt = hit.gameObject.GetComponent<Character3D>();
            int indexValue = ButterflyTypeSelection.Instance.SelectionTypeValue;
            Vector3 dir = hit.contacts[0].normal;
            bool raycast = Physics.Raycast(transform.position, dir, 4.5f);

            if (playerScpt.canDash == false && indexValue == 2 && canMoove && !raycast)
            {
                target = new Vector3(transform.position.x, transform.position.y, transform.position.z) + dir * 2.5f;
                mooveClock = 2f;
            }           
        }
    }    
}
