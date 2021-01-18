using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public GameObject panel;
    public Button button;
    Animator panelAnimator, buttonAnimator;
    RectTransform rectTransform;
    static bool isOpen;

    void Start()
    {
        isOpen = false;
        panelAnimator = panel.GetComponent<Animator>();
        buttonAnimator = gameObject.GetComponent<Animator>();
        rectTransform = panel.GetComponent<RectTransform>();

        panel.SetActive(false);
    }

    public void ChangePanelState()
    {
        if (panel != null)
        {
                panel.SetActive(true);

                panelAnimator.SetBool("Open", !isOpen);
                buttonAnimator.SetBool("Open", !isOpen);

                isOpen = panelAnimator.GetBool("Open");

                ChangeButtonColor();
        }
    }

    public void OnDisable()
    {
        if (isOpen)
        {
            rectTransform.offsetMin = new Vector2(338.5098f, 32.77275f);
            rectTransform.offsetMax = new Vector2(-45.22975f, -338.8527f);
            isOpen = !isOpen;

            ChangeButtonColor();
        }
    }

    private void ChangeButtonColor()
    {
        if (isOpen)
        {
            button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            button.GetComponent<Image>().color = Color.black;
        }
    }
}
