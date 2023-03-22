using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacioHamburguesa : MonoBehaviour,IngredientCreacio
{
    public GameObject crearObjecte(GameObject colisio)
    {
        GameObject instance_prefab = Resources.Load("Hamburguer") as GameObject;
        var instance = Instantiate(instance_prefab, colisio.gameObject.transform.position, Quaternion.identity);
        instance.name = "Hamburguer";

        return instance;
    }
}
