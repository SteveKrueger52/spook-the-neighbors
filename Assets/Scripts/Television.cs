using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class Television : HauntableObject
{
    
    // Start is called before the first frame update
    public override void OnStart()
    {
       
    }

    public override void OnBoo()
    {
        //isTriggered = true;
            //Instantiate(new ObjectTrigger(), this.transform);
            this.isTriggered = !this.isTriggered;

            GameObject[] people = GameObject.FindGameObjectsWithTag("Person");
            foreach (GameObject target in people)
            {
                float distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance < 5)//5 is arbitrary range, requires ingame testing
                {
                    //target.GetComponent<Person>().TriggerFunction(this.gameObject);

                    target.GetComponent<Person>().Scare(20,this.name);
                }
            }
        
    }
}
