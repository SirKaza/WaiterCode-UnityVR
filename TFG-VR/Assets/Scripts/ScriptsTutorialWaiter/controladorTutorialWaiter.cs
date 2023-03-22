using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controladorTutorialWaiter : MonoBehaviour
{
    [SerializeField]
    private OVRPlayerController player;


    private int iterTutorial = 0;

    private controladorMenusTutorialWaiter controllerMenus;  // Clase controladora de los menús (entrar para ver mayor descripción). 

    // SINGLETON
    public static controladorTutorialWaiter current;
    private UserController userControl;

    private bool gamePaused;

    private GameObject Panel1;
    private GameObject Panel2;
    private GameObject Panel3;
    private Vector3 PanelPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }
    void Start()
    {


        controllerMenus = this.GetComponentInParent<controladorMenusTutorialWaiter>();
        this.userControl = GameObject.Find("UserControl").GetComponent<UserController>();


        //GameObject.Find("MusicControllerWaiterTutorial").GetComponent<musicController>().setVolume(this.userControl.getVolume());

        Panel1 = GameObject.Find("Panel1");
        PanelPosition = Panel1.transform.position;
        Panel1.SetActive(false);
        Panel2 = GameObject.Find("Panel2");
        Panel2.SetActive(false);

        Panel3 = GameObject.Find("Panel3");
        //Panel3.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        //if (isDayStarted && !gamePaused)
        //{
        //    this.timePerDay = calculateTime(timePerDay);
        //}
        if (OVRInput.GetDown(OVRInput.Button.Two) && gamePaused)
        {
            restartGame();
            controllerMenus.deactivatePauseMenu();
            gamePaused = false;
        }

        // PAUSE MENU ACTIVATION


        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            controllerMenus.showPauseMenu();
            gamePaused = true;
        }

        //ADVANCING SCREENS


        if (OVRInput.GetDown(OVRInput.Button.One) && !gamePaused)
        {
            if (iterTutorial == 0)
            {
                Panel3.SetActive(false); 
                controllerMenus.setText("You can move around using joystick. Left one to move, and right one to control the camera.");
                controllerMenus.showImage("MovementJoystick");
                iterTutorial++;
            }

            else if(iterTutorial == 1)
            {
                controllerMenus.setText("Or you also can use teleport mode. This mode reduce dizzinees. If you are not used to play VR games, we recommend this one. Maintain pressed press the trigger and let it go!");
                controllerMenus.showImage("TeleportTrigger");
                iterTutorial++;
            }

            else if (iterTutorial == 2)
            {
                controllerMenus.setText("One panel appeared! We will learn how to use it!");
                Panel1.SetActive(true);
                iterTutorial++;
            }

            else if (iterTutorial == 3)
            {
                iterTutorial++;
                controllerMenus.setText("Put your hand on the green panel, maintain press either of the two trigger indicated in the image to take it please");
                controllerMenus.showImage("ControllerTutorialOculus");
                controllerMenus.showButton(false);
            }

            else if (iterTutorial == 10)
            {
                userControl.tutorialWaiterCompleted();
                DontDestroyOnLoad(GameObject.Find("UserControl"));
                SceneManager.LoadScene("WaiterScene");
            }


        }

        if (iterTutorial == 4 && Panel1.GetComponent<OVRGrabbable>().isGrabbed)
        {
            controllerMenus.setText("Bring it closer to the whiteboard and let it go!");
            controllerMenus.hideImage();
            iterTutorial++;

        }

        else if (iterTutorial == 5 && Panel1.GetComponent<panelScriptTutorial>().getIsInPanel())
        {
            controllerMenus.setText("Perfect! Do it again with new panel. Let's practice!");
            Panel2.SetActive(true);
            iterTutorial++;

        }

        else if (iterTutorial == 6 && Panel2.GetComponent<panelScriptTutorial>().getIsInPanel())
        {
            controllerMenus.setText("Perfect! One more time!");
            Panel3.SetActive(true);
            iterTutorial++;

        }

        else if (iterTutorial == 7 && Panel3.GetComponent<panelScriptTutorial>().getIsInPanel())
        {
            controllerMenus.setText("Perfect! You also can take panels from the whiteboard, try it!");
            iterTutorial++;
        }

        else if (iterTutorial == 8 && (Panel2.GetComponent<panelScriptTutorial>().getItWasInPanel() || Panel3.GetComponent<panelScriptTutorial>().getItWasInPanel()))
        {
            controllerMenus.setText("Good job! You can let it go or you can put the panel in other position!");
            iterTutorial++;
        }

        else if (iterTutorial == 9 && ((Panel2.GetComponent<panelScriptTutorial>().getItWasInPanel() && !Panel2.GetComponent<OVRGrabbable>().isGrabbed) || (Panel3.GetComponent<panelScriptTutorial>().getItWasInPanel() && !Panel3.GetComponent<OVRGrabbable>().isGrabbed)))
        {
            controllerMenus.setText("Perfect! You can practice until you want. Press A to start the game");
            controllerMenus.showButton(true);
            iterTutorial++;
        }


    }

    private void restartGame()
    {
        Time.timeScale = 1;
        this.gamePaused = false;
    }

    public UserController current_user()
    {
        return this.userControl;
    }
    //Create another panel on the table
    public void createPanel()
    {

        Panel1 = Instantiate(Resources.Load("Panels/Panel") as GameObject, PanelPosition, Quaternion.identity);
    }

    public int getIterTutorial()
    {
        return iterTutorial;
    }
}
