using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HauntableObject : MonoBehaviour
{
    public bool isHaunted = false;
    public bool isHighlighted = false;
    public bool isTriggered = false;
    public float speed = 0;
    //public ControlButtons buttons;
    private Text cText;
    private Text xText;
    private Text spaceText;
    private Text zText;

    private GameObject _ghost;
    public GameObject ghost
    {
        get
        {
            if (_ghost == null)
                _ghost = GameObject.Find("Ghost");
            return _ghost;
        }
        set { _ghost = value; }
    }


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
        //buttons = GameObject.FindGameObjectWithTag("ButtonPanel").GetComponent<ControlButtons>();
        cText = GameObject.FindGameObjectWithTag("CText").GetComponent<Text>();
        xText = GameObject.FindGameObjectWithTag("XText").GetComponent<Text>();
        spaceText = GameObject.FindGameObjectWithTag("SpaceText").GetComponent<Text>();
        zText = GameObject.FindGameObjectWithTag("ZText").GetComponent<Text>();

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

        cText.text = YText;
        xText.text = XText;
        spaceText.text = AText;
        zText.text = BText;

    }

    public void Unhaunt()
    {
        isHaunted = false;
        ghost.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -1);
        ghost.SetActive(true);

        cText.text = "";
        xText.text = "Haunt";
        spaceText.text = "Boo!";
        zText.text = "Hide";
    }
}
