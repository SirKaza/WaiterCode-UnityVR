using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ingredient" || collision.gameObject.tag == "dish")
        {
            Destroy(collision.gameObject, 1);
        }
    }
}
