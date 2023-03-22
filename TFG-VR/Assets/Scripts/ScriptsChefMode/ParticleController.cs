using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Esta clase sirve para controlar cuando se pone el ketchup
public class ParticleController : MonoBehaviour
{
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "ingredient"){
            if (other.transform.parent == null){
            }else{
                if (other.transform.parent.tag == "dish" && other.transform.parent.gameObject.GetComponent<AttIngredients>().GetHamburguer().getLastIngredient().name != "ketchup"){
                    GameObject instance_prefab = Resources.Load("ketchupGOG") as GameObject;
                    var instance = Instantiate(instance_prefab, new Vector3(other.transform.position.x,other.transform.position.y+0.025f,other.transform.position.z), Quaternion.identity);
                    instance.name = "ketchup";
                    other.transform.parent.gameObject.GetComponent<AttIngredients>().addKetchup(instance);

                }
            }
        }

    }

}
