using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class Television : HauntableObject
{
    private ObjectTrigger triggerscripttv;
    // Start is called before the first frame update
    public override void OnStart()
    {
        triggerscripttv = GetComponent<ObjectTrigger>();
    }

    public override void OnBoo()
    {
        triggerscripttv.isTriggered = !triggerscripttv.isTriggered;
        
        //Instantiate(new ObjectTrigger(), this.transform);
        GameObject[] people = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject target in people)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 5)//5 is arbitrary range, requires ingame testing
            {
                target.GetComponent<Person>().Scare(20);
            }
        }
    }
}
