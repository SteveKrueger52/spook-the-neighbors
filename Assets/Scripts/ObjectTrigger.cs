using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public  bool isTriggered;
    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        Invoke("Trigger", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Trigger()
    {
        isTriggered = true;
    }


}
