using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BuildScript : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("GreyBoxLD", LoadSceneMode.Additive);
        SceneManager.LoadScene("HUD_Scene", LoadSceneMode.Additive);
        SceneManager.LoadScene("VOLUMES", LoadSceneMode.Additive);
    }
}
