using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTray : MonoBehaviour
{
    private Vector3 posicionOriginal; // La posición original del objeto
    private bool destroyed = false; // Indica si el objeto ya ha sido destruido

    private void Start()
    {
        // Guardar la posición original del objeto
        posicionOriginal = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!destroyed && collision.gameObject.CompareTag("floor"))
        {
            destroyed = true;
            Destroy(gameObject);

            if (gameObject.tag == "tray")
            {
                if (GameControllerWaiter.current.delivering)
                { // Entregando pedido
                    GameControllerWaiter.current.recreatingDeliver = true;
                    GameControllerWaiter.current.createDelivery();
                }
                else
                { // Sin estado de entrega
                    GameObject new_object = Instantiate(Resources.Load("Tray") as GameObject, posicionOriginal, Quaternion.identity);
                    new_object.name = "Tray";
                    // Después de recrear el objeto Tray
                    GameControllerWaiter.current.TrayPosition = new_object.transform.position;
                    posicionOriginal = GameControllerWaiter.current.TrayPosition;
                }
            }
        }
    }
}
