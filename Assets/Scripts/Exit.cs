using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    int playersinscene;
    public GameObject wintext;
    // Start is called before the first frame update
    void Start()
    {
        playersinscene = 2;
        wintext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playersinscene <= 0)
        {
            wintext.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            playersinscene--;
            collision.gameObject.transform.parent.gameObject.SetActive(false);
        }

    }

}
