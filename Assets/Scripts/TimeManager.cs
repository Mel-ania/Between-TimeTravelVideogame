using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Manager
{
    [SerializeField] private List<Item> itemList = new List<Item>();
    [SerializeField] private List<DangerousObject> dangerList = new List<DangerousObject>();
    [SerializeField] private Transform timeView = null;

    private Color pastColor;
    private Color presentColor;

    private Image filter;
    private Image present;
    private Image past;

    private bool isPresent = true;

    // property
    public bool IsPresent
    {
        get
        {
            return isPresent;
        }
    }

    private void Awake()
    {
        Transform currentTime = timeView.Find("CurrentTime");
        pastColor = new Color(0.913f, 0.741f, 0.175f, 0.08f);
        presentColor = new Color(0.913f, 0.741f, 0.175f, 0f);
        filter = timeView.GetComponent<Image>();
        present = currentTime.Find("Present").GetComponent<Image>();
        past = currentTime.Find("Past").GetComponent<Image>();
    }

    // change time from present to past or past to present (according to
    // the current time), call the function that change the postiion
    // of the item in the scene and the function that turn on or off
    // the dangerous objects
    public void ChangeTime()
    {
        isPresent = !isPresent;
        ChangeFilter();
        foreach (Item item in itemList)
        {
            if (!isPresent)
            {
                item.gameObject.SetActive(true);
            }
            item.ResetPosition();
        }
        foreach (DangerousObject danger in dangerList){
            danger.TurnOnOff();
        }
    }

    // change the color of the filter according to the current time
    // and active the right text
    private void ChangeFilter()
    {
        if (!isPresent)
        {
            filter.color = pastColor;
            present.gameObject.SetActive(false);
            past.gameObject.SetActive(true);
        }
        else
        {
            filter.color = presentColor;
            past.gameObject.SetActive(false);
            present.gameObject.SetActive(true);
        }
    }
}
