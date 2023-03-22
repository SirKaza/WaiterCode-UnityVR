using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class controladorMenus: MonoBehaviour
{
    private GameObject pauseMenu;
    private GameObject screenAssistant;
    [SerializeField]
    private OVRPlayerController player;

    private GameObject orderImage;
    private GameObject textScreen;
    private GameObject continueObject;
    private GameObject time;

    private GameObject achievementsMenu;
    private GameObject gameMode;

    private GameObject ranking;

    private bool isShowingComanda = false;

    private void Start()
    {
        ranking = GameObject.Find("Ranking");
        ranking.SetActive(false);

        pauseMenu = GameObject.Find("Canvas");
        achievementsMenu = GameObject.Find("AchievementsMenuChef");
        pauseMenu.SetActive(false);
        achievementsMenu.SetActive(false);
        orderImage = GameObject.Find("OrderImg");
        orderImage.SetActive(false);
        textScreen = GameObject.Find("messageText");
        continueObject = GameObject.Find("ContinueObject");
        time = GameObject.Find("Time");


    }

    public void mostrarMenuGameMode()
    {
        this.gameMode.SetActive(true);
    }

    public void hideMenuGameMode()
    {
        this.gameMode.SetActive(false);
    }
    public void showPauseMenu()
    {
        //Queremos que el menu se forme justo delante del jugador y encarado hacia él
        //Primero ajustamos la posición del menú
        var newPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
        this.pauseMenu.transform.position = newPosition + (player.transform.forward * 1.5f);
        this.pauseMenu.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y + 180f, player.transform.rotation.eulerAngles.z);
        this.pauseMenu.SetActive(true);
        GameObject.Find("Slider").GetComponent<Slider>().value = controladorPartidaChef.current.current_user().getVolume();
    }

    public void showAchievementsMenu(){
        achievementsMenu.transform.position = pauseMenu.transform.position;
        achievementsMenu.transform.rotation = pauseMenu.transform.rotation;
        var achievements = controladorPartidaChef.current.current_user();
        if (achievements.getFirstOrderChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getTenOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getTwentyfiveOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getFiftyOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getHundredOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(5).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFirstConditionalOrderChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(6).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenConditionalOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(7).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFiftyConditionalOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(9).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }


        if (achievements.getFirstBasicOrderChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getTenBasicOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getTwentyfiveBasicOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }

        if (achievements.getFirstConditionalIfOrderChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenConditionalIfOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(5).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyConditionalIfOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(6).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFirstConditionalIfElseOrderChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(7).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenConditionalIfElseOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(8).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyConditionalIfElseOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(9).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFirstIterativeOrderChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(10).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenIterativeOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(11).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyIterativeOrdersChefAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(12).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        this.pauseMenu.SetActive(false);
        this.achievementsMenu.SetActive(true);
    }


    public void unlockLogro(int numLogro){
        this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(numLogro).transform.GetChild(1).GetComponent<Image>().color = Color.white;
    }

    public void deactivePauseMenu()
    {
        this.pauseMenu.SetActive(false);
        this.achievementsMenu.SetActive(false);
    }

    public void startOfTheDayMenu(int num_dia)
    {
        textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Day " + num_dia + "\n" + "Good luck!";
        //screenAssistant.GetComponent<screenAssistant>().mostrarMissatge("Dia " + num_dia + "\n" + "Mucha suerte !");
    }

    public void startOfTheDayMenuUnlockedSimpleConditionals(int num_dia, int iteracion)
    {
        if (iteracion == 0)
        {
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Day " + num_dia + "\n" + "Congratulations! You unlocked the simple conditional orders!";
        }
        else if (iteracion == 1)
        {
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "This orders has one condition: \n If it is fulfilled, you have to put the ingredient";
        }
        else if (iteracion == 2)
        {
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Example: \n if you have 3 of cheese: put cheese";

        }
        else
        {
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Congratulations! You unlocked a new ingredient: Lettuce";

        }
        //screenAssistant.GetComponent<screenAssistant>().mostrarMissatge("Dia " + num_dia + "\n" + "Mucha suerte !");
    }

    public void startOfTheDayMenuUnlockedDoubleConditionals(int num_dia,int iteracion)
    {
        if (iteracion == 0){
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Day " + num_dia + "\n" + "Congratulations! You unlocked the double conditional orders!";
        }
        else if (iteracion == 1){
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "This orders has one condition: \n If it is fulfilled, you have to put the ingredient \n If not, you have the alternative!";
        }else if (iteracion == 2){
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Example: \n if you have 3 of cheese: put cheese \n If not: put meat \n Let's go!";

        }
        //screenAssistant.GetComponent<screenAssistant>().mostrarMissatge("Dia " + num_dia + "\n" + "Mucha suerte !");
    }

     public void startOfTheDayMenuUnlockedIteratives(int num_dia,int iteracion)
    {
        if (iteracion == 0){
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Day " + num_dia + "\n" + "Congratulations! You unlocked the loop orders!";
        }else if (iteracion == 1){
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "These orders has a loop that you have to repeat as many times as indicated.";
        }else {
            textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "Example: \n Repeat 2 times: Put meat\n Let's go!";

        }
        //screenAssistant.GetComponent<screenAssistant>().mostrarMissatge("Dia " + num_dia + "\n" + "Mucha suerte !");
    }

    public void endOfTheDayMenu(float exp)
    {
        if (isShowingComanda)
        {
            orderImage.SetActive(false);
            textScreen.SetActive(true);
            continueObject.SetActive(true);
            isShowingComanda = false;
        }
        textScreen.GetComponent<TMPro.TextMeshProUGUI>().text = "End of the day!" + "\n" + "EXP :" + exp;

        //screenAssistant.GetComponent<screenAssistant>().mostrarMissatge("Jornada finalitzada !" +  "\n" + "EXP :" + exp);

    }

    public void orderMenu(string img)
    {
        textScreen.SetActive(false);
        continueObject.SetActive(false);
        orderImage.SetActive(true);
        var spriteIMG = Resources.Load<Sprite>(img);
        orderImage.GetComponent<UnityEngine.UI.Image>().sprite  = spriteIMG;


        isShowingComanda = true;
        //screenAssistant.GetComponent<screenAssistant>().mostrarMissatge("Aixo es una comanda");

    }

    public void showFeedback(string str_feedback)
    {
        if (isShowingComanda)
        {
            orderImage.SetActive(false);
            textScreen.SetActive(true);
            continueObject.SetActive(true);
            isShowingComanda = false;
        }
        textScreen.GetComponent<TextMeshProUGUI>().text = str_feedback;

    }



    public void updateTemps(string actTemp){
        time.GetComponent<TextMeshProUGUI>().text = actTemp + ":00 h";
    }

   

    
    
}
