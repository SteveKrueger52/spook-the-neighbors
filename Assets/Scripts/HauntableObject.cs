using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HauntableObject : MonoBehaviour
{
    public bool isHaunted = false;
    public bool isHighlighted = false;
    public float speed = 0;
    public GameObject ghost;

    public virtual void OnInteract() { }
    public virtual void OnBoo() { }
    public virtual void OnHide() { }

    // Start is called before the first frame update
    void Start()
    {
        ghost = GameObject.Find("Ghost");
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighted && !isHaunted)
        {
            if (Input.GetButtonDown("Haunt"))
                Haunt();
        }

        if (isHaunted)
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
                transform.localPosition += new Vector3(1, 0, 0) * speed;
            else if (Input.GetAxis("Horizontal") < 0)
                transform.localPosition += new Vector3(-1, 0, 0) * speed;
        }
    }

    void Haunt()
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
