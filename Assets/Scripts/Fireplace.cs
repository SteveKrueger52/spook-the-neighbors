using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : HauntableObject
{
    bool lit = true;
    // Start is called before the first frame update
    public override void OnStart()
    {

    }

    public override void OnInteract()
    {
        if (lit)
        {
            //animate
            this.isTriggered = !this.isTriggered;
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            lit = false;
            GameObject[] people = GameObject.FindGameObjectsWithTag("Person");
            foreach (GameObject target in people)
            {
                float distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance < 5)//5 is arbitrary range, requires ingame testing
                {
                    target.GetComponent<Person>().Scare(20,this.name);
                }
            }
        }

    }
}
