using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBebida : MonoBehaviour
{
    private Vector3 posicionOriginal; // La posición original del objeto
    private string objectName;

    private void Start()
    {
        // Guardar la posición original del objeto
        posicionOriginal = transform.position;
        objectName = gameObject.name;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            Destroy(gameObject);

            if(gameObject.tag == "panel"){
                GameObject new_object = Instantiate(Resources.Load("Panels/"+objectName) as GameObject, posicionOriginal, Quaternion.identity);
                new_object.name = objectName;
            }
        }
    }
}
