using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCardinalitats : MonoBehaviour
{
    private bool isNewGenerated;

    private Dictionary<string, (bool,int)> ingredients_available = new Dictionary<string, (bool, int)>()
    {
        {"BreadDOWN",(true,5) },
        {"Hamburguer",(true,5)},
        {"BreadUP",(true,5)},
        {"ketchup",(true,999)},
        {"cheese", (true,5)},
        {"lettuce", (false,5)}
        
    };

    public ControlCardinalitats(){
        isNewGenerated = true;
    }

    public int getCardinality(string _object)
    {
        return ingredients_available[_object].Item2;
    }
    public bool getNewGenerated()
    {
        return this.isNewGenerated;
    }
    public void setNewGenerator(bool conf)
    {
        this.isNewGenerated = conf;
    }
    public void AddCardinalitat(string _object)
    {
        ingredients_available[_object] = (ingredients_available[_object].Item1, ingredients_available[_object].Item2 + 1);
    }

    public void RestCardinality(string _object)
    {
        if (ingredients_available[_object].Item2 > 0)
        {
            ingredients_available[_object] = (ingredients_available[_object].Item1, ingredients_available[_object].Item2 - 1);

        }
    }
    public void generateCardinalities()
    {       
        ingredients_available["BreadDOWN"] = (true, 7);
        ingredients_available["BreadUP"] = (true, Random.Range(5,7));
        ingredients_available["Hamburguer"] = (true, 7);
        ingredients_available["cheese"] = (true, 7);

        if (ingredients_available["lettuce"].Item1){
            ingredients_available["lettuce"] = (true, 7);

        }

        isNewGenerated = true;
    
    }

    public void unlockLettuce(){
        ingredients_available["lettuce"] = (true,5);
    }

    public bool isDisponible(string objecte)
    {
        return ingredients_available[objecte].Item1;
    }
}
