using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipsManager : MonoBehaviour
{
    public static TipsManager instance;
    public enum TipType { BottomTip, CustomTip, SideTip};



    public Animator m_anim;
    private float tipTimerCount;
    private float tipTimerDuration;
    private TipType tipTimerType;

    //Bottomtips
    public List<string> BottomTipsText = new List<string>();
    public TextMeshProUGUI bottomTipText;

    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        tipTimerDuration = -1f;
        tipTimerCount = 0;
    }

    void Update()
    {
        if(tipTimerDuration > 0.01f)
        {
            if(tipTimerCount < tipTimerDuration)
            {
                tipTimerCount += Time.deltaTime;
            }
            else
            {
                tipTimerDuration = -1f;
                tipTimerCount = 0f;
                HideTip(tipTimerType);
                 
            }
        }
    }


    public void ShowTip(int index,TipType m_type, float duration = -1)
    {
        //if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Hidden") == false)
        //{
        //    m_anim.SetTrigger("BottomTip_Disappear");
        //}

        switch (m_type)
        {
            case TipType.BottomTip:
                bottomTipText.text = BottomTipsText[index];
                m_anim.SetTrigger("BottomTip_Appear");
                break;
        }


        if(duration >= 0f)
        {
            tipTimerDuration = duration;
            tipTimerCount = 0;
            tipTimerType = m_type;
        }
        
    }

    public void HideTip(TipType m_type)
    {
        switch (m_type)
        {
            case TipType.BottomTip:
                m_anim.SetTrigger("BottomTip_Disappear");
                break;
        }
        Debug.Log("HIDEE");
    }

}
