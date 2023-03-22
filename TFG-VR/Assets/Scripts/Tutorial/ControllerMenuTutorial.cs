using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMenuTutorial : MonoBehaviour
{
    private GameObject menuPausa;
    private GameObject screenAssistant;

    GameObject imatgeComanda;
    GameObject textScreen;
    GameObject continueObject;
    GameObject temps;

    GameObject menuLogros;
    
    private bool isShowingComanda = false;


    private void Start()
    {
        menuPausa = GameObject.Find("Canvas");
        menuLogros = GameObject.Find("AchievementsTutorialChef");
        menuPausa.SetActive(false);
        menuLogros.SetActive(false);
        imatgeComanda = GameObject.Find("comandaImg");
        imatgeComanda.SetActive(false);
        textScreen = GameObject.Find("messageText");
        continueObject = GameObject.Find("ContinuarObject");
        temps = GameObject.Find("Temps");

    }

    public void deactivateContinue(){
        this.continueObject.SetActive(false);
    }
    public void activateContinue(){
        this.continueObject.SetActive(true);
    }

    public void showPauseMenu()
    {
        this.menuPausa.SetActive(true);
    }

    public void mostrarMenuLogros(){
        this.menuPausa.SetActive(false);
        this.menuLogros.SetActive(true);
    }

    public void unlockLogro(int numLogro){
        this.menuLogros.transform.GetChild(0).transform.GetChild(0).transform.GetChild(numLogro).transform.GetChild(1).GetComponent<Image>().color = Color.white;
    }

    public void deactivatePauseMenu()
    {
        this.menuPausa.SetActive(false);
        this.menuLogros.SetActive(false);
    }

    public void startOfTheDayMenu(int num_dia)
    {
        textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Day " + num_dia + "\n" + "Good luck!";
        //screenAssistant.GetComponent<screenAssistant>().mostrarMissatge("Dia " + num_dia + "\n" + "Mucha suerte !");
    }
    


    public void endOfTheDayMenu(float exp)
    {
        if (isShowingComanda)
        {
            imatgeComanda.SetActive(false);
            textScreen.SetActive(true);
            continueObject.SetActive(true);
            isShowingComanda = false;
        }
        textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Day ended!" + "\n" + "EXP :" + exp;


    }

    public void showControllers(){
        imatgeComanda.SetActive(true);
        imatgeComanda.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Sprites/ControllerTutorialOculus");
    }
    public void showOrder(){
        imatgeComanda.SetActive(true);
        imatgeComanda.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Sprites/OrdersEng/BasicOrder0");
    }
    public void restartScreen(){
        imatgeComanda.SetActive(false);
    }

    public void orderMenu(string img)
    {
        textScreen.SetActive(false);
        continueObject.SetActive(false);
        imatgeComanda.SetActive(true);
        var spriteIMG = Resources.Load<Sprite>(img);
        if (spriteIMG==null){
        }
        imatgeComanda.GetComponent<UnityEngine.UI.Image>().sprite  = spriteIMG;


        isShowingComanda = true;

    }
    public void showFeedback(string str_feedback)
    {
        if (isShowingComanda)
        {
            imatgeComanda.SetActive(false);
            textScreen.SetActive(true);
            continueObject.SetActive(true);
            isShowingComanda = false;
        }
        textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = str_feedback;

    }


    public void updateTemps(string actTemp){
        temps.GetComponent<TMPro.TextMeshProUGUI>().text = actTemp + ":00 h";
    }
}

   
