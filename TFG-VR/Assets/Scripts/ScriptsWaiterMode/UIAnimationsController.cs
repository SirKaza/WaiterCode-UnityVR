using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAnimationsController : MonoBehaviour
{
    [SerializeField]
    private OVRPlayerController player;

    private GameObject textClient1;
    private GameObject textClient2;
    private GameObject textClient3;
    private GameObject textClient4;
    private GameObject textClient5;
    private GameObject arrow;
    private Vector3 arrowStart;
    private Vector3 arrowEnd;
    private Vector3 textClientStart1;
    private Vector3 textClientStart2;
    private Vector3 textClientStart3;
    private Vector3 textClientStart4;
    private Vector3 textClientStart5;
    private Vector3 textClientEnd1;
    private Vector3 textClientEnd2;
    private Vector3 textClientEnd3;
    private Vector3 textClientEnd4;
    private Vector3 textClientEnd5;
    private float movement;
    // Start is called before the first frame update
    void Start()
    {
        textClient1 = GameObject.Find("TextClient1");
        textClient2 = GameObject.Find("TextClient2");
        textClient3 = GameObject.Find("TextClient3");
        textClient4 = GameObject.Find("TextClient4");
        textClient5 = GameObject.Find("TextClient5");
        arrow = GameObject.Find("Arrow");
        arrowStart = arrow.transform.position;
        arrowEnd = arrowStart - new Vector3(0f, 0.3f, 0f);
        textClientStart1 = textClient1.transform.position;
        textClientEnd1 = textClientStart1 + new Vector3(0f, 0.1f, 0f);
        textClientStart2 = textClient2.transform.position;
        textClientEnd2 = textClientStart2 + new Vector3(0f, 0.1f, 0f);
        textClientStart3 = textClient3.transform.position;
        textClientEnd3 = textClientStart3 + new Vector3(0f, 0.1f, 0f);
        textClientStart4 = textClient4.transform.position;
        textClientEnd4 = textClientStart4 + new Vector3(0f, 0.1f, 0f);
        textClientStart5 = textClient5.transform.position;
        textClientEnd5 = textClientStart5 + new Vector3(0f, 0.1f, 0f);

        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Mathf.PingPong(Time.time, 1f) / 1f;
        if (arrow.activeSelf)
        {
            arrow.transform.position = Vector3.Lerp(arrowStart, arrowEnd, movement);
        }

        if (textClient1.activeSelf)
        {
            if (!GameControllerWaiter.current.getIsComandaActive())
            {
                textClient1.transform.position = Vector3.Lerp(textClientStart1, textClientEnd1, movement);
                textClient1.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
            }

            else
            {
                textClient1.transform.position = textClientStart1;
            }
            //textClient1.transform.Rotate(Vector2.up, Time.deltaTime * 5f);

        }


        else if (textClient2.activeSelf)
        {
            if (!GameControllerWaiter.current.getIsComandaActive())
            {
                textClient2.transform.position = Vector3.Lerp(textClientStart2, textClientEnd2, movement);
                textClient2.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
            }
            else
            {
                textClient2.transform.position = textClientStart2;
            }
        }

        else if (textClient3.activeSelf)
        {
            if (!GameControllerWaiter.current.getIsComandaActive())
            {
                textClient3.transform.position = Vector3.Lerp(textClientStart3, textClientEnd3, movement);
                textClient3.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
            }
            else
            {
                textClient3.transform.position = textClientStart3;
            }
        }

        else if (textClient4.activeSelf)
        {
            if (!GameControllerWaiter.current.getIsComandaActive())
            {
                textClient4.transform.position = Vector3.Lerp(textClientStart4, textClientEnd4, movement);
                textClient4.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
            }
            else
            {
                textClient4.transform.position = textClientStart4;
            }
        }

        else if (textClient5.activeSelf)
        {
            if (!GameControllerWaiter.current.getIsComandaActive())
            {
                textClient5.transform.position = Vector3.Lerp(textClientStart5, textClientEnd5, movement);
                textClient5.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
            }
            else
            {
                textClient5.transform.position = textClientStart5;
            }
        }

    }
}
