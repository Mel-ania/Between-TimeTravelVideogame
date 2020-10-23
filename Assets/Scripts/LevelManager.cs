using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Player player = null;

    public void NextLevel()
    {
        //SceneManager.LoadScene(0);
        player.gameObject.SetActive(false);
        Debug.Log("entrato");
    }
}
