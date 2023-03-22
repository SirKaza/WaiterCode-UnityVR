using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class APICommunicationWaiter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject savedConfirmed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void safeUser()
    {
        StartCoroutine(putRequest());
    }


    IEnumerator putRequest()
    {

        string jsonData = JsonUtility.ToJson(GameControllerWaiter.current.current_user().getCurrentUser());
        //var jsonString = JsonUtility.ToJson(jsonData) ?? "";

        //byte[] myData = System.Text.Encoding.UTF8.GetBytes();
        UnityWebRequest www = UnityWebRequest.Put("http://shrouded-sands-87010.herokuapp.com/sendUser", jsonData);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();
        savedConfirmed.GetComponent<TextMeshProUGUI>().SetText("SAVED!");
    }

}
