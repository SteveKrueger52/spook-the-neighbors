using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public float fear = 0;
    public float maxFear = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scare(float fright)
    {
        fear += fright;
        if (fear > maxFear)
            fear = maxFear;
    }
}
