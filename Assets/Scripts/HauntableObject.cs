using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HauntableObject : MonoBehaviour
{
    public bool isHaunted = false;
    public bool isHighlighted = false;
    public float speed = 0;
    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        ghost = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighted && !isHaunted)
        {
            if (Input.GetButtonDown("Haunt"))
                Haunt();
        }

        if (isHaunted)
        {
            if (Input.GetButtonDown("Haunt"))
                Unhaunt();
            //all generic haunted controls and logic here
        }
    }

    void Haunt()
    {
        isHaunted = true;
        ghost.SetActive(false);
    }

    void Unhaunt()
    {
        isHaunted = false;
        ghost.transform.position = this.gameObject.transform.position;
        ghost.SetActive(true);
    }
}
