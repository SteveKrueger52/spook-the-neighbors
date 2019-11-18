using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public  bool isTriggered;

    //public float time;
    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        //Invoke("Trigger", time);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.X))
        {
            isTriggered = !isTriggered;
        }
    }

    

}
