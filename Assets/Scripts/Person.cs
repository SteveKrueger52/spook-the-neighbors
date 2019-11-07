using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public float speed;
    public float fear = 0;
    public float maxFear = 100;
    public string status = "idle"; // "idle" "investigate" or "gtfo"
                                   //probably need some kind of "last heard noise" variable for behaviors to use

    public List<GameObject> exits = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
       // exits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Exit"));
    }

    // Update is called once per frame
    void Update()
    {
        if (status == "idle")
        {
            //idle behavior here
        } else if (status == "investigate")
        {
            //investigate behavior here
        } else if (status == "gtfo")
        {
            //gtfo behvaior here
        }
    }

    public void Scare(float fright)
    {
        fear += fright;
        if (fear > maxFear)
            fear = maxFear;
    }

    //public GameObject GetClosestExit() //this finds closest exit to person on the screen, not necessarily exit with shortest path
    //{
    //    exits.Sort(delegate (GameObject a, GameObject b)
    //    {
    //        return Vector2.Distance(this.transform.position, a.transform.position)
    //        .CompareTo(
    //          Vector2.Distance(this.transform.position, b.transform.position));
    //    });
    //    return exits[0];
    //}
    
}
