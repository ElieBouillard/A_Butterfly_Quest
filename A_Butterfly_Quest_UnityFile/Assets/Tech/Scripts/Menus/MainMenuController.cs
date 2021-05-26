using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public float TitleTime;
    public GameObject StartMenu;
    public GameObject BlackScreen;
    public GameObject Title;
    private Animator TitleAnimator;
    private Animator BlackScreenAnimator;

    private float clockTitle;
    private float clockStartMenu;

    private void Start()
    {
        TitleAnimator = Title.GetComponent<Animator>();
        BlackScreenAnimator = BlackScreen.GetComponent<Animator>();
        clockTitle = TitleTime;
        clockStartMenu = TitleTime + 2f;
        TitleAnimator.SetBool("InFront", true);
        BlackScreenAnimator.SetBool("Active", true); 
    }

    bool launched;
    private void Update()
    {
        if(clockTitle > 0)
        {
            clockTitle -= Time.deltaTime;
        }
        else
        {
            TitleAnimator.SetBool("InFront", false);
            BlackScreenAnimator.SetBool("Active", false);
        }

        if (!launched)
        {
            if (clockStartMenu > 0)
            {
                clockStartMenu -= Time.deltaTime;
            }
            else
            {
                StartMenu.SetActive(true);
                launched = true;
            }
        }
    }
}
