using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Menu()
    {
        EndScoreController.reset();
        SceneManager.LoadScene(0);
    }

    public static int LastLevel = 0;

    public static void lastLevel(int n)
    {
        LastLevel = n;
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(LastLevel);
    }
}
