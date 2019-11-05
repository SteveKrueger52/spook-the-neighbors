﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField]
    float MinX, MaxX, speed, InitWait;
    float X, Y, WaitTime;

    public Person PersonScript;

    GameObject TriggeredGameobject;

    Vector2 MoveSpot, Target;

  

    public ObjectTrigger objecttriggerscript;

    // Start is called before the first frame update
    void Start()
    {
        Y = transform.position.y;
        X = Random.Range(MinX, MaxX);
       

        PersonScript = GetComponent<Person>();
     
    }

    // Update is called once per frame
    void Update()
    {



        if (PersonScript.status == "idle")
        {
           Patrol();
        }

        if (PersonScript.status == "investigate")
        {
            Investigate();
        }

        transform.position = Vector2.MoveTowards(transform.position, MoveSpot, speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InteractableObject" )
        {
            objecttriggerscript = collision.gameObject.GetComponent<ObjectTrigger>();

            if (objecttriggerscript.isTriggered)
            {
                
                PersonScript.status = "investigate";

            }
        }
        TriggeredGameobject = objecttriggerscript.gameObject;
        Target = collision.gameObject.transform.position;
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InteractableObject")
        {
            objecttriggerscript = collision.gameObject.GetComponent<ObjectTrigger>();

            if (objecttriggerscript.isTriggered)
            {

                PersonScript.status = "investigate";


            }
        }
        TriggeredGameobject = objecttriggerscript.gameObject;
        Target = collision.gameObject.transform.position;
    }



    void Patrol()
    {
        MoveSpot = new Vector2(X, Y);
        

        if ((Vector2.Distance(transform.position, MoveSpot)) < 0.2f)
        {
            if (WaitTime <= 0)
            {
                X = Random.Range(MinX, MaxX);
                WaitTime = InitWait;
            }

            else
            {
                WaitTime -= Time.deltaTime;

            }
        }
    }

    void Investigate()
    {
        MoveSpot = new Vector2(Target.x, Y);

        Debug.Log(TriggeredGameobject);

        if ((Vector2.Distance(transform.position, MoveSpot)) < 0.2f)
        {
            objecttriggerscript.isTriggered = false;
            StartCoroutine(WaitForReach());

        }

    }

    IEnumerator WaitForReach()
    {
        yield return new WaitForSeconds(5f);
        PersonScript.status = "idle";

    }

}
