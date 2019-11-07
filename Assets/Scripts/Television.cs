using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : HauntableObject
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
    }
}
