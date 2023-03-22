using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class controladorMenusTutorialWaiter : MonoBehaviour
{
    private GameObject pauseMenu;
    [SerializeField]
    private OVRPlayerController player;

    private GameObject textScreenWaiter;

    private GameObject achievementsMenu;

    private GameObject ranking;

    private GameObject savedText;



    private GameObject image;
    private GameObject continueText;
    private GameObject btnA;
    private Sprite myImg;
    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.Find("ImageTutorial");
        image.SetActive(false);

        savedText = GameObject.Find("SavingGameTextTutorial");
        pauseMenu = GameObject.Find("PauseMenuWaiterTutorial");
        pauseMenu.SetActive(false);
        achievementsMenu = GameObject.Find("AchievementsWaiterTutorial");
        achievementsMenu.SetActive(false);

        textScreenWaiter = GameObject.Find("messageTextWaiterTutorial");

        continueText = GameObject.Find("ContinueTextTutorial");
        btnA = GameObject.Find("BtnATutorial");
    }

    // Update is called once per frame
    public void showPauseMenu()
    {
        // We want the menu to form right in front of the player and facing him
        // First we adjust the menu position
        var newPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
        this.pauseMenu.transform.position = newPosition + (player.transform.forward * 1.5f);
        this.pauseMenu.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y + 180f, player.transform.rotation.eulerAngles.z);
        GameObject.Find("Slider").GetComponent<Slider>().value = controladorTutorialWaiter.current.current_user().getVolume();
        this.pauseMenu.SetActive(true);
    }
    public void deactivatePauseMenu()
    {
        this.pauseMenu.SetActive(false);
        this.achievementsMenu.SetActive(false);
        savedText.GetComponent<TextMeshProUGUI>().SetText("SAVE");
    }
    public void showImage(string textImage)
    {

        myImg = Resources.Load<Sprite>("Sprites/" + textImage);
        image.GetComponent<Image>().sprite = myImg;
        image.SetActive(true);

    }

    public void hideImage()
    {
        image.SetActive(false);
    }

    public void setText(string Text)
    {
        textScreenWaiter.GetComponent<TextMeshProUGUI>().text = Text;
    }

    public void showButton(bool booleano)
    {
        continueText.SetActive(booleano);
        btnA.SetActive(booleano);
    }

    public void showAchievementsMenu()
    {
        this.pauseMenu.SetActive(false);
        this.achievementsMenu.SetActive(true);
        achievementsMenu.transform.position = achievementsMenu.transform.position;
        achievementsMenu.transform.rotation = pauseMenu.transform.rotation;
        var achievements = controladorTutorialWaiter.current.current_user();
        if (achievements.getFirstOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getTwentyfiveOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
        if (achievements.getFiftyOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFirstConditionalIfOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenConditionalIfOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyConditionalIfOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(5).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFirstConditionalIfElseOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(6).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenConditionalIfElseOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(7).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyConditionalIfElseOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(8).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getFirstIterativeOrderWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(9).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getTenIterativeOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(10).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }
        if (achievements.getThirtyIterativeOrdersWaiterAchievement())
        {
            this.achievementsMenu.transform.GetChild(0).transform.GetChild(0).transform.GetChild(11).transform.GetChild(1).GetComponent<Image>().color = Color.white;
        }

    }

}
