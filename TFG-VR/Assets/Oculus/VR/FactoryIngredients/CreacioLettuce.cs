using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacioLettuce : MonoBehaviour,IngredientCreacio
{
    public GameObject crearObjecte(GameObject colisio)
    {
        GameObject instance_prefab = Resources.Load("lettuceGO") as GameObject;
        var instance = Instantiate(instance_prefab, colisio.gameObject.transform.position, Quaternion.identity);
        instance.name = "lettuce";

        return instance;
    }
}