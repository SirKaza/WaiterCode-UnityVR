using Oculus.Platform.Samples.VrHoops;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Main class of the game. It controls all the variables and the main loop of the game.
/// Through a singleton pattern, the rest of the classes can access it to consult the current state of the game.
/// </summary>
public class controladorPartidaChef : MonoBehaviour
{
    [SerializeField]
    private OVRPlayerController player;

    private float timePerDay = 96f; // Variable to control the durability of each day. 
    private float timeActualOrder = 0f; // Variable to measure the time to cook the current command.

    private float dayExp = 0f; // Experience accumulated throughout the day.
    private float totalExp = 0f; // Experience accumulated throughout the game.

    private int actualDay = 1; // Current day of the start of the day
    private int actualHour = 9; // Current hour of actual day
    private bool isEndOfTheDay = false; // true => We are in the dialogs at the end of the day.

    private bool gamePaused = false; // Variable to know if the game is paused or not.
    public bool isComandaShowingUp = false; // Variable to know if a command is present or not on the screen.

    private bool isUnlockedLevel2 = false;
    private bool isJustUnlockedLevel2 = false;



    private bool isUnlockedLevel3 = false;
    private bool isJustUnlockedLevel3 = false;

    private bool isUnlockedLevel4 = false;
    private bool isJustUnlockedLevel4 = false;

    private int iterTutorial = 0;

    private GameObject menuPausa; // Pause Menu
    private controladorMenus UIController;  // UI Controller class 
  

    private FeedbackGenerator feedbackGenerator; // Class to generate feedback of an order
    private ComandaContChef orderGenerator; // Class to generate and control the orders
    private ControlCardinalitats cardinalitatyController;

    private Subject subject = new Subject();
    private AchievementsController achievementsController;
    // SINGLETON
    public static controladorPartidaChef current;
    private UserController userControl;

    private GameObject lettuceGenerator;


    int step = 0;


    private void Awake()
    {
        current = this;
    }

    // Init the classes and the differents gameObjects
    private void Start()
    {
        Debug.Log("Entramos en controlador Partida");
        menuPausa = GameObject.Find("Canvas");
        UIController = this.GetComponentInParent<controladorMenus>();
        //controladorMenus.menuIniciDeDia(dia_actual);

        feedbackGenerator = new FeedbackGenerator();
        orderGenerator = new ComandaContChef();
        cardinalitatyController = new ControlCardinalitats();
        achievementsController = new AchievementsController();
        subject.AddObserver(achievementsController);

        this.userControl = GameObject.Find("UserControl").GetComponent<UserController>();
        this.lettuceGenerator = GameObject.Find("lettuce_generator");
        this.lettuceGenerator.SetActive(false);

        this.actualDay = this.userControl.getCurrentDay();
        this.totalExp = this.userControl.getTotalExp();
        GameObject.Find("MusicController").GetComponent<musicController>().setVolume(this.userControl.getVolume());

    }

    private void Update()
    {
            
        if (isComandaShowingUp && !gamePaused)
        {
            this.timePerDay = calcularTemps(timePerDay);
        }
        else if (OVRInput.GetDown(OVRInput.Button.Two) && gamePaused)
        {
            reanudarGame();
            UIController.deactivePauseMenu();
            gamePaused = false;
        }

        // ACTIVATING PAUSE MENU


        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            pausarGame();
            UIController.showPauseMenu();
            gamePaused = true;
        }

        //AVANZAR PANTALLAS


