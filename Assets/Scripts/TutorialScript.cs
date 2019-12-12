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
        //Instantiate(cat, new Vector3(Random.Range(1, 10), 0, 0), Quaternion.identity);
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
        currentText.text = "Whoa, that was one scaredy cat! Good work! But it seemed like that cat was already scared of something...";
        StartCoroutine(pause(4, part6));
    }

    void part6()
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
