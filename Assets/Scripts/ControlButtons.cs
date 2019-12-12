using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{

    public Button ui_up;
    public Button ui_left;
    public Button ui_down;
    public Button ui_right;

    private Text cText;
    private Text xText;
    private Text spaceText;
    private Text zText;

    //public GameObject ghost;
    //private GhostController gc;

    // Start is called before the first frame update
    void Start()
    {
        //gc = ghost.GetComponent<GhostController>();
        cText = GameObject.FindGameObjectWithTag("CText").GetComponent<Text>();
        xText = GameObject.FindGameObjectWithTag("XText").GetComponent<Text>();
        spaceText = GameObject.FindGameObjectWithTag("SpaceText").GetComponent<Text>();
        zText = GameObject.FindGameObjectWithTag("ZText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (String.IsNullOrEmpty(cText.text))
        {
            DeactivateButton(ui_up);
        }

        else if (!String.IsNullOrEmpty(cText.text))
        {
            ActivateButton(ui_up);
        }

        if (String.IsNullOrEmpty(xText.text))
        {
            DeactivateButton(ui_left);
        }

        else if (!String.IsNullOrEmpty(xText.text))
        {
            ActivateButton(ui_left);
        }

        if (String.IsNullOrEmpty(spaceText.text))
        {
            DeactivateButton(ui_down);
        }

        else if (!String.IsNullOrEmpty(spaceText.text))
        {
            ActivateButton(ui_down);
        }

        if (String.IsNullOrEmpty(zText.text))
        {
            DeactivateButton(ui_right);
        }

        else if (!String.IsNullOrEmpty(zText.text))
        {
            ActivateButton(ui_right);
        }
    }


    void ActivateButton(Button b)
    {
        b.interactable = true;
        b.transform.GetChild(0).gameObject.SetActive(true);
    }

    void DeactivateButton(Button b)
    {
        b.interactable = false;
        b.transform.GetChild(0).gameObject.SetActive(false);
    }
}
