using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestroyPanel : MonoBehaviour
{
    private Vector3 posicionOriginal; // La posición original del objeto
    private Vector3 localScaleOriginal = new Vector3(0.2f, 0.05f, 1.5f); // La escala original del objeto
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
                new_object.transform.localScale = localScaleOriginal;
            }
            // GameObject[] objectsWithTagAndName = GameObject.FindGameObjectsWithTag("panel")
            // .Where(obj => obj.name == objectName)
            // .ToArray();

            // if (objectsWithTagAndName.Length == 0) // Si no existe una instancia, creamos una nueva
            // {
            // Crear una nueva instancia del objeto original en la posición original
            
            // }
        }
    }
}
