using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public Button playButton;
    public Button controlsButton;
    public Button creditsButton;
    public Button quitButton;

    public GameObject controlsMenu;
    private bool controlsButtonPressed = false;

    public GameObject creditsMenu;
    private bool creditsButtonPressed = false;

    private void Start()
    {
        Button playBtn = playButton.GetComponent<Button>();
        Button controlsBtn = controlsButton.GetComponent<Button>();
        Button creditsBtn = creditsButton.GetComponent<Button>();
        Button quitBtn = quitButton.GetComponent<Button>();

        playBtn.onClick.AddListener(play);
        controlsBtn.onClick.AddListener(controls);
        creditsBtn.onClick.AddListener(credits);
        quitBtn.onClick.AddListener(quit);

    }


    public void play()
    {
        SceneManager.LoadScene("GameDemo");
    }

    public void controls()
    {
        if (!controlsButtonPressed)
        {
            controlsMenu.SetActive(true);
            controlsButtonPressed = true;
            StartCoroutine(FadeIn(controlsMenu));
        }

        else
        {
            StartCoroutine(FadeOut(controlsMenu));
            controlsButtonPressed = false;
        }
    }

    public void credits()
    {
        if (!creditsButtonPressed)
        {
            creditsMenu.SetActive(true);
            creditsButtonPressed = true;
            StartCoroutine(FadeIn(creditsMenu));
        }

        else
        {
            StartCoroutine(FadeOut(creditsMenu));
            creditsButtonPressed = false;
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    IEnumerator FadeIn(GameObject menu)
    {
        float secondsPassed = 0;
        while (secondsPassed < 2f)
        {
            secondsPassed += Time.deltaTime;
            float blend = Mathf.Clamp01(secondsPassed / 2f);
            menu.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, blend);
            yield return null;
        }
       
    }

    IEnumerator FadeOut(GameObject menu)
    {
        float secondsPassed = 0;
        while (secondsPassed < 2f)
        {
            secondsPassed += Time.deltaTime;
            float blend = Mathf.Clamp01(secondsPassed / 2f);
            menu.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, blend);
            yield return null;
        }

        menu.SetActive(false);
    }

}
