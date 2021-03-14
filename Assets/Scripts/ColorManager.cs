using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : Manager
{
    private Color white  = new Color(1f, 1f, 1f, 1f);
    private Color yellow = new Color(1f, 0.85f, 0f, 1f);
    private Color red    = new Color(1f, 0.2f, 0f, 1f);
    private Color green  = new Color(0.25f, 0.99f, 0.008f, 1f);
    private Color blue   = new Color(0.008f, 0.76f, 0.99f, 1f);
    private Color purple = new Color(0.95f, 0f, 1f, 1f);
    private Color playerColor;

    // property
    public Color IsPlayerColor
    {
        get
        {
            switch (PlayerPrefs.GetInt("PlayerColor"))
            {
                default:
                case 0:
                    playerColor = white;
                    break;
                case 1:
                    playerColor = yellow;
                    break;
                case 2:
                    playerColor = red;
                    break;
                case 3:
                    playerColor = green;
                    break;
                case 4:
                    playerColor = blue;
                    break;
                case 5:
                    playerColor = purple;
                    break;
            }
            return playerColor;
        }
    }
    
    // save the preference for the player color
    public void SavePlayerColor(int color)
    {
        PlayerPrefs.SetInt("PlayerColor", color);
    }
}