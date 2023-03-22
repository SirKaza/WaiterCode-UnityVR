using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryIngredients 
{
    public GameObject getIngredient(GameObject colisio)
    {
        switch (colisio.name)
        {
            case "BreadUPGenerator":
                return (new CreacioPaSuperior()).crearObjecte(colisio);
            case "DishesGenerator":
                return (new CreacioPlats()).crearObjecte(colisio);
            case "BreadDOWNGenerator":
                return (new CreacioPaInferior()).crearObjecte(colisio);
            case "HamburguerGenerator":
                return (new CreacioHamburguesa()).crearObjecte(colisio);
            case "cheese_generator":
                return (new CreacioCheese()).crearObjecte(colisio);
            case "lettuce_generator":
                return (new CreacioLettuce()).crearObjecte(colisio);
            default:
                return null;
        }
    }
}
