using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : HauntableObject
{
    // Start is called before the first frame update
    AudioSource[] audios;
    private Animator anim;
    bool isPlaying = false;

    public override void OnStart()
    {
        anim = gameObject.GetComponent<Animator>();
        audios = GetComponents<AudioSource>();
        _renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnUnHaunt()
    {
        isPlaying = false;
        anim.SetBool("Active", false);
        audios[0].volume = 1;
        audios[1].volume = 0;
        //animate off
    }

    public override void OnBoo()
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            //animate on
            anim.SetBool("Active", true);
            audios[0].volume = 0;
            audios[1].volume = 1;
        }
        else
        {
            //animate off
            anim.SetBool("Active", false);
            audios[0].volume = 1;
            audios[1].volume = 0;
        }
        //isTriggered = true;
        //Instantiate(new ObjectTrigger(), this.transform);
        this.isTriggered = !this.isTriggered;

        GameObject[] people = GameObject.FindGameObjectsWithTag("Person");
        foreach (GameObject target in people)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 5)//5 is arbitrary range, requires ingame testing
            {
                target.GetComponent<Person>().Scare(20, this.name);
            }
        }
    }
}
