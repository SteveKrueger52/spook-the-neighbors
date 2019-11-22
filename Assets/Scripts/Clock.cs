using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : HauntableObject
{

    public override void OnStart()
    {
        this.Haunt();
        this.isHighlighted = true;
    }

    public override void OnInteract()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
