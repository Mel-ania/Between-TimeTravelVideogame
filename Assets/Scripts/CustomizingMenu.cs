using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizingMenu : Menu
{
    private Image hood;
    private Image body;
    private Image armLeft;
    private Image armRight;

    [SerializeField] private ColorManager cm = null;

    private void Awake()
    {
        hood     = GameObject.Find("Player/Hood").GetComponent<Image>();
        body     = GameObject.Find("Player/Body").GetComponent<Image>();
        armLeft  = GameObject.Find("Player/Arm left").GetComponent<Image>();
        armRight = GameObject.Find("Player/Arm right").GetComponent<Image>();
    }

    private void Start()
    {
        hood.color     = cm.IsPlayerColor;
        body.color     = cm.IsPlayerColor;
        armLeft.color  = cm.IsPlayerColor;
        armRight.color = cm.IsPlayerColor;
    }

    public void ChangePlayerColor(int color)
    {
        cm.SavePlayerColor(color);
        hood.color     = cm.IsPlayerColor;
        body.color     = cm.IsPlayerColor;
        armLeft.color  = cm.IsPlayerColor;
        armRight.color = cm.IsPlayerColor;
    }
}
