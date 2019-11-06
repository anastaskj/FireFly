using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;
    public void PlayGame()
    {
        panel.SetActive(false);
        if (this.tag == "MainMenu")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
