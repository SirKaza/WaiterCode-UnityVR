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

    private bool isWaiting = false;
    private bool isBeingTouched = false;
    private bool isClicked = false;

    private GameObject panel;

    public Material blueMat;
    // Start is called before the first frame update

    private Vector3 buttonUpPosition;
    private Vector3 buttonDownPosition;

    void Start()
    {

        minLocalY = transform.localPosition.y - 0.04f;
        maxLocalY = transform.localPosition.y;

        //Start with button minLocalY
        buttonDownPosition = new Vector3(transform.localPosition.x, minLocalY, transform.localPosition.z);
        buttonUpPosition = new Vector3(transform.localPosition.x, maxLocalY, transform.localPosition.z);

    }

    // Update is called once per frame
    void Update()
    {

        if (!isClicked)
        {
            //Se supone que aquí bloqueamos la altura máxima del botón
            if(transform.localPosition.y >= maxLocalY)
            {
                //Volvemos a la posición 
                transform.localPosition = buttonUpPosition;
            }

            //Cuando lleguemos a la posición por debajo de MinLocalY, entregaremos el pedido y bloqueamos el botón en esa posición
            if(transform.localPosition.y <= minLocalY)
            {
                isClicked = true;
                transform.localPosition = buttonDownPosition;
                OnButtonDown();
            }
        }
    }
        
    public void OnButtonDown()
    {
        panel = GameObject.Find("BasicPanel");

        //Enviamos el pedido para ver si está bien



        OrderWaiter sendOrder = panel.GetComponent<AttInstructions>().GetHamburguer();
        GameControllerWaiter.current.arriveOrder(sendOrder);

        
        //Devolvemos el botón a su lugar inicial 2 segundos más tarde de haberlo pulsado
        StartCoroutine("Waiting");
        StartCoroutine("ButtonUp");
        
    }

    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(3);
        isWaiting = false;

    }
    IEnumerator ButtonUp()
    {
        while (isWaiting)
        {
            yield return new WaitForSeconds(0.1f);
        }

        GetComponent<MeshRenderer>().material = blueMat;
        transform.localPosition = new Vector3(transform.localPosition.x, maxLocalY, transform.localPosition.z);
        isClicked = false;
    }


}
