using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{

    public Button ui_y;
    public Button ui_x;
    public Button ui_a;
    public Button ui_b;

    public GameObject ghost;
    private GhostController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = ghost.GetComponent<GhostController>();
        DeactivateButton(ui_y);
        DeactivateButton(ui_x);
        DeactivateButton(ui_a);
        DeactivateButton(ui_b);
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.hauntables[0].GetComponent<HauntableObject>().isHighlighted)
        {
            ActivateButton(ui_x);
        }

        else
        {
            DeactivateButton(ui_x);
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
