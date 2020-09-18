using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonHolder : MonoBehaviour
{
    [SerializeField]
    private ButtonHolder buttonHolder;

    private Transform container;
    private Transform buttonTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        buttonTemplate = container.Find("buttonTemplate");
        buttonTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        buttonHolder.OnButtonsChanged += ButtonHolder_OnButtonsChanged;
    }

    private void ButtonHolder_OnButtonsChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child != buttonTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        List<Button> buttonList = buttonHolder.GetButtonList();
        for (int i = 0; i < buttonList.Count; i++)
        {
            Button.ButtonType buttonType = buttonList[i].GetButtonType();
            Transform buttonTransform = Instantiate(buttonTemplate, container);
            buttonTemplate.gameObject.SetActive(true);
            buttonTemplate.GetComponent<RectTransform>().anchoredPosition = new Vector2(50 * i, 0);
            Image buttonImage = buttonTransform.Find("image").GetComponent<Image>();
            switch (buttonType)
            {
                default:
                case Button.ButtonType.Red:     buttonImage.color = Color.red;     break;
                case Button.ButtonType.Green:   buttonImage.color = Color.green;   break;
            }
        }
    }

}

