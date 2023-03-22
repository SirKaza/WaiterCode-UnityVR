using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamburguer
{
    private ArrayList ingredients = new ArrayList();


    public void afegirIngredients(string nomIngredient)
    {
        ingredients.Add(nomIngredient);
    }

    public ArrayList getHamburguer()
    {
        return this.ingredients;
    }

    

}