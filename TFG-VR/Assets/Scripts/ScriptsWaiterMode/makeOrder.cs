using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class makeOrder : MonoBehaviour
{
    private bool isNearTable = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Este lo tenemos por si el player ya está dentro del collider cuando lo activamos
    //void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player" && !isNearTable)
    //    {
    //        isNearTable = true;
    //        controladorPartidaWaiter.current.setIsNearTable(isNearTable);
    //        textClient1.GetComponent<TextMeshProUGUI>().SetText(isNearTable.ToString);

    //    }

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isNearTable)
        {
            isNearTable = true;
            GameControllerWaiter.current.setIsNearTable(isNearTable);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearTable = false;
            GameControllerWaiter.current.setIsNearTable(isNearTable);
        }
    }
}
