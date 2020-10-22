using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> itemList = new List<Item>();

    [SerializeField]
    private Transform filter = null;

    private Color pastColor;
    private Color presentColor;

    private TextMeshProUGUI text;

    private Image image;

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
        Transform currentTime = filter.Find("CurrentTime");
        pastColor = new Color(0.913f, 0.741f, 0.175f, 0.08f);
        presentColor = new Color(0.913f, 0.741f, 0.175f, 0f);
        text = currentTime.Find("Text").GetComponent<TextMeshProUGUI>();
        image = filter.GetComponent<Image>();
    }

    // change time from present to past or past to present (according to
    // the current time) and call the function that change the postiion
    // of the item in the scene
    public void ChangeTime()
    {
        isPresent = !isPresent;
        ChangeFilter();
        foreach (Item item in itemList)
        {
            item.ResetPosition();
        }
    }

    // change the color of the filter according to the current time
    private void ChangeFilter()
    {
        if (!isPresent)
        {
            image.color = pastColor;
            text.SetText("Past");
        }
        else
        {
            image.color = presentColor;
            text.SetText("Present");
        }
    }
}
