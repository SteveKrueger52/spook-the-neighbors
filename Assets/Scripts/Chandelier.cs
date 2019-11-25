using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier : HauntableObject
{


    // Start is called before the first frame update
    public override void OnStart()
    {
       
    }

    IEnumerator Crash()
    {

        //isTriggered = true;
        //Instantiate(new ObjectTrigger(), this.transform);
        yield return new WaitForSeconds(1);

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

    public override void OnBoo()
    {
        //new WaitForSeconds(1);
        this.isTriggered = !this.isTriggered;
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //Instantiate(new ObjectTrigger(), this.transform);
        StartCoroutine(Crash());

    }
}
