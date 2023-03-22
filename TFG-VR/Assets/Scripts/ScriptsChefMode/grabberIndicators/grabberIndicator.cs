using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabberIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    Color actualMaterial;
    bool isGrabbed;
    bool isChanged = false;
    void Start()
    {
        this.actualMaterial = this.gameObject.GetComponent<Renderer>().material.color;
        if (this.tag != "dish")
        {
            this.transform.GetChild(0).gameObject.SetActive(false);

        }
    }

    public void setActualMaterial(Color color)
    {
        this.actualMaterial = color;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "hand")
        {
            if (UndoManager.current.getHighlightedObject())
            {
                //Buscar cual es el más cercano. 
                var go_high = UndoManager.current.getHighlightedObject();
                go_high.gameObject.GetComponent<Renderer>().material.color = go_high.gameObject.GetComponent<grabberIndicator>().actualMaterial;
            }
            this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            isChanged = true;
            UndoManager.current.setHighlightedObject(this.gameObject);

            if (this.gameObject.tag == "dish")
            {
                foreach (Transform child in this.gameObject.transform)
                {
                    child.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }
            }
        }   

    }

   
    private void OnTriggerExit(Collider collision)
    {

        if (collision.tag == "hand")
        {
            this.gameObject.GetComponent<Renderer>().material.color = actualMaterial;
            isChanged = false;
            UndoManager.current.setHighlightedObject(null);
            if (this.gameObject.tag == "dish")
            {
                foreach (Transform child in this.gameObject.transform)
                {
                    child.gameObject.GetComponent<Renderer>().material.color = child.gameObject.GetComponent<grabberIndicator>().actualMaterial;
                }
            }
        }

     
    }

    


    // Update is called once per frame
    void Update()
    {
        isGrabbed = this.gameObject.GetComponent<OVRGrabbable>().isGrabbed;

        if (isChanged && this.gameObject.tag != "GeneratorIng")
        {

            if (isGrabbed)
            {
                if (this.gameObject.tag == "dish")
                {
                    foreach (Transform child in this.gameObject.transform)
                    {
                        child.gameObject.GetComponent<Renderer>().material.color = child.gameObject.GetComponent<grabberIndicator>().actualMaterial;
                    }
                }
                this.gameObject.GetComponent<Renderer>().material.color = actualMaterial;
                isChanged = false;

            }
           
        }

      
       
        
    }
}
