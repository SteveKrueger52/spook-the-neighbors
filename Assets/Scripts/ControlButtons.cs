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

    public GameObject ghost;
    private GhostController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = ghost.GetComponent<GhostController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (String.IsNullOrEmpty(ui_up.GetComponentInChildren<Text>().text))
        {
            DeactivateButton(ui_up);
        }

        else if (!String.IsNullOrEmpty(ui_up.GetComponentInChildren<Text>().text))
        {
            ActivateButton(ui_up);
        }

        if (String.IsNullOrEmpty(ui_left.GetComponentInChildren<Text>().text))
        {
            DeactivateButton(ui_left);
        }

        else if (!String.IsNullOrEmpty(ui_left.GetComponentInChildren<Text>().text))
        {
            ActivateButton(ui_left);
        }

        if (String.IsNullOrEmpty(ui_down.GetComponentInChildren<Text>().text))
        {
            DeactivateButton(ui_down);
        }

        else if (!String.IsNullOrEmpty(ui_down.GetComponentInChildren<Text>().text))
        {
            ActivateButton(ui_down);
        }

        if (String.IsNullOrEmpty(ui_right.GetComponentInChildren<Text>().text))
        {
            DeactivateButton(ui_right);
        }

        else if (!String.IsNullOrEmpty(ui_right.GetComponentInChildren<Text>().text))
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
