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

    IEnumerator Crash()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Crash!");
        GameObject[] people = GameObject.FindGameObjectsWithTag("NPC");
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
        triggerscript.isTriggered = !triggerscript.isTriggered;
        new WaitForSeconds(1);
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //Instantiate(new ObjectTrigger(), this.transform);
        StartCoroutine(Crash());

    }
}
