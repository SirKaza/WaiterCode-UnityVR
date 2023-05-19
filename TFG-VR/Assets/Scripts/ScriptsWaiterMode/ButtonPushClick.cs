using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPushClick : MonoBehaviour
{
    //Probar a hacerlo todo public para poder mirar como va cambiando los valores y comparar con los valores que yo he declarado
    private float minLocalY;
    private float maxLocalY;

    private bool isClicked = false;
    public bool isButtonPressed = false;
    private bool isWaiting = false;

    private GameObject panel;

    public Material blueMat;
    // Start is called before the first frame update

    private Vector3 buttonUpPosition;
    private Vector3 buttonDownPosition;

    void Start()
    {
        minLocalY = transform.localPosition.y - 0.03f;
        maxLocalY = transform.localPosition.y;

        //Start with button minLocalY
        buttonDownPosition = new Vector3(transform.localPosition.x, minLocalY, transform.localPosition.z);
        buttonUpPosition = new Vector3(transform.localPosition.x, maxLocalY, transform.localPosition.z);

    }

    // Update is called once per frame
    void Update()
    {
        //Se supone que aquí bloqueamos la altura máxima del botón
        if(transform.localPosition.y >= maxLocalY)
        {
            //Volvemos a la posición 
            transform.localPosition = buttonUpPosition;
        }

        //Cuando lleguemos a la posición por debajo, entregaremos el pedido y bloqueamos el botón en esa posición
        if(transform.localPosition.y <= minLocalY)
        {
            transform.localPosition = buttonDownPosition;
            OnButtonDown();
        }
    }
        
    public void OnButtonDown()
    {
        if (!isButtonPressed && GameControllerWaiter.current.delivering == false)
        {
            isButtonPressed = true;

            panel = GameObject.Find("BasicPanel");
            
            //Devolvemos el botón a su lugar inicial 2 segundos más tarde de haberlo pulsado
            StartCoroutine("Waiting");

            //Enviamos el pedido para ver si está bien
            OrderWaiter sendOrder = panel.GetComponent<AttInstructions>().GetHamburguer();
            GameControllerWaiter.current.arriveOrder(sendOrder);
        }
    }
    IEnumerator Waiting()
    {
        isWaiting = true;
        StartCoroutine("ButtonUp");
        yield return new WaitForSeconds(2);
        isWaiting = false;

    }
    IEnumerator ButtonUp()
    {
        float maxWaitTime = 5.0f; // tiempo máximo de espera en segundos
        float elapsedTime = 0.0f;

        while (isWaiting)
        {
            if (elapsedTime >= maxWaitTime)
            {
                Debug.LogWarning("ButtonUp() coroutine timed out waiting for isWaiting to become false");
                break; // salir del bucle si ha pasado demasiado tiempo
            }
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, maxLocalY, transform.localPosition.z);
        isButtonPressed = false;
        GetComponent<MeshRenderer>().material = blueMat;
    }
}
