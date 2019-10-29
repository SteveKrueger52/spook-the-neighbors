using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public float fear = 0;
    public float maxFear = 100;
    public string status = "idle"; // "idle" "investigate" or "gtfo"
    //probably need some kind of "last heard noise" variable for behaviors to use

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (status == "idle")
        {
            //idle behavior here
        } else if (status == "investigate")
        {
            //investigate behavior here
        } else if (status == "gtfo")
        {
            //gtfo behvaior here
        }
    }

    public void Scare(float fright)
    {
        fear += fright;
        if (fear > maxFear)
            fear = maxFear;
    }
}
