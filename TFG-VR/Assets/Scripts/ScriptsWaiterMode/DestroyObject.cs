using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
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
            Destroy(this.gameObject);

            // // Crear una nueva instancia del objeto original en la posición original
            // GameObject new_object = Instantiate(Resources.Load(objectName) as GameObject, posicionOriginal, Quaternion.identity);
            // new_object.name = objectName;
        }
    }
}
