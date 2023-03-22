using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenusControllerWaiter : MonoBehaviour
{
    private GameObject pauseMenu;
    [SerializeField]
    private OVRPlayerController player;

    private GameObject textScreenWaiter;
    private GameObject tempsWaiter;

    private GameObject achievementsMenu;

    private GameObject ranking;

    private GameObject savedText;

    private GameObject tutorialText;
    private GameObject buttonText;

    private bool isShowingComanda = false;

    private GameObject imageBig;
    private GameObject imageSmall;
    private Sprite myImg;
    private void Start()
    {
        savedText = GameObject.Find("SavingGameText");
        pauseMenu = GameObject.Find("PauseMenuWaiter");
        achievementsMenu = GameObject.Find("AchievementsWaiter");
        achievementsMenu.SetActive(false);
        pauseMenu.SetActive(false);

        textScreenWaiter = GameObject.Find("messageTextWaiter");
        tempsWaiter = GameObject.Find("TimeWaiter");

        imageBig = GameObject.Find("ImageBelowBig"); ;
        imageBig.SetActive(false);
        imageSmall = GameObject.Find("ImageBelowSmall"); ;
        imageSmall.SetActive(false);

        tutorialText = GameObject.Find("CanvasTextStart");
        buttonText = GameObject.Find("ButtonText");
        buttonText.SetActive(false);
    }


    public void showPauseMenu()
    {
        // We want the menu to form right in front of the player and facing him
        // First we adjust the menu position
        var newPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
        this.pauseMenu.transform.position = newPosition + (player.transform.forward * 1.5f);
        this.pauseMenu.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y + 180f, player.transform.rotation.eulerAngles.z);
        this.pauseMenu.SetActive(true);
        GameObject.Find("SliderWaiter").GetComponent<Slider>().value = GameControllerWaiter.current.current_user().getVolume();
    }

    public void activeTutorialText(bool active)
    {
        tutorialText.SetActive(active);
    }

    public void activeButtonText(bool active)
    {
        buttonText.SetActive(active);
    }

    public void showRanking()
    {

    }

    public void showAchievementsMenu()
    {

        achievementsMenu.transform.position = pauseMenu.transform.position;
        achievementsMenu.transform.rotation = pauseMenu.transform.rotation;
        this.pauseMenu.SetActive(false);
        this.achievementsMenu.SetActive(true);
        var achievements = GameControllerWaiter.current.current_user();
        if (achievements.getFirstOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getTenOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getTwentyfiveOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getFiftyOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getHundredOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(5).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFirstConditionalOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(6).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenConditionalOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(7).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyConditionalOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(8).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFiftyConditionalOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(9).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }


        if (achievements.getFirstBasicOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getTenBasicOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getTwentyfiveBasicOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }

        if (achievements.getFirstConditionalIfOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenConditionalIfOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(5).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyConditionalIfOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(6).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFirstConditionalIfElseOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(7).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenConditionalIfElseOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(8).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyConditionalIfElseOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(9).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFirstIterativeOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(10).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenIterativeOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(11).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyIterativeOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(1).transform.GetChild(12).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }

    }


    public void unlockAchievement(int achievementNum)
    {
        this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(achievementNum).transform.GetChild(1).GetComponent<Image>().color = Color.white;
    }

    public void deactivatePauseMenu()
    {
        this.pauseMenu.SetActive(false);
        this.achievementsMenu.SetActive(false);
        savedText.GetComponent<TextMeshProUGUI>().SetText("SAVE");
    }


    public void orderMenuWaiter(string text)
    {
        imageBig.SetActive(false);
        imageSmall.SetActive(false);
        textScreenWaiter.GetComponent<TextMeshProUGUI>().SetText(text);
        isShowingComanda = true;
        //screenAssistant.GetComponent<screenAssistant>().mostrarMissatge("Aixo es una comanda");
    }



    public void updateTemps(string actTemp)
    {
        tempsWaiter.GetComponent<TextMeshProUGUI>().text = actTemp + ":00 h";
    }

    public void menuEndOfTheDay(float exp)
    {
        if (isShowingComanda)
        {
            textScreenWaiter.SetActive(true);
            isShowingComanda = false;
        }
        textScreenWaiter.GetComponent<TextMeshProUGUI>().text = "Day ended!" + "\n" + "EXP :" + exp;

    }

    public void menuStartOfTheDay(int dia)
    {
        textScreenWaiter.GetComponent<TMPro.TextMeshProUGUI>().text = "Day: "+ dia.ToString()+" ended. Let's go with the next one! Go for another order!";
    }
    public void menuStartOfTheDayUnlockedLettuce(int dia)
    {
        textScreenWaiter.GetComponent<TMPro.TextMeshProUGUI>().text = "Day: " + dia.ToString() + " ended. You have leveled up and you unlocked the ingredient: lettuce." + "Go for the next order!";

    }
    public void menuStartOfTheDayUnlockedCondicionalsIf(int dia)
    {
        textScreenWaiter.GetComponent<TMPro.TextMeshProUGUI>().text = "Day: " + dia.ToString() + " ended. You have leveled up and you unlocked unlock the basic conditional orders." + "Go for the next order!";
        
    }
    public void menuStartOfTheDayUnlockedCondicionalsIfElse(int dia)
    {
        textScreenWaiter.GetComponent<TMPro.TextMeshProUGUI>().text = "Day: " + dia.ToString() + " ended. You have leveled up and you unlock the double conditional orders." + "Go for another order genius!";
    }

    public void menuIniciDeDiaUnlockedBucle(int dia)
    {
        textScreenWaiter.GetComponent<TMPro.TextMeshProUGUI>().text = "Day: " + dia.ToString() + " ended. You have leveled up and you unlock the iterative orders" + "Let's go for the next order!";
    }
    public void mostrarFeedback(string feedback)
    {
        textScreenWaiter.GetComponent<TextMeshProUGUI>().SetText(feedback);
        isShowingComanda = false;
    }

    public void showImageBig(bool booleano, string textImage)
    {
        if (!booleano)
        {
            imageBig.SetActive(booleano);
        }
        else
        {
            imageBig.SetActive(booleano);
            myImg = Resources.Load<Sprite>("Panels/images/" +textImage);
            imageBig.GetComponent<Image>().sprite = myImg;
        }

    }
    public void showImageSmall(bool booleano, string textImage)
    {
        if (!booleano)
        {
            imageSmall.SetActive(booleano);
        }
        else
        {
            imageSmall.SetActive(booleano);
            myImg = Resources.Load<Sprite>("Panels/images/" + textImage);
            imageSmall.GetComponent<Image>().sprite = myImg;
        }

    }

}
