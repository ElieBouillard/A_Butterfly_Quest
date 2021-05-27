using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public List<string> scenesToLoad = new List<string>();
    [SerializeField]
    private bool instantLoad = false;

    private void Awake()
    {
        if (instantLoad)
        {
            LoadGameplayScenes();
        }
    }


    public void LoadGameplayScenes()
    {
        for (int i = 0; i < scenesToLoad.Count; i++)
        {   if(i == 0)
            {
                SceneManager.LoadScene(scenesToLoad[i]);
            }
            else
            {
                SceneManager.LoadScene(scenesToLoad[i], LoadSceneMode.Additive);
            }            
        }
    }


    [ContextMenu("Assign all scenes in build")]
    private void AssignBuildScenes()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (scenesToLoad.Contains(SceneManager.GetSceneByBuildIndex(i).name) == false && SceneManager.GetSceneByBuildIndex(i).name != "main_scene")
            {
                scenesToLoad.Add(SceneManager.GetSceneByBuildIndex(i).name);
            }
        }
    }
}
