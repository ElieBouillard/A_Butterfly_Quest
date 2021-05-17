using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BuildScript : MonoBehaviour
{
    public void Awake()
    {
    }

    private void Start()
    {
        SceneManager.LoadScene("LD", LoadSceneMode.Additive);
        SceneManager.LoadScene("VOLUMES", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI_HUD_Scene", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI_TIPS_Scene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        
    }
}
