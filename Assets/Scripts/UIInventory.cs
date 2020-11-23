﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Player player = null;

    private Transform container;
    private Transform keyTemplate;
    private Transform dicesTemplate;

    private void Awake()
    {
        container = transform.Find("Container");
        keyTemplate = container.Find("KeyTemplate");
        keyTemplate.gameObject.SetActive(false);
        dicesTemplate = container.Find("DicesTemplate");
        dicesTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        player.OnInventoryChanged += ButtonHolder_OnButtonsChanged;
    }

    private void ButtonHolder_OnButtonsChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // delete the old inventory
        foreach(Transform child in container)
        {
            if (child != keyTemplate && child != dicesTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        // create the new keys inventory and set the right color
        List<Key> keyList = player.KeyList;
        int i;
        for (i = 0; i < keyList.Count; i++)
        {
            Key key = keyList[i];
            Key.KeyType keyType = key.IsKeyType;
            Transform keyTransform = Instantiate(keyTemplate, container);
            keyTransform.gameObject.SetActive(true);
            keyTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50 * i, 0);
            Image keyImage = keyTransform.Find("Base").GetComponent<Image>();
            switch (keyType)
            {
                default:
                case Key.KeyType.Red:     
                    keyImage.color = Color.red;     
                    break;
                case Key.KeyType.Green:   
                    keyImage.color = Color.green;   
                    break;
            }
        }

        //create the new collectibles inventory
        Transform dicesTransform = Instantiate(dicesTemplate, container);
        dicesTransform.gameObject.SetActive(true);
        dicesTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50 * i, 0);
        TextMeshPro dicesNumber = dicesTransform.Find("Number").GetComponent<TextMeshPro>();
        dicesNumber.text = player.DicesNumber.ToString();
        
    }
}

