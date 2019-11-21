﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour
{

    [SerializeField]
    float MinX, MaxX, WaitTime, InitWait;

    Vector2 MoveSpot, Target;
    float GhostToDoor, PersonToDoor;

    HauntableObject HauntableScript;

    float X, Y;

    public GameObject StatusPanel;

    public Text StatusText;

    GhostController _ghostControllerScript;


    

    public float speed;
    public float fear = 0;
    public float maxFear = 100;
    public string status = "idle"; // "idle" "investigate" or "gtfo"
                                   //probably need some kind of "last heard noise" variable for behaviors to use

    public List<GameObject> exits = new List<GameObject>();
    public List<GameObject> Doors = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        exits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Exit"));
        Doors = new List<GameObject>(GameObject.FindGameObjectsWithTag("Door"));
        Y = transform.position.y;
        X = Random.Range(MinX, MaxX);
    }

    // Update is called once per frame
    void Update()
    {
        if (status == "idle")
        {
            Patrol();
            StatusPanel.SetActive(false);
            //idle behavior here
        } else if (status == "investigate")
        {
           
            StatusPanel.SetActive(true);
            StatusText.text = "?";
            Investigate();
            
        } else if (status == "gtfo")
        {
            StatusPanel.SetActive(true);
            StatusText.text = "!";
            //gtfo behvaior here

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "HauntableObject")
        {
            HauntableScript = collision.gameObject.GetComponent<HauntableObject>();

            if (HauntableScript.isTriggered)
            {
                //StopWalking = true;
                status = "investigate";

            }
        }

        Target = collision.gameObject.transform.position;

    }

    public void Scare(float fright)
    {
        fear += fright;
        if (fear > maxFear)
            fear = maxFear;
    }

    public GameObject GetClosestExit() //this finds closest exit to person on the screen, not necessarily exit with shortest path
    {
        exits.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector2.Distance(this.transform.position, a.transform.position)
            .CompareTo(
              Vector2.Distance(this.transform.position, b.transform.position));
        });
        return exits[0];
    }

    public GameObject GetClosestDoor() //this finds closest exit to person on the screen, not necessarily exit with shortest path
    {
        Doors.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector2.Distance(this.transform.position, a.transform.position)
            .CompareTo(
              Vector2.Distance(this.transform.position, b.transform.position));
        });

        GhostToDoor = Vector2.Distance(_ghostControllerScript.transform.position, Doors[0].transform.position);
        PersonToDoor = Vector2.Distance(transform.position, Doors[0].transform.position);

        if (GhostToDoor < PersonToDoor)
        {
            return Doors[1];
        }

        else
        {
            return Doors[0];
        }
    }


    void Patrol()
    {
        MoveSpot = new Vector2(X, Y);
        transform.position = Vector2.MoveTowards(transform.position, MoveSpot, speed * Time.deltaTime);


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

        if (fear <= 50)
        {
            MoveSpot = new Vector2(Target.x, Y);
            transform.position = Vector2.MoveTowards(transform.position, MoveSpot, speed * Time.deltaTime);



            if ((Vector2.Distance(transform.position, MoveSpot)) < 0.2f)
            {

                HauntableScript.isTriggered = false;
                status = "idle";

            }
        }

        else if (fear > 50 && fear <= 75 )
        {
            MoveSpot = new Vector2(GetClosestDoor().transform.position.x, Y);
            transform.position = Vector2.MoveTowards(transform.position, MoveSpot, speed * Time.deltaTime);



            if ((Vector2.Distance(transform.position, MoveSpot)) < 0.2f)
            {

                //HauntableScript.isTriggered = false;
                status = "idle";

            }
        }

    }

}
