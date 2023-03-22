using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComandaChef
{
    private ArrayList ingredients;

    public ComandaChef()
    {
        ingredients = new ArrayList();
    }

    public void addIngredients(GameObject ingredient)
    {
        this.ingredients.Add(ingredient);
    }
    public void removeIngredients(GameObject ingredient)
    {
        if (ingredients.Count > 0)
        {
            this.ingredients.Remove(ingredient);

        }
    }

    public string toString()
    {
        var resultat = "";

        for (int i = 0; i < ingredients.Count; i++)
        {
            resultat += (ingredients[i] as GameObject).name;
        }


        return resultat;
    }

    public ArrayList getIngredients()
    {
        return this.ingredients;
    }

    public GameObject getLastIngredient()
    {
        if (this.ingredients.Count == 0)
        {
            return null;
        }
        return this.ingredients[this.ingredients.Count - 1] as GameObject;
    }
}
