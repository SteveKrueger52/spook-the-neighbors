using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : HauntableObject
{
    bool toasted = false;
    private Animator anim;
    private AudioSource sound;
    
    // Start is called before the first frame update
    public override void OnStart()
    {
        sound = GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
    }

    public override void OnHaunt()
    {
        anim.SetBool("Haunted", isHaunted);
    }
    
    public override void Unhaunt()
    {
        isHaunted = false;
        ghost.transform.position = this.gameObject.transform.position;
        ghost.SetActive(true);
        anim.SetBool("Haunted", isHaunted);
    }
    
    public override void OnInteract()
    {
        if (!toasted)
        {
            isTriggered = !isTriggered;
            anim.SetBool("Triggered", isTriggered);
            sound.Play();
            
            //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            toasted = true;
            GameObject[] people = GameObject.FindGameObjectsWithTag("Person");
            foreach (GameObject target in people)
            {
                float distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance < 5)//5 is arbitrary range, requires ingame testing
                {
                    target.GetComponent<Person>().Scare(20);
                }
            }
        }

    }
}
