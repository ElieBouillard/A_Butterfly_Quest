using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public int Index;
    public int MaxIndex;
    [Header("Debug")]
    public bool KeyDown;

    public GameObject[] Buttons;

    //[HideInInspector]
    public bool canPlaySound;

    private void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            if (!KeyDown)
            {
                if(Input.GetAxis("Vertical")< 0)
                {
                    if(Index < MaxIndex)
                    {
                        Index++;
                    }
                    else
                    {
                        Index = 0;
                    }
                }
                else if(Input.GetAxis("Vertical") > 0)
                {
                    if(Index > 0)
                    {
                        Index--;
                    }
                    else
                    {
                        Index = MaxIndex;
                    }
                }
                KeyDown = true;
                canPlaySound = true;
            }
        }
        else
        {
            KeyDown = false;
        }
    }
}
