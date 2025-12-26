using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Scene Opening");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditScene()
    {
        SceneManager.LoadSceneAsync("Credit Scene");
    }

    public void ReviewGame()
    {
        Application.OpenURL("https://forms.gle/MC8ND1zgQapq36kg6"); 
    }
}
