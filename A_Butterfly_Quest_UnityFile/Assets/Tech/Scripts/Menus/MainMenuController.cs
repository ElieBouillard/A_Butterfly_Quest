using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject MusiquePlayer;
    public GameObject StartMenu;
    public GameObject BlackScreen;
    public GameObject BlackScreen2;
    public GameObject Title;
    private Animator TitleAnimator;
    private Animator BlackScreenAnimator;
    private Animator BlackScreenAnimator2;

    private float clockFirstBlackScreen;
    private float clockTitle;
    private float clockStartMenu;

    private void Start()
    {
        TitleAnimator = Title.GetComponent<Animator>();
        BlackScreenAnimator = BlackScreen.GetComponent<Animator>();
        BlackScreenAnimator2 = BlackScreen2.GetComponent<Animator>();
        clockFirstBlackScreen = 2f;
        clockTitle = 4f;
        clockStartMenu = 6f;
        TitleAnimator.SetBool("InFront", true);
        BlackScreenAnimator.SetBool("Active", true);
        BlackScreenAnimator2.SetBool("Active", true);
        DontDestroyOnLoad(MusiquePlayer);
    }

    bool launched;
    private void Update()
    {
        if(clockFirstBlackScreen > 0)
        {
            clockFirstBlackScreen -= Time.deltaTime;
        }
        else
        {
            BlackScreenAnimator2.SetBool("Active", false);
        }

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
