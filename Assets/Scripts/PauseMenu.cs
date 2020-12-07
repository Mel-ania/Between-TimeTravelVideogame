using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [SerializeField] private GameObject _continue = null;

    // reload the scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // load the home screen without saving progress
    public void GoHome()
    {
        Destroy(GameObject.Find("Music Player"));
        SceneManager.LoadScene("Home");
    }

    // freeze or defreeze the game
    public void OnPause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // when the player dies, the game freeze and it's not possible
    // to continue the game, just restart or go to home
    public void DeadPlayer()
    {
        gameObject.SetActive(true);
        OnPause(true);
        _continue.gameObject.SetActive(false);
    }
}
