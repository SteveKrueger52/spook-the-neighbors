using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField]
    float MinX, MaxX, speed, InitWait;
    float X, Y,WaitTime;

    List<GameObject> TriggeredObjects;

    Vector2 MoveSpot;

    bool TargetReached;
 
    

    public bool isInvestigating;

    public ObjectTrigger objecttriggerscript;

    // Start is called before the first frame update
    void Start()
    {
        Y = transform.position.y;
        X = Random.Range(MinX, MaxX);
        isInvestigating = false;
        TriggeredObjects = new List<GameObject>();
        TargetReached = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TriggeredObjects.Count > 0)
        {
            isInvestigating = true;
        }
        if(TriggeredObjects.Count <= 0)
        {
            isInvestigating = false;
        }
        

        if (!isInvestigating)
        {
            Patrol();
        }

        if(isInvestigating)
        {
            Investigate();
        }

     //   Debug.Log(TriggeredObjects.Count);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "InteractableObject")
        {
            

            objecttriggerscript = collision.gameObject.GetComponent<ObjectTrigger>();

            if(objecttriggerscript.isTriggered)
            {
                Debug.Log(collision.gameObject.name + " triggered");
                TriggeredObjects.Add(collision.gameObject);
            }
            
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

    //void Investigate()
    //{
    //    if (TargetReached)
    //    {
    //        foreach (GameObject item in TriggeredObjects)
    //        {
    //            TargetReached = false;
    //            MoveSpot = new Vector2(item.transform.position.x, Y);
    //            transform.position = Vector2.MoveTowards(transform.position, MoveSpot, speed * Time.deltaTime);
    //            if (Vector2.Distance(transform.position, MoveSpot) < 0.2f)
    //            {
    //                TargetReached = true;
    //            }
    //        }
    //    }

        
    //    TriggeredObjects = new List<GameObject>();
        
        

    //}
}
