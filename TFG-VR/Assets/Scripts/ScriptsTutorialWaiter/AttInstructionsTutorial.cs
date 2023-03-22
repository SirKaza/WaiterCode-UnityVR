using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class AttInstructionsTutorial : MonoBehaviour
{
    private OrderWaiter current_hamburguer;
    private float conditionalSeparationAxisY = 0.18f;

    // Start is called before the first frame update
    void Start()
    {
        current_hamburguer = new OrderWaiter();

        //pruebas
    }


    private void OnTriggerStay(Collider other)
    {

        //Detectamos los paneles que hemos soltado y aún no hemos colocado

        if (other.tag == "panel" && !other.GetComponent<OVRGrabbable>().isGrabbed && !other.GetComponent<panelScriptTutorial>().getIsInPanel())
        {

            if (current_hamburguer.getInstructions().Count == 0)
            {
                //Queremos la posicion encima del panel y debajo de la ultima instruccion que se ha añadido.
                //Si no hemos puesto ninguna instrucción, simplemente lo posicionamos arriba de todo del panel
                other.gameObject.transform.position = new Vector3(this.transform.position.x + other.transform.localScale.y, this.transform.position.y + 0.9f, this.transform.position.z);
                other.gameObject.transform.rotation = Quaternion.Euler(180, 180, 90);

                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.gameObject.GetComponent<panelScriptTutorial>().setIsInPanel(true);
                current_hamburguer.addInstruction(other.gameObject);
            }


            //Si el panel que estoy poniendo no lo coloco entre ninguna de los paneles ya existentes, se coloca justo debajo.
            else if (other.gameObject.transform.position.y < current_hamburguer.getLastInstruction().transform.position.y)
            {

                other.gameObject.transform.position = new Vector3(this.transform.position.x + other.transform.localScale.y, current_hamburguer.getLastInstruction().transform.position.y - conditionalSeparationAxisY, this.transform.position.z);

                other.gameObject.transform.rotation = Quaternion.Euler(180, 180, 90);

                //Congelamos la posición del panel. Si el freeze no está dentro del if/else se caen los paneles. 
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.gameObject.GetComponent<panelScriptTutorial>().setIsInPanel(true);

                current_hamburguer.addInstruction(other.gameObject);
                if (other.gameObject.GetComponent<panelScriptTutorial>().getItWasInPanel())
                {

                    reOrderFunction(other);
                }

                else
                {
                    if(controladorTutorialWaiter.current.getIterTutorial() > 6)
                    {
                        
                        controladorTutorialWaiter.current.createPanel();
                    }
                }


            }

            //En cualquier otro caso la instrucción estará entre otras dos ya existentes. 
            else
            {

                int i = 0;

                //Recorremos los ingredientes hasta que encontremos entre que dos paneles hemos puesto el nuevo panel
                while (!other.gameObject.GetComponent<panelScriptTutorial>().getIsInPanel() || i == current_hamburguer.getInstructions().Count - 1)
                {

                    if (other.gameObject.transform.position.y < ((GameObject)current_hamburguer.getInstructions()[i]).transform.position.y && other.gameObject.transform.position.y > ((GameObject)current_hamburguer.getInstructions()[i + 1]).transform.position.y)
                    {
                        other.gameObject.GetComponent<panelScriptTutorial>().setIsInPanel(true);

                        //Colocamos la nueva instruccion entre los dos paneles
                        other.gameObject.transform.position = new Vector3(this.transform.position.x + other.transform.localScale.y, ((GameObject)current_hamburguer.getInstructions()[i]).transform.position.y - other.transform.localScale.x, this.transform.position.z);

                        //Insertamos la instrucción a la lista justo delante de la que estamos obeservando en ese momento                       
                        current_hamburguer.insertInstruction(i + 1, other.gameObject);

                        other.gameObject.transform.rotation = Quaternion.Euler(180, 180, 90);
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        reOrderFunction(other);
                        if (!other.gameObject.GetComponent<panelScriptTutorial>().getItWasInPanel() && controladorTutorialWaiter.current.getIterTutorial() > 4)
                        {

                            controladorTutorialWaiter.current.createPanel();
                        }

                    }
                    else
                    {
                        i++;
                    }


                }

            }
        }
    }
    public void reOrderFunction(Collider other)
    {
        for (int x = 1; x <= current_hamburguer.getInstructions().Count - 1; x++)
        {
            //Sólo queremos modificar las posiciones de las instrucciones padre, los hijos se moveran con ellos. 

            ((GameObject)current_hamburguer.getInstructions()[x]).transform.position = new Vector3(this.transform.position.x + other.transform.localScale.y, ((GameObject)current_hamburguer.getInstructions()[x - 1]).transform.position.y - conditionalSeparationAxisY, this.transform.position.z);

        }
    }

    public OrderWaiter GetHamburguer()
    {
        return this.current_hamburguer;
    }


}


