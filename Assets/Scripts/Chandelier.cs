using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier : HauntableObject
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnBoo()
    {
        //fall and scare
        Unhaunt();
        gameObject.SetActive(false);
    }
}
