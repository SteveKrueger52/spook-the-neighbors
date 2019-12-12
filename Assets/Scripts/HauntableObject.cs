﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HauntableObject : MonoBehaviour
{
    public bool isHaunted = false;
    public bool isHighlighted = false;
    public bool isTriggered = false;
    public float speed = 0;
    protected SpriteRenderer _renderer;

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
    public string XText = "Appear";

    public virtual void OnInteract() { }
    public virtual void OnStart() { }
    public virtual void OnBoo() { }
    public virtual void OnHide() { }
    public virtual void Investigate() { }
    public virtual void OnHaunt() { }
    public virtual void OnUnHaunt() { }

   // private ObjectTrigger triggerscript;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighted)
        {
            _renderer.color = highlight_color;
        }
        else
        {
            _renderer.color = new Color(255,255,255,255);
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
        OnHaunt();
    }

    public virtual void Unhaunt()
    {
        isHaunted = false;
        ghost.transform.position = this.gameObject.transform.position;
        ghost.SetActive(true);
    }
}
