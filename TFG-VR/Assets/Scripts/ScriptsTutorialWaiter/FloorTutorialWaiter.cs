using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTutorialWaiter : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "panel")
        {
            if (!other.GetComponent<OVRGrabbable>().isGrabbed && controladorTutorialWaiter.current.getIterTutorial()>9)
            {

                Destroy(other.gameObject);
                controladorTutorialWaiter.current.createPanel();
            }
        }
    }
}
