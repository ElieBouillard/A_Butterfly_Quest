using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsGenerator : MonoBehaviour
{
   public int intTip;
    public enum endCondition 
    { 
        PressB, 
        PressA, 
        ScopeAndShoot,
        ScopeAndShootBrambles,
        EnumShoot, 
        Move, 
        Dash, 
        Net, 
        Equip, 
        BigJump,
        Key,
        EndKey,
        DashTempete,
        TirTempete,
        DashIllu
    }

    public endCondition m_condition;
    bool isShowingTip;
    bool jump;
    bool dontShowTipAgain;

    public string SFXName;
    float clockHide;
    bool canHide;
    bool canClockHide;

    bool canPlaySFX;

    public GameObject gameObjectNeeded;

    private void Start()
    {
        canClockHide = true;
        canPlaySFX = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dontShowTipAgain == false )
            {          
                if (AudioManager.instance.sounds[0].source != null && canPlaySFX)
                {
                    AudioManager.instance.Play(SFXName);
                    canPlaySFX = false;
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
            canClockHide = false;
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
            if (gameObjectNeeded.GetComponent<TriggerTips>().succes == true)
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.DashTempete && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button2) && gameObjectNeeded.GetComponent<MovingCube>().CollideWithPlayer == true)
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.DashIllu && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button2) && Character3D.m_instance.GetComponent<ButterflyTypeSelection>().SelectionTypeValue == 1)
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        if (m_condition == endCondition.Net && isShowingTip)
        {
            if (Character3D.m_instance.GetComponent<ButterflyInventory>().ButterflyInInventoryValue >= 1)
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
            if (gameObjectNeeded.GetComponent<Receptacle>().Completed == true)
            {
                Hide();
                dontShowTipAgain = true;
            }

        }
        if (m_condition == endCondition.ScopeAndShootBrambles && isShowingTip)
        {
            
            if (Input.GetAxis("Aim") > 0 && Input.GetAxisRaw("Fire1") == 1)
            {
                Hide();
                dontShowTipAgain = true;
            }
        }
        
        
        if (m_condition == endCondition.Key && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                Hide();
                dontShowTipAgain = true;
            }
            
        }

        if (m_condition == endCondition.BigJump && isShowingTip)
        {
            if (gameObjectNeeded.GetComponent<TriggerTips>().succes == true)
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

            if (KeyInventory.instance.GetKeyCount() < 3)
            {
                if (canClockHide)
                {
                    clockHide = 5;
                }

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

