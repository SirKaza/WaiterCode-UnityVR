using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
///  Clase principal del juego. Controla todas las variables y el loop principal del juego. 
///  Mediante un patrón singleton, el resto de clases pueden acceder a ella para consultar el estado actual del juego. 
/// </summary>
public class ControllerTutorial : MonoBehaviour
{
    
    private float timePerDay = 8f; // Variable para controlar la durabilidad de cada jornada. 
    private float timeComandaActual = 0f; // Variable para medir el tiempo en cocinar la comanda actual. 

    private float dayExp = 0f; // Experiencia acumulada en todo el día. 
    private float totalExp = 0f; // Experiencia acumulada en toda la partida. 

    private int actualDay = 1; // Dia actual de inicio del juego
    private int actualHour = 9; // Hora del día actual
    private bool isEndOfTheDay = true; // true => Estamos en los dialogos de final de día. 

    private bool gamePaused = false; // Variable para conocer si el juego esta pausado o no. 
    public bool isComandaShowingUp = false; // Variable para saber si una comanda está presente o no en la pantalla. 


    private int iterTutorial = 0;

    private GameObject menuPausa; // Menú de pausa del juego
    private ControllerMenuTutorial UIController;  // Clase controladora de los menús (entrar para ver mayor descripción). 
  

    private FeedbackGenerator feedbackGenerator; // Clase para generar el feedback de una comanda (entrar para ver mayor descripción). 
    private ComandaContChef orderGenerator; // Clase para generar y controlar las diferentes comandas (entrar para ver mayor descripción).
    private ControlCardinalitats controlCardinalities; 

    private Subject subject = new Subject();
    private AchievementsController achievementsController;
    // SINGLETON
    public static ControllerTutorial current;
    private UserController userControl;
    private ControllerSonidos soundControl;

    private GameObject lettuceGenerator; 

    int step = 0;
    private bool isNextStateBlocked;
    private bool isGeneratoringGrabbed;
    private void Awake()
    {
        current = this;
    }

    // Inicializamos todas las clases y los diferentes gameobjects que tengamos. 
    private void Start()
    {
        menuPausa = GameObject.Find("Canvas");
        UIController = this.GetComponentInParent<ControllerMenuTutorial>();
        //controladorMenus.menuIniciDeDia(dia_actual);

        feedbackGenerator = new FeedbackGenerator();
        orderGenerator = new ComandaContChef();
        controlCardinalities = new ControlCardinalitats();
        soundControl = GameObject.Find("ControllerSonidos").GetComponent<ControllerSonidos>();

        achievementsController = new AchievementsController();
        subject.AddObserver(achievementsController);

        this.userControl = GameObject.Find("UserControl").GetComponent<UserController>();
        this.lettuceGenerator = GameObject.Find("lettuce_generator");
        this.lettuceGenerator.SetActive(false);

        this.actualDay = this.userControl.getCurrentDay();
        this.totalExp = this.userControl.getTotalExp();
        this.isNextStateBlocked = false;
        this.isGeneratoringGrabbed = false;
        this.controlCardinalities.generateCardinalities();

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
            UIController.deactivatePauseMenu();
            gamePaused = false; 
        }

