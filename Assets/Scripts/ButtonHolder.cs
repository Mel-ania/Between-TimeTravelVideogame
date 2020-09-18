using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHolder : MonoBehaviour
{
    public event EventHandler OnButtonsChanged;

    [SerializeField]
    private List<ItemPosition> itemPositionList;
    private List<Button> buttonList;

    private bool present = true;

    public List<Button> GetButtonList()
    {
        return buttonList;
    }

    private void Awake()
    {
        buttonList = new List<Button>();
    }

    private void Update()
    {
        if(ContainsAtLeastOne() && Input.GetKeyUp(KeyCode.T))
        {
            ChangeTime();
        }
    }

    public void AddButton(Button button)
    {
        buttonList.Add(button);
        OnButtonsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveOneButton(Button.ButtonType bt)
    {
        buttonList.Remove(FindButton(bt));
        OnButtonsChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool ContainsButton(Button.ButtonType bt)
    {
        foreach (Button button in buttonList)
        {
            if (button.GetButtonType() == bt)
            {
                return true;
            }
        }
        return false;
        
    }

    private Button FindButton(Button.ButtonType bt)
    {
        foreach (Button button in buttonList)
        {
            if (button.GetButtonType() == bt)
            {
                return button;
            }
        }
        return null;
    }

    private void ColorListGreen()
    {
        foreach(Button button in buttonList)
        {
            if (button.GetButtonType()==Button.ButtonType.Red)
            {
                button.ColorGreen();
            }
        }
    }

    public bool ContainsAtLeastOne()
    {
        return buttonList.Count > 0;
    }

    private void ChangeTime()
    {
        present = !present;
        foreach(ItemPosition itemPosition in itemPositionList)
        {
            itemPosition.TimeChanger(present);
        }
    }

    public bool TimePresent()
    {
        return present;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Button button = collision.GetComponent<Button>();
        if (button)
        {
            AddButton(button);
            Destroy(button.gameObject);
        }

        Door door = collision.GetComponent<Door>();
        if (door)
        {
            if (ContainsButton(Button.ButtonType.Green))
            {
                RemoveOneButton(Button.ButtonType.Green);
                door.OpenDoor();
            }
        }

        Transmitter transmitter = collision.GetComponent<Transmitter>();
        if (transmitter)
        {
            if (transmitter.Active())
            {
                if (ContainsButton(Button.ButtonType.Red))
                {
                    ColorListGreen();
                    OnButtonsChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
