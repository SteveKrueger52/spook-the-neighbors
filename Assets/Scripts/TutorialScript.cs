using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public Text currentText;
    public GameObject cat;
    public GameObject person;

    public GameObject tv;
    public GameObject piano;

    // Start is called before the first frame update
    void Start()
    {
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
        currentText.text = "Look, a cat just wandered into your home! Why don't you spook it? Float up to the cat and press the spacebar.";
        StartCoroutine(WaitForObjVanish(cat, part4));
    }

    void part4()
    {
        currentText.text = "Whoa, that was one scaredy cat! Good work! But it seemed like that cat was already scared of something... Wanna try scaring people?";
        StartCoroutine(pause(5, part6));
    }

    /*void part5()
    {
        moved some stuff around so there really isnt a part5 anymore      
    }

    */

    void part6()
    {
        person.SetActive(true);
        person.GetComponent<Person>().Scare(60);
        currentText.text = "How convenient, someone just walked in! They don't seem too afraid yet, but you can fix that. Float up to the TV and press X.";
        StartCoroutine(WaitForTVHaunt(tv, part7));
    }

    void part7()
    {
        currentText.text = "You just haunted the TV! Try moving around in it.";
        StartCoroutine(pause(5, part8));
    }

    void part8()
    {
        currentText.text = "Now try pressing the spacebar when you're close to them.";
        StartCoroutine(WaitForKey(KeyCode.Space, part9));
    }

    void part9()
    {
        currentText.text = "Nice, you scared them a little! But unlike the cat, this person needs to be scared more. You'll need to haunt another object to do that.";
        StartCoroutine(pause(5, part10));
    }

    void part10()
    {
        currentText.text = "Remember how to unhaunt? Just press X. Remember, the panel on the right shows you what you can do at any given time.";
        StartCoroutine(WaitForTVUnhaunt(tv, part11));
    }

    void part11()
    {
        currentText.text = "Great! Now let's find a new object to haunt... how about the piano?";
        StartCoroutine(WaitForPianoHaunt(piano, part12));
    }

    void part12()
    {
        currentText.text = "Now press the spacebar again, when the person's near you.";
        StartCoroutine(WaitForPersonScare(person, part13));
    }

    void part13()
    {
        currentText.text = "Cool, see how scared they are? Now to get them outta here; press X to unhaunt, and then press space to boo them one last time!";
        StartCoroutine(WaitForObjVanish(person, part14));
    }

    void part14()
    {
        currentText.text = "Look at them run! Now you know how to haunt and unhaunt, and how to scare people. You are now ready to Spook the Neighbors.";
        StartCoroutine(pause(5, StartGame));
    }

    void StartGame()
    {
        SceneManager.LoadScene("GameDemoCopy");
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

    IEnumerator WaitForObjVanish(GameObject go, System.Action func)
    {
        while (go.activeSelf)
        {
            yield return null; 
        }

        func();
    }

    IEnumerator WaitForTVHaunt(GameObject tv, System.Action func)
    {
        while (!tv.GetComponent<Television>().isHaunted)
        {
            yield return null;
        }

        func();
    }

    IEnumerator WaitForTVUnhaunt(GameObject tv, System.Action func)
    {
        while (tv.GetComponent<Television>().isHaunted)
        {
            yield return null;
        }

        func();
    }

    IEnumerator WaitForPianoHaunt(GameObject p, System.Action func)
    {
        while (!p.GetComponent<Piano>().isHaunted)
        {
            yield return null;
        }

        func();
    }

    IEnumerator WaitForPersonScare(GameObject p, System.Action func)
    {
        while (p.GetComponent<Person>().fear != 100)
        {
            yield return null;
        }

        func();
    }

}
