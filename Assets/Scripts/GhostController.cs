using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float speed;
    public List<GameObject> hauntables = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        hauntables = new List<GameObject>(GameObject.FindGameObjectsWithTag("HauntableObject"));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        delta = delta.magnitude > 1 ? delta.normalized : delta;
        transform.localPosition += new Vector3(delta.x, delta.y, 0) * speed;
        if (Input.GetButtonDown("Boo"))
            boo();
        if (Input.GetButtonDown("Hide"))
        {
            //hide
        }

        if (Input.GetButtonUp("Hide"))
        {
            //unhide
        }

        hauntables.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector2.Distance(this.transform.position, a.transform.position)
            .CompareTo(
              Vector2.Distance(this.transform.position, b.transform.position));
        });
        foreach (GameObject i in hauntables)
        {
            HauntableObject obj = i.GetComponent<HauntableObject>();
            obj.isHighlighted = false;
        }
        if (Vector2.Distance(this.transform.position, hauntables[0].transform.position) < 5) //5 is arbitrary range, requires ingame testing
            hauntables[0].GetComponent<HauntableObject>().isHighlighted = true;
    }

    void boo()
    {
        //boo!
        Instantiate(new ObjectTrigger(), this.transform);
        GameObject[] people = GameObject.FindGameObjectsWithTag("Person");
        foreach (GameObject target in people)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 5)//5 is arbitrary range, requires ingame testing
            {
                target.GetComponent<Person>().status = "gtfo";
            }
        }
    }
}
