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

    public List<GameObject> exits = new List<GameObject>();

    public GameObject GhostObject;

    string Status;

    SpriteRenderer rendnew;



    // Start is called before the first frame update
    void Start()
    {
        Y = transform.position.y;
        X = Random.Range(MinX, MaxX);

        Status = "idle";

        exits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Exit"));

        rendnew = GetComponent<SpriteRenderer>();
        rendnew.color = new Color(1, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Status == "idle")
        {
            idle();
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(Vector2.Distance(GhostObject.transform.position, transform.position) <= 5)
            {
                Status = "gtfo";
            }

        }

        if(Status=="gtfo")
        {
            GTFO();
        }


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

    public void GTFO()
    {

        MoveSpot = new Vector2(GetClosestExit().transform.position.x,Y);
        transform.position = Vector2.MoveTowards(transform.position, MoveSpot, RunSpeed * Time.deltaTime);



        if ((Vector2.Distance(transform.position, MoveSpot)) < 0.2f)
        {

            gameObject.SetActive(false);

        }
    }
}
