﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : MonoBehaviour
{
    public float speed;
    public List<GameObject> hauntables = new List<GameObject>();
    public bool hidden = false;
    public float hauntRange = 0.1f;
    public AudioSource booSound;
    



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
        {
            boo();
        }
        if (Input.GetButtonDown("Hide"))
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 0.5f);
            hidden = true;
        }

        if (Input.GetButtonUp("Hide"))
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 1);
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
        if (Vector2.Distance(this.transform.position, hauntables[0].transform.position) < hauntRange)
        {
            //if (this.GetComponent<BoxCollider2D>().Distance(hauntables[0].GetComponent<BoxCollider2D>()).distance < hauntRange)
            hauntables[0].GetComponent<HauntableObject>().isHighlighted = true;
            hauntables[0].GetComponent<HauntableObject>().xText.text = "Haunt";
        }

        else
        {
            hauntables[0].GetComponent<HauntableObject>().xText.text = "";
        }



    }

    void boo()
    {
        booSound.Play();
        //boo!
        // Instantiate(new ObjectTrigger(), this.transform);
        GameObject[] people = GameObject.FindGameObjectsWithTag("Person");
        foreach (GameObject target in people)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 5 && target.GetComponent<Person>().fear >= 100)//5 is arbitrary range, requires ingame testing/           
            {
                target.GetComponent<Person>().status = "gtfo";
            }
        }

        
                

        
    }
}
