using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;
using UnityEngine.Networking;

using UnityEngine.EventSystems;
using TMPro;

public class menuIniciScript : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject StartMenu;
    private GameObject RegisterMenu;
    private GameObject LogInMenu;
    private GameObject GameModeMenu;
    private GameObject keyboard;
    private GameObject currentField;

    private UserController userControl;

    void Start()
    {

        this.StartMenu = GameObject.Find("Start");
        this.RegisterMenu = GameObject.Find("SignInMenu");
        this.LogInMenu = GameObject.Find("LogInMenu");
        this.GameModeMenu = GameObject.Find("GameModeMenu");
        this.keyboard = GameObject.Find("Keyboard");
        this.userControl = GameObject.Find("UserControl").GetComponent<UserController>();

        this.GameModeMenu.SetActive(false);
        LogInMenu.SetActive(false);
        RegisterMenu.SetActive(false);
        this.keyboard.SetActive(false);

    }

    public void showSignInMenu(){
        this.StartMenu.SetActive(false);
        this.RegisterMenu.SetActive(true);
    }
    public void showLogInMenu(){
        this.StartMenu.SetActive(false);
        this.LogInMenu.SetActive(true);
    }
    public void showGameModeMenu()
    {
        ocultarKeyBoard();
        LogInMenu.SetActive(false);
        RegisterMenu.SetActive(false);
        this.GameModeMenu.SetActive(true);
    }

    public void chefMode()
    {
        if (!userControl.isTutorialChefCompleted())
        {
            DontDestroyOnLoad(GameObject.Find("UserControl"));
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            DontDestroyOnLoad(GameObject.Find("UserControl"));
            SceneManager.LoadScene("ChefScene");
        }
    }

    public void waiterMode()
    {

        if (!userControl.isTutorialWaiterCompleted())
        {
            DontDestroyOnLoad(GameObject.Find("UserControl"));
            SceneManager.LoadScene("WaiterTutorial");
        }
        else
        {
            DontDestroyOnLoad(GameObject.Find("UserControl"));
            SceneManager.LoadScene("WaiterScene");
        }

    }

    public void mostrarKeyBoard(){
        this.keyboard.SetActive(true);

        currentField = GameObject.Find(EventSystem.current.currentSelectedGameObject.name);
    }

    public void onKeyPressed(){
        if (currentField){
            currentField.GetComponent<TMPro.TMP_InputField>().text = currentField.GetComponent<TMPro.TMP_InputField>().text + EventSystem.current.currentSelectedGameObject.name;
        }
    }
    public void removePressed(){
        if (currentField){
            currentField.GetComponent<TMPro.TMP_InputField>().text = currentField.GetComponent<TMPro.TMP_InputField>().text.Remove(currentField.GetComponent<TMPro.TMP_InputField>().text.Length -1);

        }
    }

    public void ocultarKeyBoard(){
        this.keyboard.SetActive(false);
    }

    public void backFromSignIn(){
        this.currentField = null;
        GameObject.Find("UserNameFieldRegister").GetComponent<TMPro.TMP_InputField>().text = "";
        GameObject.Find("PasswordFieldRegister").GetComponent<TMPro.TMP_InputField>().text = "";
        this.keyboard.SetActive(false);
        this.RegisterMenu.SetActive(false);
        this.StartMenu.SetActive(true);

    }
    public void backFromLogIn(){
        this.currentField = null;
        GameObject.Find("UserNameFieldLogIn").GetComponent<TMPro.TMP_InputField>().text = "";
        GameObject.Find("PasswordFieldLogIn").GetComponent<TMPro.TMP_InputField>().text = "";
        this.keyboard.SetActive(false);
        this.LogInMenu.SetActive(false);
        this.StartMenu.SetActive(true);
      
    }


    public void saveOnExit(){
        StartCoroutine(putRequest());
    }

    public void authentication(){
        StartCoroutine(searchUser());
    }

    IEnumerator searchUser(){
        var name = GameObject.Find("UserNameFieldLogIn").GetComponent<TMPro.TMP_InputField>().text;
        var password = GameObject.Find("PasswordFieldLogIn").GetComponent<TMPro.TMP_InputField>().text;
        //var uri = "mongodb + srv://alexfuentesraventos:Baloncesto8@cluster-qbp6st97.lg4ch.mongodb.net/test/searchUser/" + name + "&" + password;
        var uri = "https://shrouded-sands-87010.herokuapp.com/searchUser/"+name+"&"+password;

        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
 
            if (request.isNetworkError) // Error
            {
                GameObject.Find("TituloLogIn").GetComponent<TMPro.TextMeshProUGUI>().text = request.error;
                Debug.LogError(request.error);
                GameObject.Find("PasswordFieldLogIn").SetActive(false);
                GameObject.Find("UserNameFieldLogIn").SetActive(false);
            }
            else // Success
            {
                var current_user = JsonUtility.FromJson<UserJSON>(request.downloadHandler.text);
                userControl.setCurrentUser(current_user);
                GameObject.Find("TituloLogIn").GetComponent<TMPro.TextMeshProUGUI>().text = userControl.getCurrentUser().name;
                
                showGameModeMenu();

            }
        }
    }
    public void SignIn(){
        StartCoroutine(createUser());
    }

    IEnumerator createUser(){
        var name = GameObject.Find("UserNameFieldRegister").GetComponent<TMPro.TMP_InputField>().text;
        var password = GameObject.Find("PasswordFieldRegister").GetComponent<TMPro.TMP_InputField>().text;
        var uri = "https://shrouded-sands-87010.herokuapp.com/createUser/"+name+"&"+password;

        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
 
            if (request.isNetworkError) // Error
            {

                GameObject.Find("TituloLogIn").GetComponent<TMPro.TextMeshProUGUI>().text = request.error;           
            }
            else // Success
            {

                //UserJSON result =  (UserJSON)json_serializer.DeserializeObject(request.downloadHandler.text);
                var currentUser = JsonUtility.FromJson<UserJSON>(request.downloadHandler.text);
                var userControl = GameObject.Find("UserControl").GetComponent<UserController>();
                userControl.setCurrentUser(currentUser);
                DontDestroyOnLoad (GameObject.Find("UserControl"));
                //CarregarTutorial();
                showGameModeMenu();
            }
        }
    }

    IEnumerator putRequest(){
        GameObject.Find("UserControl").GetComponent<UserController>().setCurrentDay(100000);
        string jsonData = JsonUtility.ToJson(GameObject.Find("UserControl").GetComponent<UserController>().getCurrentUser());
        //var jsonString = JsonUtility.ToJson(jsonData) ?? "";
       
        //byte[] myData = System.Text.Encoding.UTF8.GetBytes();
        UnityWebRequest www = UnityWebRequest.Put("http://shrouded-sands-87010.herokuapp.com/sendUser", jsonData);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();


    }

  
}
