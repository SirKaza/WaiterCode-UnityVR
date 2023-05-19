using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FeedbackGeneratorWaiter : MonoBehaviour
{
    private OrderJSONWaiter actualOrder;
    private void Start()
    {

    }

    public (string, float, bool) getFeedback(OrderWaiter order, float time)
    {   
        bool result = false;
        int exp = 0;

        ArrayList instructions = order.getInstructions(); // Get the instructions from the panel
        ArrayList instructions_str = new ArrayList();
        ArrayList instructions_screen = new ArrayList(actualOrder.instructions); // Get the correct instructions from the JSON
        ArrayList removeList1 = new ArrayList();
        ArrayList removeList2 = new ArrayList(actualOrder.instructions);

        foreach (GameObject instruction in instructions) // Get the names of the instructions
        {
            instructions_str.Add(instruction.name);
            removeList1.Add(instruction.name);
        }

        if (instructions_str.Count > instructions_screen.Count) // More instructions than needed
        {
            return ("You put too many instructions. Try again!", 0f, false);
        }
        else if (instructions_str.Count < instructions_screen.Count && instructions_str.Count > 0) // Less instructions than needed
        {
            return ("You still have instructions to put. Don't give up!", 0f, false);
        }
        else if (instructions_str.Count == 0) // No instructions
        {
            return ("You did not put any instruction. Try again!", 0f, false);
        }
        else{ // Same number of instructions
            string bott_bread = "DownBreadPanel"; // Queremos comprobar unordered instructions
            string top_bread = "TopBreadPanel";

            int idx_bott_right = instructions_screen.IndexOf(bott_bread); // Indices del JSON
            int idx_top_right = instructions_screen.IndexOf(top_bread);
            
            if(idx_bott_right != -1 && idx_top_right != -1){ // Hay hamburguesa en el MENU

                int idx_bott_us = instructions_str.IndexOf(bott_bread); // Nuestros Indices
                int idx_top_us = instructions_str.IndexOf(top_bread);
                if (idx_bott_us != -1 && idx_top_us != -1) // Hay panes en nuestro pedido
                {
                    ArrayList sublista1 = instructions_str.GetRange(idx_bott_us, idx_top_us - idx_bott_us + 1);
                    ArrayList sublista2 = instructions_screen.GetRange(idx_bott_right, idx_top_right - idx_bott_right + 1);
                    
                    // Compare both hamburguers
                    if (sublista1.Count != sublista2.Count)
                    {
                        return ("There are more instructions on hamburguer than needed. Try again", 0f, false);
                    }
                    else // Same Length
                    {
                        for (int i = 0; i < sublista1.Count; i++)
                        {
                            if (!string.Equals(sublista1[i], sublista2[i])) // Si no son iguales
                            {
                                return ("The instructions inside hamburguer are unordered. Try again", 0f, false);
                                //string aux = (string)sublista1[i] + " " + (string)sublista2[i];
                                //return (aux, 0f, false);
                            }
                            else{ // Si son iguales vamos borrando instrucciones
                                removeList1.Remove(sublista1[i]);
                                removeList2.Remove(sublista2[i]);
                            }
                        }
                    }
                }else{ // No se puso algun pan o ambos
                    return ("Some bread instructions are missing from the hamburger. Try again", 0f, false);
                }
            }
            else // No hay pan de hamburguesa en el MENU
            {
                if(idx_bott_right != -1 || idx_top_right != -1){ // No se contempla esta opcion
                    return ("There are bread instructions in the menu. Try again", 0f, false);
                }
            }
            
            // En el caso que hubiera hamburguesa ya lo hemos quitado de removeList, si no habia hamburguesa en el menu da igual
            // Tambien hemos comprobado que no haya extras entre medio de la hamburguesa
            // Ahora queda comprobar los extras

            removeList1.Sort(); // Ordenamos ambas listas y comparamos
            removeList2.Sort();
            if (Enumerable.SequenceEqual(removeList1.Cast<string>(), removeList2.Cast<string>())) { // Las listas contienen los mismos elementos
                result = true;
            }
            else{
                return ("The hamburguer instructions are fine, but not the extras. Try again", 0f, false);
            }
        }
        
        if (result)
        {
            if (time > 20f)
            {
                exp = 25;
            }
            else if (time < 20f)
            {
                exp = 60;
            }

            return ("Succes! Let's deliver the order!", exp, true);
        }

        return ("Incorrect! Try again", 0f, false);
    }

    public void setComandaActual(OrderJSONWaiter comanda)
    {
        actualOrder = comanda;
    }
}
