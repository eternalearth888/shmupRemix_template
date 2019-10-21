using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("_Main_Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
