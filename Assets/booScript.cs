using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booScript : MonoBehaviour
{
    public AudioSource booSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            booSound.Play();
        }
    }
}
