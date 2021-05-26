using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    public List<GameObject> trigger = new List<GameObject>();

    void Start()
    {
        TipsManager.instance.ShowTip(4, TipsManager.TipType.BottomTip);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TipsManager.instance.ShowTip(0, TipsManager.TipType.BottomTip);
        }
    }
}
