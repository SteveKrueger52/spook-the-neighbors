using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : HauntableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
