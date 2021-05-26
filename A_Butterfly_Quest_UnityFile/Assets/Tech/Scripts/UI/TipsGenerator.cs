using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsGenerator : MonoBehaviour
{
   public int intTip;
    public enum endCondition { PressB, PressA, Scope, EnumShoot, Move, Dash, Net, Equip}
    public endCondition m_condition;
    public bool isShowingTip;
    bool move;


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
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                Hide();
            }
        }
        if (m_condition == endCondition.PressA && isShowingTip)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
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

        //Scope into Fire
        if (m_condition == endCondition.Scope && isShowingTip)
        {
            if (Input.GetAxis("Aim") > 0)
            {
                Hide();
                m_condition = endCondition.EnumShoot;
                TipsManager.instance.ShowTip(7, TipsManager.TipType.BottomTip);
                isShowingTip = true;
            }
        }

        if (m_condition == endCondition.EnumShoot && isShowingTip)
        {
            if (Input.GetAxisRaw("Fire1") == 1)
            {
                Hide();
            }
        }

        //if (m_condition == endCondition.Move && isShowingTip)
        //{
        //    if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Vertical") != 0)
        //    {
        //        Hide();
        //        move = true;
        //    }
        //}

        //if (move == true)
        //{
        //    TipsManager.instance.ShowTip(5, TipsManager.TipType.BottomTip);
        //    isShowingTip = true;
            
        //        if (Input.GetAxisRaw("MouseX") != 0 || Input.GetAxisRaw("MouseY") != 0)
        //        {
        //            Hide();
        //        }
            
        //}
        
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

