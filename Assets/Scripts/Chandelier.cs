using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier : HauntableObject
{
    private ObjectTrigger triggerscript;

    // Start is called before the first frame update
    public override void OnStart()
    {
        triggerscript = GetComponent<ObjectTrigger>();
    }

    public override void OnBoo()
    {

        triggerscript.isTriggered = !triggerscript.isTriggered;
        //fall
        Instantiate(new ObjectTrigger(), this.transform);
        GameObject[] people = GameObject.FindGameObjectsWithTag("Person");
        foreach (GameObject target in people)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 5)//5 is arbitrary range, requires ingame testing
            {
                target.GetComponent<Person>().Scare(20);
            }
        }
        Unhaunt();
        gameObject.SetActive(false);
    }
}
