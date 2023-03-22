using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberIndicatorGenTutorial : MonoBehaviour
{// List of colors of the current material of the object (without highlighting)
    List<Color> actualMaterial;

    // Variables that allow us to know if the state has changed or if an object has been collected. 
    bool isChanged = false;
    bool isGrabbed = false;

    // Each of the Grabbers associated with the hands, both left and right.
    OVRGrabber hand_left_grabber;
    OVRGrabber hand_right_grabber;

    void Start()
    {
        actualMaterial = new List <Color>();
        // We walk through each child and include their material in the current supply list.
        foreach (Transform child in this.gameObject.transform)
        {
            this.actualMaterial.Add(child.gameObject.GetComponent<Renderer>().material.color);
        }
        hand_left_grabber = GameObject.Find("CustomHandLeft").GetComponent<OVRGrabber>();
        hand_right_grabber = GameObject.Find("CustomHandRight").GetComponent<OVRGrabber>();
    }
    

    private void OnTriggerEnter(Collider collision)
    {
        // If there is currently an object that has highlight, we return the current one to its natural state.

        if (UndoManager.current.getHighlightedObject())
        { 
            var go_high = UndoManager.current.getHighlightedObject();
            var i = 0;
            foreach (Transform child in this.gameObject.transform)
            {
                child.gameObject.GetComponent<Renderer>().material.color = this.actualMaterial[i];
                i += 1;
            }
        }
        // For each child of the current one we change it to black (highlight)
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
        // We activate the flag that there has been a change in color
        isChanged = true;
        // We determine that the new selected object is this.
        UndoManager.current.setHighlightedObject(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        // When we leave with the normal hand, we go through the children to return it to its natural state.
        var i = 0;
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.GetComponent<Renderer>().material.color = this.actualMaterial[i];
            i += 1;
        }
        // We will deactivate the flag to indicate that there is no longer any change
        isChanged = false;
        // We will define the highlight to null until we find a new one.
        UndoManager.current.setHighlightedObject(null);
     
    }

    // Update is called once per frame
    void Update()
    {
        // In the case that a change has occurred. The object is selected.
        if (isChanged)
        {
            // We check if any of the hands has activated the flag to generate objects.
            isGrabbed = hand_left_grabber.isGeneratorIng || hand_right_grabber.isGeneratorIng;

            // If one of them has, we differentiate which one and subtract the current cardinality of that ingredient. 
            if (isGrabbed)
            {
                if (this.gameObject.name == "DishesGenerator" && ControllerTutorial.current.getCurrentStep()==12){
                    ControllerTutorial.current.dishesSelected();
                }else if (this.gameObject.name == "BreadDOWNGenerator" && ControllerTutorial.current.getCurrentStep() == 13){
                    ControllerTutorial.current.breadDownSelected();
                }else if(this.gameObject.name == "HamburguerGenerator" && ControllerTutorial.current.getCurrentStep() == 15){
                    ControllerTutorial.current.hamburguerSelected();
                }else if(this.gameObject.name == "BreadUPGenerator" && ControllerTutorial.current.getCurrentStep() == 17){
                    ControllerTutorial.current.breadUPSelected();

                }
                else{
                    ControllerTutorial.current.isGenGrabbed();

                }
                if (hand_left_grabber.isGeneratorIng)
                {
                    if (ControllerTutorial.current.getCardinalityController().getCardinality(hand_left_grabber.grabbedObject.gameObject.name) <= 0)
                    {
                        Destroy(hand_left_grabber.grabbedObject.gameObject);

                    }
                    else
                    {
                        ControllerTutorial.current.getCardinalityController().RestCardinality(hand_left_grabber.grabbedObject.gameObject.name);

                    }

                }
                else
                {
                    if (ControllerTutorial.current.getCardinalityController().getCardinality(hand_right_grabber.grabbedObject.gameObject.name) <= 0)
                    {
                        Destroy(hand_right_grabber.grabbedObject.gameObject);
                    }
                    else
                    {
                        ControllerTutorial.current.getCardinalityController().RestCardinality(hand_right_grabber.grabbedObject.gameObject.name);

                    }


                }

                //Return the generator to its natural state
                var i = 0;
                foreach (Transform child in this.gameObject.transform)
                {
                    child.gameObject.GetComponent<Renderer>().material.color = this.actualMaterial[i];
                    i += 1;
                }
                isChanged = false;

                UndoManager.current.setHighlightedObject(null);

            }

        }
    }
}
