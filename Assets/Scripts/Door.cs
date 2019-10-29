using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Door : HauntableObject
{
    public bool isClosed = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHaunted)
        {

        }
    }

    public void Open()
    {
        isClosed = false;
    }

    void Slam()
    {
        // make noise
        isClosed = !isClosed;
    }
}
