using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private Player player = null;

    private Transform container;
    private Transform keyTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        keyTemplate = container.Find("buttonTemplate");
        keyTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        player.OnKeysChanged += ButtonHolder_OnButtonsChanged;
    }

    private void ButtonHolder_OnButtonsChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child != keyTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        List<Key> keyList = player.KeyList;
        for (int i = 0; i < keyList.Count; i++)
        {
            Key.KeyType keyType = keyList[i].IsKeyType;
            Transform keyTransform = Instantiate(keyTemplate, container);
            keyTemplate.gameObject.SetActive(true);
            keyTemplate.GetComponent<RectTransform>().anchoredPosition = new Vector2(50 * i, 0);
            Image keyImage = keyTransform.Find("image").GetComponent<Image>();
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
    }
}