        if (OVRInput.GetDown(OVRInput.Button.One) && !gamePaused)
        {

            //Si pulsem el botó A quan el día a finalitzat, aleshores entrem a un nou día. 
            if (isEndOfTheDay)
            {


                selectMenuIniciCocinero();
                if (!isJustUnlockedLevel2 && !isJustUnlockedLevel3)
                {
                    isEndOfTheDay = false;
                    this.cardinalitatyController.generateCardinalities();
                }


            }
            else if (!isComandaShowingUp)
            {
                UIController.startOfTheDayMenu(0);
                orderGenerator.generarComanda();
                UIController.startOfTheDayMenu(3);
                UIController.orderMenu(orderGenerator.getComandaActual().img);
                isComandaShowingUp = true;
                    
            }
        }    

    }

    public void selectMenuIniciCocinero(){        
        if (isJustUnlockedLevel2){

            UIController.startOfTheDayMenuUnlockedSimpleConditionals(actualDay,iterTutorial);
            if (iterTutorial > 3){
                isJustUnlockedLevel2 = false;
                iterTutorial = 0;
                unlockLettuce();
            }else{
                iterTutorial += 1;
            }
        }
        else if (isJustUnlockedLevel3)
        {

            UIController.startOfTheDayMenuUnlockedDoubleConditionals(actualDay, iterTutorial);
            if (iterTutorial > 3)
            {
                isJustUnlockedLevel3 = false;
                iterTutorial = 0;
                unlockLettuce();
            }
            else
            {
                iterTutorial += 1;
            }
        }
        else if (isJustUnlockedLevel4){
            UIController.startOfTheDayMenuUnlockedIteratives(actualDay,iterTutorial);
            if (iterTutorial > 2){
                isJustUnlockedLevel4 = false;
                iterTutorial = 0;
            }else{
                iterTutorial += 1;
            }

        }
        else{
            UIController.startOfTheDayMenu(actualDay);   

        }


        if (!this.isUnlockedLevel2 && this.userControl.getFirstOrderChefAchievement()){
            this.isUnlockedLevel2 = true;
            orderGenerator.unlockNivells(2);
        }
        if (!this.isUnlockedLevel3 && this.userControl.getFirstConditionalIfOrderChefAchievement()){
            this.isUnlockedLevel3 = true;
            orderGenerator.unlockNivells(3);
        }

        if (!this.isUnlockedLevel4 && this.userControl.getFirstConditionalIfElseOrderChefAchievement())
        {
            this.isUnlockedLevel4 = true;
            orderGenerator.unlockNivells(4);
        }


    }

    public void unlockLettuce(){
        this.lettuceGenerator.SetActive(true);
        cardinalitatyController.unlockLettuce();
    }
    //Method to init the day. Active the new day text and generate the new cardinalities while deactive the end of the day flag
    public void startDay(int dia_actual)
    {
        UIController.startOfTheDayMenu(this.userControl.getCurrentDay());
        isEndOfTheDay = false;
    }

     public float calcularTemps(float temps)
     {
        temps -= Time.deltaTime; //Substracting the time from what remains of the day
        this.timeActualOrder += Time.deltaTime; //Adding the time to the order time

        updateReloj(temps);
        // If the time is less than 0 it means that the countdown for the day has ended.
        if (temps <= 0f)
        {
            UIController.endOfTheDayMenu(dayExp);//Displaying the end of the day text on the screen
            isEndOfTheDay = true; // Activating end of the day flag
            temps = 96f; // Restarting the time for the next day
            actualDay += 1; // Add actual day to go to the next day. 
            this.userControl.setCurrentDay(actualDay);
            totalExp += totalExp;
            this.userControl.setTotalExp((int)totalExp);
            isComandaShowingUp = false; // Deactivating order flag
            this.timeActualOrder = 0f; // Restarting order time
            removeAllObjects();
        }
        return temps; // Returning the updated time
     }  

     public void removeAllObjects(){
         GameObject[] allObjects = GameObject.FindGameObjectsWithTag("ingredient");
         foreach(GameObject go in allObjects){
             Destroy(go);
         }
     }


     public void updateReloj(float temps){
         this.actualHour = (int)((96 - temps)/12) + 9;
         UIController.updateTemps(this.actualHour +"");
     }

    //Method that allows to pause the game, activating the corresponding flag.
    private void pausarGame()
    {
        Time.timeScale = 0;
        this.gamePaused = true;
    }

    // Method that allows you to resume the game, disabling the corresponding flag.
    private void reanudarGame()
    {
        Time.timeScale = 1;
        this.gamePaused = false;
    }


    public void arriveComanda(ComandaChef comanda)
    {
        feedbackGenerator.setActualOrder(orderGenerator.getComandaActual()); // We define the command generated in the class that generates the feedback.
        (string,float) feedback = feedbackGenerator.getFeedback(comanda,this.timeActualOrder); // Get one string with feedback and a corresponding integer a score received. 
        UIController.showFeedback(feedback.Item1); // We show the feedback text message on the screen.

        if (feedback.Item2 > 1){
            if (this.orderGenerator.getComandaActual().type == "basic"){
                userControl.setNumBasicOrdersChef(userControl.getNumBasicOrdersChef() + 1);
                userControl.setNumOrdersChef(userControl.getNumOrdersChef() + 1);
            }
            else if (this.orderGenerator.getComandaActual().type == "conditionalIf")
            {
                userControl.setNumConditionalIfOrdersChef(userControl.getNumConditionalIfOrdersChef() + 1);
                userControl.setNumConditionalOrdersChef(userControl.getNumConditionalOrdersChef() + 1);
                userControl.setNumOrdersChef(userControl.getNumOrdersChef() + 1);
            }
            else if (this.orderGenerator.getComandaActual().type == "conditionalIfElse")
            {
                userControl.setNumConditionalIfElseOrdersChef(userControl.getNumConditionalIfElseOrdersChef() + 1);
                userControl.setNumConditionalOrdersChef(userControl.getNumConditionalOrdersChef() + 1);
                userControl.setNumOrdersChef(userControl.getNumOrdersChef() + 1);
            }
            else{
                userControl.setNumIterativeOrdersChef(userControl.getNumIterativeOrdersChef() + 1);
                userControl.setNumOrdersChef(userControl.getNumOrdersChef() + 1);
            }
        }



        if (this.userControl.getNumOrdersChef() == 1 && !this.userControl.getFirstOrderChefAchievement())
        {
            //Activating 1 orders achievement
            this.userControl.setFirstOrderChefAchievement(true);

        }
        if (this.userControl.getNumOrdersChef() == 10 && !this.userControl.getTenOrdersChefAchievement())
        {
            //Activating 10 orders achievement
            this.userControl.setTenOrdersChefAchievement(true);

        }
        else if (this.userControl.getNumOrdersChef() == 25 && !this.userControl.getTwentyfiveOrdersChefAchievement())
        {

            this.userControl.setTwentyfiveOrdersChefAchievement(true);
            //Activating 25 orders achievement

        }
        if (this.userControl.getNumOrdersChef() == 50 && !this.userControl.getFiftyOrdersChefAchievement())
        {
            //Activating 50 orders achievement
            this.userControl.setFiftyOrderChefAchievement(true);

        }
        else if (this.userControl.getNumOrdersChef() == 100 && !this.userControl.getHundredOrdersChefAchievement())
        {
            //Activating 100 orders achievement
            this.userControl.setHundredOrdersChefAchievement(true);
            

        }

        if (this.userControl.getNumBasicOrdersChef() == 1 && !this.userControl.getFirstBasicOrderChefAchievement())
        {
            //Activating 1 basic orders achievement
            this.userControl.setFirstBasicOrderChefAchievement(true);
 
        }
        else if (this.userControl.getNumBasicOrdersChef() == 10 && !this.userControl.getTenBasicOrdersChefAchievement())
        {
            //Activating 10 basic orders achievement
            this.userControl.setTenBasicOrdersChefAchievement(true);
            isJustUnlockedLevel2 = true;

        }
        else if(this.userControl.getNumBasicOrdersChef() == 25 && !this.userControl.getTwentyfiveBasicOrdersChefAchievement()){

            this.userControl.setTwentyfiveBasicOrdersChefAchievement(true);
            //Activating 25 basic orders achievement

        }

        else if (this.userControl.getNumConditionalOrdersChef() == 1 && !this.userControl.getFirstConditionalOrderChefAchievement())
        {

            this.userControl.setFirstConditionalOrderChefAchievement(true);
            //Activating 1 conditional order achievement

        }
        else if (this.userControl.getNumConditionalOrdersChef() == 10 && !this.userControl.getTenConditionalOrdersChefAchievement())
        {

            this.userControl.setTenConditionalOrdersChefAchievement(true);
            //Activating 10 conditional orders achievement

        }
        else if (this.userControl.getNumConditionalOrdersChef() == 50 && !this.userControl.getFiftyConditionalOrdersChefAchievement())
        {

            this.userControl.setFiftyConditionalOrdersChefAchievement(true);
            //Activating 50 conditional orders achievement

        }
        else if (this.userControl.getNumConditionalIfOrdersChef() == 1 && !this.userControl.getFirstConditionalIfOrderChefAchievement())
        {

            this.userControl.setFirstConditionalIfOrderChefAchievement(true);
            //Activating 1 basic conditional order achievement

        }
        else if (this.userControl.getNumConditionalIfOrdersChef() == 10 && !this.userControl.getTenConditionalIfOrdersChefAchievement())
        {

            this.userControl.setTenConditionalIfOrdersChefAchievement(true);
            isJustUnlockedLevel3 = true;
            //Activating 10 basic conditional orders achievement

        }
        else if (this.userControl.getNumConditionalIfOrdersChef() == 30 && !this.userControl.getThirtyConditionalIfOrdersChefAchievement())
        {

            this.userControl.setThirtyConditionalIfOrdersChefAchievement(true);
            //Activating 30 basic conditional orders achievement

        }
        else if(this.userControl.getNumConditionalIfElseOrdersChef() == 1 && !this.userControl.getFirstConditionalIfElseOrderChefAchievement()){

            this.userControl.setFirstConditionalIfElseOrderChefAchievement(true);
            //Activating 1 double conditional order achievement

        }
        else if(this.userControl.getNumConditionalIfElseOrdersChef() == 10 && !this.userControl.getTenConditionalIfElseOrdersChefAchievement()){
            isJustUnlockedLevel4 = true;
            this.userControl.setTenConditionalIfElseOrdersChefAchievement(true);
            //Activating 10 double conditional orders achievement

        }
        else if(this.userControl.getNumConditionalIfElseOrdersChef() == 30 && !this.userControl.getThirtyConditionalIfElseOrdersChefAchievement()){

            this.userControl.setThirtyConditionalIfElseOrdersChefAchievement(true);
            isJustUnlockedLevel4 = true;
            //Activating 30 double conditional orders achievement

        }
        else if(this.userControl.getNumIterativeOrdersChef() == 1 && !this.userControl.getFirstIterativeOrderChefAchievement()){

            //Activating 1 iterative order achievement
            this.userControl.setFirstIterativeOrderChefAchievement(true);    

        }else if(this.userControl.getNumIterativeOrdersChef() == 10 && !this.userControl.getTenIterativeOrdersChefAchievement()){

            //Activating 10 iterative orders achievement
            this.userControl.setTenIterativeOrdersChefAchievement(true);

        }else if(this.userControl.getNumIterativeOrdersChef() == 30 && !this.userControl.getThirtyIterativeOrdersChefAchievement()){

            this.userControl.setThirtyIterativeOrdersChefAchievement(true);
            //Activating 30 iterative orders achievement
        }
        dayExp += feedback.Item2; // Sumamos la experiencia de esta comanda a la experiencia del día.         
        isComandaShowingUp = false; // Desactivamos la flag de mostrar la comanda. 
        this.timeActualOrder = 0f; // Reiniciamos el tiempo. 
    }


    public ComandaContChef getComandaGenerator()
    {
        return this.orderGenerator;
    }

    public ControlCardinalitats getCardinalityController(){
        return this.cardinalitatyController;
    }

    public controladorMenus GetControladorMenus()
    {
        return this.UIController;
    }

    public UserController current_user(){
        return this.userControl;
    }

   

}
