using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacioPlats : MonoBehaviour, IngredientCreacio
{
    public GameObject crearObjecte(GameObject colisio)
    {
        GameObject instance_prefab = Resources.Load("dishes_deriv") as GameObject;
        var instance = Instantiate(instance_prefab, colisio.gameObject.transform.position, Quaternion.identity);
        instance.name = "dishes_deriv";

        return instance;
    }
}
