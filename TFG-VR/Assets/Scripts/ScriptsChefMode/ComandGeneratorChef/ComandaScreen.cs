using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ComandaScreen : MonoBehaviour
{
    public string nivel;
    public string img;
    
    public ArrayList ingredients;

    public ComandaScreen()
    {
        
    }

    public void addIngredients(string ingredient)
    {
        this.ingredients.Add(ingredient);
    }
    
    public ArrayList getIngredients()
    {
        return this.ingredients;
    }

  
}
