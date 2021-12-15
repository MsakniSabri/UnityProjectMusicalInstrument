using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartNormalGame()
    {
        SceneManager.LoadScene("NormalGameScene");
    }

    public void StartFreeGame()
    {
        SceneManager.LoadScene("FreeGameScene");
    }

}
