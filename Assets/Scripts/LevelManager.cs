using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Player player = null;

    //go to the next level and save the progress
    public void NextLevel()
    {
        player.gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt("FinalLevel"))
        {
            SceneManager.LoadScene("Home");
        }
        else
        {
            PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
