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


    private void Start()
    {
        //TipsManager.instance.ShowTip(4, TipsManager.TipType.BottomTip);
        //TipsManager.instance.ShowTip(4, TipsManager.TipType.BottomTip);
        //isShowingTip = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isShowingTip == false)
            {
                Show();
            }
        }
    }

    private void Update()
    {
        if (m_condition == endCondition.PressB && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                Hide();
            }
        }
        if (m_condition == endCondition.PressA && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                Hide();
            }
        }
        if (m_condition == endCondition.Dash && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                Hide();
            }
        }
        if (m_condition == endCondition.Net && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                Hide();
            }
        }
        if (m_condition == endCondition.Equip && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                Hide();
            }
        }
        if (m_condition == endCondition.ScopeAndShoot && isShowingTip) 
        {
            if (Input.GetAxis("Aim") > 0 && Input.GetAxisRaw("Fire1") == 1)
            {
                Hide();
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
            }
        }
        if (m_condition == endCondition.Key && isShowingTip)
        {
            if (Input.GetAxisRaw("GiveKey") == 1)
            {
               Hide();
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
                StartCoroutine(HideAfterTimer());
            }

        }

    }

    IEnumerator HideAfterTimer()
    {
        yield return new WaitForSeconds(5);
        Hide();
    }


        private void Hide()
    {
        isShowingTip = false;
        TipsManager.instance.HideTip(TipsManager.TipType.BottomTip);
    }
    private void Show()
    {
        TipsManager.instance.ShowTip(intTip, TipsManager.TipType.BottomTip);
        isShowingTip = true;
    }
}