        // ACTIVACIÓN DEL MENÚ DE PAUSA
        
     
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            pausarGame();
            UIController.deactivatePauseMenu();
            gamePaused = true; 
        }

        //AVANZAR PANTALLAS

        
        if (OVRInput.GetDown(OVRInput.Button.One) && !gamePaused)
        {
            if (!isNextStateBlocked){
                if(soundControl.isEnded()){
                    GameObject.Find("UserControl").GetComponent<UserController>().tutorialChefCompleted();
                    DontDestroyOnLoad (GameObject.Find("UserControl"));
                    SceneManager.LoadScene("ChefScene");
                }
                soundControl.nextStep();

                if (soundControl.getStep()==6){
                    UIController.showControllers();
                    UIController.deactivateContinue();
                    isNextStateBlocked = true;
                }else if (soundControl.getStep()==12 || soundControl.getStep()==13 || soundControl.getStep()==15 || soundControl.getStep() ==17 || soundControl.getStep()==19){
                    UIController.deactivateContinue();
                    isNextStateBlocked = true;

                }else if(soundControl.getStep() >= 21 && soundControl.getStep()<=23){
                    UIController.showOrder();
                }                
                else{
                    UIController.restartScreen();
                }

            }
        }
    }

    public void isGenGrabbed(){
        this.isGeneratoringGrabbed = true;
        if (isNextStateBlocked && soundControl.getStep() ==6){
            isNextStateBlocked = false;
            UIController.deactivateContinue();
        }
    }
    public void dishesSelected(){
        if (isNextStateBlocked && soundControl.getStep() == 12){
            isNextStateBlocked = false;
            UIController.deactivateContinue();
        }
    }
    public void breadDownSelected(){
        if (isNextStateBlocked && soundControl.getStep() == 13){
            isNextStateBlocked = false;
            UIController.deactivateContinue();
        }
    }
    public void hamburguerSelected(){
        if (isNextStateBlocked && soundControl.getStep() == 15){
            isNextStateBlocked = false;
            UIController.deactivateContinue();
        }
    }
    public void breadUPSelected(){
        if (isNextStateBlocked && soundControl.getStep() == 17){
            isNextStateBlocked = false;
            UIController.deactivateContinue();
        }
    }
    public void sendHamburguerTutorial(){
        if (isNextStateBlocked && soundControl.getStep() == 19){
            isNextStateBlocked = false;
            UIController.activateContinue();
        }
    }
    public int getCurrentStep(){
        return soundControl.getStep();
    }

    public void unlockLettuce(){
        this.lettuceGenerator.SetActive(true);
        controlCardinalities.unlockLettuce();
    }
    //Metodo para iniciar el dia, activa el texto del nuevo día y genera las nuevas cardinalidades mientras que desactiva la flag de final de día. 
    public void startDay(int dia_actual)
    {
        UIController.startOfTheDayMenu(this.userControl.getCurrentDay());
        isEndOfTheDay = false;
    }

     public float calcularTemps(float temps)
     {
        temps -= Time.deltaTime; // Restamos el tiempo a lo que queda de día. 
        this.timeComandaActual += Time.deltaTime; // Sumamos el tiempo a lo que llevamos con la comanda. 

        updateReloj(temps);
        // Si el tiempo es menor que 0 quiere decir que la cuenta atrás del día ha finalizado. 
        if (temps <= 0f)
        {
            UIController.endOfTheDayMenu(dayExp);//Mostrem el menú que indica la finalització del dia. 
            isEndOfTheDay = true; // Activamos la flag que indica el final del día.
            temps = 8f; // Reiniciamos el tiempo para el día siguiente.
            actualDay += 1; // Incrementamos el día actual para avanzar al siguiente. 
            this.userControl.setCurrentDay(actualDay);
            totalExp += dayExp;
            this.userControl.setTotalExp((int)totalExp);
            isComandaShowingUp = false; // Desactivamos la flag de la comanda. 
            this.timeComandaActual = 0f; // Reiniciamos el iempo de la comanda.
        }
        return temps; // Retornamos el tiempo actualizado. 
     }  


     public void updateReloj(float temps){
         this.actualHour = (int)(8 - temps) + 9;
        UIController.updateTemps(this.actualHour +"");
     }

    //Metodo que permite pausar el juego, activando la flag correspondiente. 
    private void pausarGame()
    {
        Time.timeScale = 0;
        this.gamePaused = true;
    }

    //Metodo que permite reanudar el juego, desactivando la flag correspondiente. 
    private void reanudarGame()
    {
        Time.timeScale = 1;
        this.gamePaused = false;
    }


    //public void arriveComanda(ComandaChef comanda)
    //{
    //    feedbackGenerator.setComandaActual(comanda_generador.getComandaActual()); //Definimos la comanda generada en la clase que generará el feedback. 
    //    (string,float) feedback = feedbackGenerator.getFeedback(comanda,this.timeComandaActual); // Obtenemos una string con el feedback y un entero correspondiente a la puntuación recibida. 
    //    controladorMenus.mostrarFeedback(feedback.Item1); //Mostramos en pantalla el mensaje de texto del feedback. 
        
    //    if (feedback.Item2 > 1){
    //        if (this.comanda_generador.getComandaActual().tipo == "basico"){
    //            userControl.setNumComandesNormalsChef(userControl.getNumComandesNormalsChef() + 1);
    //        }else if (this.comanda_generador.getComandaActual().tipo == "condicional"){
    //            userControl.setNumComandesCondicionalsChef(userControl.getNumComandesCondicionals()Chef + 1);
    //        }else{
    //            userControl.setNumComandesIterativasChef(userControl.getNumComandesIterativas() + 1);
    //        }
    //    }
        



    //    if (this.userControl.getNumComandesNormals() == 1 && !this.userControl.getLogroPrimeraComanda())
    //    {

    //        this.userControl.setLogroPrimeraComanda(true);
 
    //    }else if(this.userControl.getNumComandesNormals() == 20 && !this.userControl.getLogroVintComanda()){
    //        this.userControl.setLogroVintComanda(true);
    //        //Activar logro 10 comandas normales
    //    }else if(this.userControl.getNumComandesNormals() == 40 && !this.userControl.getLogroQuarantaComanda()){
    //        this.userControl.setLogroQuarantaComanda(true);
    //        //Activar logro 40 comandas normales
    //    }else if(this.userControl.getNumComandesCondicionals() == 1 && !this.userControl.getLogroPrimeraCondicional()){
    //        this.userControl.setLogroPrimeraCondicional(true);
    //        //Activar logro 1 comandas condicional
    //    }else if(this.userControl.getNumComandesCondicionals() == 10 && !this.userControl.getLogroDeuCondicional()){
    //        this.userControl.setLogroDeuCondicional(true);

    //        //Activar logro 10 comandas condicional
    //    }else if(this.userControl.getNumComandesCondicionals() == 30 && !this.userControl.getLogroTrentaCondicional()){
    //        this.userControl.setLogroTrentaCondicional(true);

    //        //Activar logro 30 comandas condicionals
    //    }else if(this.userControl.getNumComandesIterativas() == 1 && !this.userControl.getLogroPrimeraIterativa()){
    //        //Activar logro 1 comandas iterativas
    //        this.userControl.setLogroPrimeraIterativa(true);    

    //    }else if(this.userControl.getNumComandesIterativas() == 10 && !this.userControl.getLogroDeuIterativa()){
    //        //Activar logro 10 comandas iterativas
    //        this.userControl.setLogroDeuIterativa(true);

    //    }else if(this.userControl.getNumComandesIterativas() == 30 && !this.userControl.getLogroTrentaIterativa()){
    //        this.userControl.setLogroTrentaIterativa(true);

    //        //Activar logro 30 comandas condicionals
    //    }
    //    exp_dia += feedback.Item2; // Sumamos la experiencia de esta comanda a la experiencia del día.         
    //    isComandaShowingUp = false; // Desactivamos la flag de mostrar la comanda. 
    //    this.timeComandaActual = 0f; // Reiniciamos el tiempo. 
    //}

    public ComandaContChef getComandaGenerator()
    {
        return this.orderGenerator;
    }


    public ControlCardinalitats getCardinalityController(){
        return this.controlCardinalities;
    }

    public ControllerMenuTutorial GetControladorMenus()
    {
        return this.UIController;
    }

    public UserController current_user(){
        return this.userControl;
    }

}
