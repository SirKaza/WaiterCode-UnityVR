using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScriptWaiter : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "panel")
        {
            if (!other.GetComponent<OVRGrabbable>().isGrabbed)
            {

                Destroy(other.gameObject);
                if (!other.GetComponent<panelScript>().getItWasInPanel())
                {

                    GameControllerWaiter.current.createPanel(other.name);
                }
            }
        }
    }
}
