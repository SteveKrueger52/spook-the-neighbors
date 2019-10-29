using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HauntableObject : MonoBehaviour
{
    public bool isHaunted = false;
    public bool isHighlighted = false;
    public float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighted && !isHaunted)
        {
            if (false) //haunt button
                Haunt();
        }

        if (isHaunted)
        {
            if (false) //haunt button
                Unhaunt();
            //all generic haunted controls and logic here
        }
    }

    void Haunt()
    {
        isHaunted = true;
        //kill ghost
    }

    void Unhaunt()
    {
        isHaunted = false;
        //instantiate ghost
    }
}
