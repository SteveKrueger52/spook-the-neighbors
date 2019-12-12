using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    [SerializeField]
    float MinX, MaxX, WaitTime, InitWait;

    Vector2 MoveSpot, Target;

    HauntableObject HauntableScript;

    float X, Y;

    public float speed, RunSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Y = transform.position.y;
        X = Random.Range(MinX, MaxX);
        

    }

    // Update is called once per frame
    void Update()
    {
        idle();
    }

    void idle()
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
}
