﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : HauntableObject
{
    // Start is called before the first frame update
    AudioSource[] audios;
    bool isPlaying = false;

    public override void OnStart()
    {
        audios = GetComponents<AudioSource>();
    }

    public override void Unhaunt()
    {
        isHaunted = false;
        ghost.transform.position = this.gameObject.transform.position;
        ghost.SetActive(true);
        isPlaying = false;
        audios[0].volume = 1;
        audios[1].volume = 0;
    }

    public override void OnBoo()
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            audios[0].volume = 0;
            audios[1].volume = 1;
        }
        else
        {
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
                target.GetComponent<Person>().Scare(20);
            }
        }
    }
}
