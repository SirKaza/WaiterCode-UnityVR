using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trayScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "tray" && !other.GetComponent<OVRGrabbable>().isGrabbed)
        {   
            other.tag = "Untagged";
            GameControllerWaiter.current.deliverOrder(this.gameObject.name, other.gameObject);
        }
    }
}
