using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Manager
{
    [SerializeField] private Player player = null;

    //go to the next level and save the progress
    public void NextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        player.gameObject.SetActive(false);

        if (index == PlayerPrefs.GetInt("FinalLevel"))
        {
            SceneManager.LoadScene("Home");
        }
        else
        {
            PlayerPrefs.SetInt("SavedLevel", index + 1);
            SceneManager.LoadScene(index + 1);
        }
    }
}
