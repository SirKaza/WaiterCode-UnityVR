using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabberIndicatorGeneratoring : MonoBehaviour
{
    //Lista de colores del material actual del objecto (sin highlight)
    List<Color> actualMaterial;

    //Variables que nos permiten saber si el estado ha cambiado o si se ha recogido un objeto. 
    bool isChanged = false;
    bool isGrabbed = false;

    //Cada uno de los Grabbers asociados a las manos, tanto izquierda como derecha. 
    OVRGrabber hand_left_grabber;
    OVRGrabber hand_right_grabber;

    void Start()
    {
        actualMaterial = new List <Color>();
        //Recorremos cada hijo e incluimos su material en la lista de materiales actuales. 
        foreach (Transform child in this.gameObject.transform)
        {
            this.actualMaterial.Add(child.gameObject.GetComponent<Renderer>().material.color);
        }
        hand_left_grabber = GameObject.Find("CustomHandLeft").GetComponent<OVRGrabber>();
        hand_right_grabber = GameObject.Find("CustomHandRight").GetComponent<OVRGrabber>();
    }
    

    private void OnTriggerEnter(Collider collision)
    {
        // Si actualmente existe un objeto que tiene highlight devolvemos el actual a su estado natural. 
        // Esto tiene sentido?
       

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
        // Para cada hijo del actual le cambiamos a negro (highlight)
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
        //Activamos la flag de que se ha producido un cambio en el color
        isChanged = true;
        //Determinamos que el nuevo objeto seleciconado es este. 
        UndoManager.current.setHighlightedObject(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        //Cuando salgamos con la mano normal, recorreremos los hijos para devolverlo a su estado natural. 
        var i = 0;
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.GetComponent<Renderer>().material.color = this.actualMaterial[i];
            i += 1;
        }
        // Desactivaremos la flag para marcar que ya no hay ningún cambio
        isChanged = false;
        // Definiremos el highlight a null hasta encontrar una nuevo.
        UndoManager.current.setHighlightedObject(null);
     
    }

    // Update is called once per frame
    void Update()
    {
        // En el caso que se haya producido un cambio, es decir que el objeto está seleccionado. 
        if (isChanged)
        {
            //Miramos si alguna de las manos ha activado la flag de generar objetos. 
            isGrabbed = hand_left_grabber.isGeneratorIng || hand_right_grabber.isGeneratorIng;

            // Si alguna lo ha hecho, diferenciamos cual y restamos la cardinalidad actual de ese ingrediente. 
            if (isGrabbed)
            {
                if (hand_left_grabber.isGeneratorIng)
                {
                    if (!controladorPartidaChef.current.isComandaShowingUp || controladorPartidaChef.current.getCardinalityController().getCardinality(hand_left_grabber.grabbedObject.gameObject.name) <= 0)
                    {
                        Destroy(hand_left_grabber.grabbedObject.gameObject);

                    }
                    else
                    {
                        controladorPartidaChef.current.getCardinalityController().RestCardinality(hand_left_grabber.grabbedObject.gameObject.name);

                    }

                }
                else
                {
                    if (!controladorPartidaChef.current.isComandaShowingUp || controladorPartidaChef.current.getCardinalityController().getCardinality(hand_right_grabber.grabbedObject.gameObject.name) <= 0)
                    {
                        Destroy(hand_right_grabber.grabbedObject.gameObject);
                    }
                    else
                    {
                        controladorPartidaChef.current.getCardinalityController().RestCardinality(hand_right_grabber.grabbedObject.gameObject.name);

                    }


                }

                //Devolvemos el generador a su estado natural. 
                var i = 0;
                foreach (Transform child in this.gameObject.transform)
                {
                    child.gameObject.GetComponent<Renderer>().material.color = this.actualMaterial[i];
                    i += 1;
                }
                //Definimos que ya no hay cambios y seteamos el highlight a null. 
                isChanged = false;

                UndoManager.current.setHighlightedObject(null);

            }

        }
    }
}
