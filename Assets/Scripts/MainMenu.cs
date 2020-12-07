using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("SavedLevel") == 0)
        {
            PlayerPrefs.SetInt("SavedLevel", 1);
        }
        PlayerPrefs.SetInt("FinalLevel", 3);
    }

    // load a new game and reset the scores
    public void NewGame()
    {
        PlayerPrefs.SetInt("SavedLevel", 1);
        PlayerPrefs.SetInt("Dices", 0);
        SceneManager.LoadScene("Level 1");
    }

    // load the previous game
    public void Continue()
    {
        SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("SavedLevel"));
    }
}
