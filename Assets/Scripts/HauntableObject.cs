using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HauntableObject : MonoBehaviour
{
    public bool isHaunted = false;
    public bool isHighlighted = false;
    public bool isTriggered = false;
    public float speed = 0;
    public GameObject ghost;
    public Color highlight_color = new Color(0,255,0,255);
    

    public string AText = "";
    public string BText = "";
    public string YText = "";
    public string XText = "";

    public virtual void OnInteract() { }
    public virtual void OnStart() { }
    public virtual void OnBoo() { }
    public virtual void OnHide() { }
    public virtual void Investigate() { }

   // private ObjectTrigger triggerscript;

    // Start is called before the first frame update
    void Start()
    {
        ghost = GameObject.Find("Ghost");
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighted)
        {
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            renderer.color = highlight_color;
        }
        else
        {
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            renderer.color = new Color(255,255,255,255);
        }
            
            
        if (isHighlighted && !isHaunted)
        {
            //Debug.Log("test");
            if (Input.GetButtonDown("Haunt"))
            {


                // Need a way to haunt exclusively one object, and toggle between stacked objects for selection
                //Debug.Log("haunt");
                Haunt();
            }
        } else if (isHaunted)
        {
            if (Input.GetButtonDown("Haunt"))
                Unhaunt();

            if (Input.GetButtonDown("Interact"))
                OnInteract();

            if (Input.GetButtonDown("Boo"))
                OnBoo();

            if (Input.GetButtonDown("Hide"))
                OnHide();

            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localPosition += new Vector3(1, 0, 0) * speed;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localPosition += new Vector3(-1, 0, 0) * speed;
            }
        }
    }

    public void Haunt()
    {
        isHaunted = true;
        ghost.SetActive(false);
    }

    public void Unhaunt()
    {
        isHaunted = false;
        ghost.transform.position = this.gameObject.transform.position;
        ghost.SetActive(true);
    }
}
