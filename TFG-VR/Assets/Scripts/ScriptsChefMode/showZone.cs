using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showZone : MonoBehaviour
{
    GameObject snapZone;
    // Start is called before the first frame update
    void Start()
    {
        snapZone = GameObject.Find("plate_snapzone");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (controladorPartidaChef.current.isComandaShowingUp)
        {
            snapZone.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        snapZone.gameObject.SetActive(false);
    }
}
