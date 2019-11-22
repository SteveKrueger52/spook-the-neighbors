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
        ActivateButton(ui_up);
        ActivateButton(ui_left);
        ActivateButton(ui_down);
        ActivateButton(ui_right);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (gc.hauntables[0].GetComponent<HauntableObject>().isHighlighted || gc.hauntables[0].GetComponent<HauntableObject>().isHaunted)
        {
            ActivateButton(ui_left);
        }

        else
        {
            DeactivateButton(ui_left);
        }
        */

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
