using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    bool butterflyCatch;
    bool isShowingTip;
    public string SFXName;
    bool canHide = true;

    void Update()
    {
        if (!butterflyCatch)
        {
            if (Character3D.m_instance.GetComponent<ButterflyInventory>().ButterflyIllusionInInventoryValue >= 1 || Character3D.m_instance.GetComponent<ButterflyInventory>().ButterflyTempeteInInventoryValue >=1)
            {

                if (AudioManager.instance.sounds[0].source != null)
                {
                    AudioManager.instance.Play(SFXName);
                }
                butterflyCatch = true;
                TipsManager.instance.ShowTip(9, TipsManager.TipType.BottomTip);
                isShowingTip = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            if (isShowingTip && canHide)
            {
                TipsManager.instance.HideTip(TipsManager.TipType.BottomTip);
                canHide = false;
            }
        }
    }
}
