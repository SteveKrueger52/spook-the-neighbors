using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float speed;
    public List<GameObject> hauntables = new List<GameObject>();
    public bool hidden = false;
    public float hauntRange = 5f;
    
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
            Color temp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(temp.r,temp.g,temp.b, 50);
            hidden = true;
        }

        if (Input.GetButtonUp("Hide"))
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(temp.r,temp.g,temp.b, 255);
            hidden = false;
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
        if (Vector2.Distance(this.transform.position, hauntables[0].transform.position) < hauntRange) //5 is arbitrary range, requires ingame testing
            hauntables[0].GetComponent<HauntableObject>().isHighlighted = true;
    }

    void boo()
    {
        //boo!
    }
}
