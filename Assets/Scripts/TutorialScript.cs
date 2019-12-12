using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Text currentText;
    public GameObject ghost;
    public GhostController gc;
    public GameObject cat;
    public GameObject person;

    // Start is called before the first frame update
    void Start()
    {
        gc = ghost.GetComponent<GhostController>();
        part1();
    }

    void part1()
    {
        currentText.text = "Press X to appear.";
        StartCoroutine(WaitForKey(KeyCode.X, part2));
    }

    void part2()
    {
        currentText.text = "Nice! You just unhaunted an object! Wanna explore a little?";
        StartCoroutine(pause(5, part3));
    }
    
    void part3()
    {
        cat.SetActive(true);
        currentText.text = "Look, a cat just wandered into your home! Why don't you spook it?";
        StartCoroutine(pause(3, part4));
    }

    void part4()
    {
        currentText.text = "Float up to the cat and press the spacebar.";
        StartCoroutine(WaitForKey(KeyCode.Space, part5));
    }

    void part5()
    {
        currentText.text = "Whoa, that was one scaredy cat! Good work! But it seemed like that cat was already scared of something... We should try scaring people!";
        StartCoroutine(pause(5, part6));
    }

    void part6()
    {
        person.SetActive(true);
        person.GetComponent<Person>().Scare(60);
        currentText.text = "How convenient, someone just walked in! They don't seem too afraid yet, but you can fix that. Float up to the TV and press X.";
        StartCoroutine(WaitForKey(KeyCode.X, part7));
    }

    void part7()
    {

    }

    IEnumerator WaitForKey(KeyCode kc, System.Action func)
    { 
        while (!Input.GetKeyDown(kc))
        {
            yield return null;
        }

        func();
    }

    IEnumerator pause(float sec, System.Action func)
    {
        yield return new WaitForSeconds(sec);
        func();
    }

}
