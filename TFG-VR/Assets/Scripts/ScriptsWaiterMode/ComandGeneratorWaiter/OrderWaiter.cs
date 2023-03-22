 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderWaiter
{
    private ArrayList instructions;

    public OrderWaiter()
    {
        instructions = new ArrayList();
    }


    public void addInstruction(GameObject instruction)
    {
        this.instructions.Add(instruction);
    }

    public void insertInstruction(int index, GameObject instruction)
    {
        this.instructions.Insert(index, instruction);
    }

    public void removeInstruction(GameObject instruction)
    {
        if (instructions.Count > 0)
        {
            this.instructions.Remove(instruction);

        }
    }

    public string toString(){
        var resultat = "";

        for(int i=0; i < instructions.Count; i++){
            resultat += (instructions[i] as GameObject).name;
        }


        return resultat;
    }

    public ArrayList getInstructions()
    {
        return this.instructions;
    }

    public GameObject getLastInstruction()
    {
        if (this.instructions.Count == 0)
        {
            return null;
        }
        return this.instructions[this.instructions.Count - 1] as GameObject;
    }
}
