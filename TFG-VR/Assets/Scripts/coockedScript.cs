using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coockedScript : MonoBehaviour
{
    private bool isCoocked = false;
    private bool activateCollision = false;
    private double timeCoocked = 2.0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activateCollision)
        {
            timeCoocked -= Time.deltaTime;
            if (timeCoocked < 0)
            {
                this.isCoocked = true;
                Color coocked_color = new Color(0.5f, 0.25f, 0);
                this.gameObject.GetComponent<Renderer>().material.color = coocked_color;
                this.gameObject.GetComponent<grabberIndicator>().setActualMaterial(coocked_color);
                this.gameObject.name = "CoockedHamburguer";
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "plancha")
        {
            this.activateCollision = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "plancha")
        {
            this.activateCollision = false;
        }
    }

    bool getStateCoocked()
    {
        return this.isCoocked;
    }
}
