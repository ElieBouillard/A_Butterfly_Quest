using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsGenerator : MonoBehaviour
{
   public int intTip;
    public enum endCondition { 
        PressB, 
        PressA, 
        ScopeAndShoot, 
        EnumShoot, 
        Move, 
        Dash, 
        Net, 
        Equip, 
        BigJump,
        Key,
        EndKey}

    public endCondition m_condition;
    bool isShowingTip;
    bool jump;
    bool dontShowTipAgain;

    public string SFXName;
    float clockHide;
    bool canHide;
    bool canClockHide;

    private void Start()
    {
        //TipsManager.instance.ShowTip(4, TipsManager.TipType.BottomTip);
        //TipsManager.instance.ShowTip(4, TipsManager.TipType.BottomTip);
        //isShowingTip = true;
        canClockHide = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dontShowTipAgain == false)
            {          
                if (AudioManager.instance.sounds[0].source != null)
                {
                    AudioManager.instance.Play(SFXName);
                }           
            }


            if (isShowingTip == false)
            {
                Show();
            }
        }
    }

    private void Update()
    {
        if (clockHide > 0)
        {
            clockHide -= Time.deltaTime;
            canHide = true;
        }
        else
        {
            if (canHide)
            {
                Hide();
                canHide = false;
                canClockHide = true;
            }
        }

        if (m_condition == endCondition.PressB && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.PressA && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.Dash && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.Net && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.Equip && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.ScopeAndShoot && isShowingTip) 
        {
            if (Input.GetAxis("Aim") > 0 && Input.GetAxisRaw("Fire1") == 1)
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.BigJump && isShowingTip) 
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                jump = true;
            }
            if (jump && Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.Key && isShowingTip)
        {
            if (Input.GetAxisRaw("GiveKey") == 1)
            {
                Hide();
                dontShowTipAgain = true;
            }
            
        }



        if (m_condition == endCondition.EndKey && isShowingTip)
        {
            if (Input.GetAxisRaw("GiveKey") == 1 && KeyInventory.instance.GetKeyCount() >= 3)
            {
                Hide();
            }
            if (KeyInventory.instance.GetKeyCount() < 3 && canClockHide)
            {
                clockHide = 5;
                Debug.Log(clockHide);
                canClockHide = false;
            }

        }
    }
        private void Hide()
        {
            isShowingTip = false;
            TipsManager.instance.HideTip(TipsManager.TipType.BottomTip);
        }

    private void Show()
    {
        if (dontShowTipAgain == false)
        {

            TipsManager.instance.ShowTip(intTip, TipsManager.TipType.BottomTip);
                isShowingTip = true;
        }
    }

   
}

