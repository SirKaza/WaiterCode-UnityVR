using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacioPaInferior : MonoBehaviour, IngredientCreacio
{
    public GameObject crearObjecte(GameObject colisio)
    {
        GameObject instance_prefab = Resources.Load("BreadDOWN") as GameObject;
        var instance = Instantiate(instance_prefab, colisio.gameObject.transform.position, Quaternion.identity);
        instance.name = "BreadDOWN";

        return instance;
    }
}
