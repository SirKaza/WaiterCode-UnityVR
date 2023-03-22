using Oculus.Platform.Samples.VrHoops;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;

/// <summary>
///  Clase principal del juego. Controla todas las variables y el loop principal del juego. 
///  Mediante un patrón singleton, el resto de clases pueden acceder a ella para consultar el estado actual del juego. 
/// </summary>
public class GameControllerWaiter : MonoBehaviour
{
    [SerializeField]
    private OVRPlayerController player;

    private float timePerDay = 194f; // Variable para controlar la durabilidad de cada jornada. 
    private float timeCurrentOrder = 0f; // Variable para medir el tiempo en cocinar la comanda actual. 

    private float dayExp = 0f; // Experiencia acumulada en todo el día. 
    private float totalExp = 0f; // Experiencia acumulada en toda la partida. 

    private int currentDay = 1; // Dia actual de inicio del juego
    private int currentHour = 9; // Hora del día actual
    private bool isEndOfTheDay = false; // true => Estamos en los dialogos de final de día. 

    private bool gamePaused = false; // Variable para conocer si el juego esta pausado o no. 
    private bool isNearTable = false; //Variable para saber si el jugador está dentro del collider de la mesa y puede "apuntar" el pedido del cliente
    private bool isComandaActive = false; //Variable para saber si en ese momento hay algun pedido activo
    private bool isDayStarted = false;

    private bool isUnlockedLevel2 = false;
    private bool isJustUnlockedLevel2 = false;

    private bool isWaiting = false;
    private bool isTableChoosed = false;

    private bool isUnlockedLevel3 = false;
    private bool isJustUnlockedLevel3 = false;

    private bool isUnlockedLevel4 = false;
    private bool isJustUnlockedLevel4 = false;

    private bool isUnlockedLevel5 = false;
    private bool isJustUnlockedLevel5 = false;

    private MenusControllerWaiter controllerMenus;  // Clase controladora de los menús (entrar para ver mayor descripción). 


    private FeedbackGeneratorWaiter feedbackGenerator; // Clase para generar el feedback de una comanda (entrar para ver mayor descripción). 
    private OrderContWaiter orderGenerator; // Clase para generar y controlar las diferentes comandas (entrar para ver mayor descripción).

    // SINGLETON
    public static GameControllerWaiter current;
    private UserController userControl;


    private GameObject textClient1;
    private GameObject textClient2;
    private GameObject textClient3;
    private GameObject textClient4;
    private GameObject textClient5;

    private GameObject table1;
    private GameObject table2;
    private GameObject table3;
    private GameObject table4;
    private GameObject table5;

    private List<GameObject> textsClient = new List<GameObject>();
    private List<GameObject> tables = new List<GameObject>();
    private int randomIndex;

    //Paneles para detectar su posición y saber donde los hemos de instanciar. No puedo quitar estas variables ya que necesito guardar su posición al principio de la partida

    private GameObject panel;
    private GameObject MeatPanel;
    private GameObject CheesePanel;
    private GameObject LettucePanel;
    private GameObject TopBreadPanel;
    private GameObject DownBreadPanel;
    private GameObject KetchupPanel;
    private GameObject ForTwoPanel;
    private GameObject IfElseMeatPlusThreePanel;
    private GameObject IfElseCheesePlusOnePanel;
    private GameObject IfElseLettucePlusThreePanel;
    private GameObject IfMeatPlusThreePanel;
    private GameObject IfCheesePlusOnePanel;
    private GameObject IfLettucePlusThreePanel;

    private Vector3 MeatPanelPosition;
    private Vector3 CheesePanelPosition;
    private Vector3 LettucePanelPosition;
    private Vector3 TopBreadPanelPosition;
    private Vector3 DownBreadPanelPosition;
    private Vector3 KetchupPanelPosition;
    private Vector3 ForTwoPanelPosition;
    private Vector3 IfElseMeatPlusThreePanelPosition;
    private Vector3 IfElseCheesePlusOnePanelPosition;
    private Vector3 IfElseLettucePlusThreePanelPosition;
    private Vector3 IfMeatPlusThreePanelPosition;
    private Vector3 IfCheesePlusOnePanelPosition;
    private Vector3 IfLettucePlusThreePanelPosition;

    private GameObject Button;

    private AudioClip ButtonSoundCorrect;
    private AudioClip ButtonSoundIncorrect;

    private AudioClip AchievementUnlockedClip;

    private Material redMatButton;
    private Material greenMatButton;
    private GameObject hatTv;
    private ParticleSystem smokeSystem;

    private GameObject arrowPointer;

    private void Awake()
    {
        current = this;
    }

    // Inicializamos todas las clases y los diferentes gameobjects que tengamos. 
    private void Start()
    {
        //Si es nuestro primer día, activamos el texto que nos guiará durante el primer pedido
        arrowPointer = GameObject.Find("Arrow");
        arrowPointer.SetActive(false);

        hatTv = GameObject.Find("hatTV");
        ButtonSoundCorrect = Resources.Load<AudioClip>("Audio/AplauseAudio");
        ButtonSoundIncorrect = Resources.Load<AudioClip>("Audio/ErrorAudio");
        redMatButton = Resources.Load<Material>("Materials/red");
        greenMatButton = Resources.Load<Material>("Materials/green");
        Button = GameObject.Find("MainButton");
        controllerMenus = this.GetComponentInParent<MenusControllerWaiter>();
        //controladorMenus.menuIniciDeDia(dia_actual);

        feedbackGenerator = new FeedbackGeneratorWaiter();
        orderGenerator = new OrderContWaiter();

        this.userControl = GameObject.Find("UserControl").GetComponent<UserController>();

        this.currentDay = this.userControl.getCurrentDay();
        this.totalExp = this.userControl.getTotalExp();
        //GameObject.Find("MusicControllerWaiter").GetComponent<musicController>().setVolume(this.userControl.getVolume());

        userControl.setDataCollection("Iniciando juego");

        textClient1 = GameObject.Find("TextClient1");
        textClient1.SetActive(false);
        textClient2 = GameObject.Find("TextClient2");
        textClient2.SetActive(false);
        textClient3 = GameObject.Find("TextClient3");
        textClient3.SetActive(false);
        textClient4 = GameObject.Find("TextClient4");
        textClient4.SetActive(false);
        textClient5 = GameObject.Find("TextClient5");
        textClient5.SetActive(false);

        textsClient.Add(textClient1);
        textsClient.Add(textClient2);
        textsClient.Add(textClient3);
        textsClient.Add(textClient4);
        textsClient.Add(textClient5);

        table1 = GameObject.Find("Table1");
        table1.GetComponent<BoxCollider>().enabled = false;
        table2 = GameObject.Find("Table2");
        table2.GetComponent<BoxCollider>().enabled = false;
        table3 = GameObject.Find("Table4");
        table3.GetComponent<BoxCollider>().enabled = false;
        table4 = GameObject.Find("Table4");
        table4.GetComponent<BoxCollider>().enabled = false;
        table5 = GameObject.Find("Table5");
        table5.GetComponent<BoxCollider>().enabled = false;

        tables.Add(table1);
        tables.Add(table2);
        tables.Add(table3);
        tables.Add(table4);
        tables.Add(table5);

        panel = GameObject.Find("BasicPanel");

        DownBreadPanel = GameObject.Find("DownBreadPanel");
        TopBreadPanel = GameObject.Find("TopBreadPanel");
        MeatPanel = GameObject.Find("MeatPanel");
        CheesePanel = GameObject.Find("CheesePanel");
        KetchupPanel = GameObject.Find("KetchupPanel");

        LettucePanel = GameObject.Find("LettucePanel");
        LettucePanel.SetActive(false);

        //Guardo las posiciones donde estan colocadas las instrucciones para luego crear los paneles ahí
        DownBreadPanelPosition = DownBreadPanel.transform.position;
        TopBreadPanelPosition = TopBreadPanel.transform.position;
        MeatPanelPosition = MeatPanel.transform.position;
        CheesePanelPosition = CheesePanel.transform.position;
        KetchupPanelPosition = KetchupPanel.transform.position;
        LettucePanelPosition = LettucePanel.transform.position;

        IfCheesePlusOnePanel = GameObject.Find("IfCheesePlusOnePanel");
        IfCheesePlusOnePanel.SetActive(false);
        IfLettucePlusThreePanel = GameObject.Find("IfLettucePlusThreePanel");
        IfLettucePlusThreePanel.SetActive(false);
        IfMeatPlusThreePanel = GameObject.Find("IfMeatPlusThreePanel");
        IfMeatPlusThreePanel.SetActive(false);


        IfCheesePlusOnePanelPosition = IfCheesePlusOnePanel.transform.position;
        IfLettucePlusThreePanelPosition = IfLettucePlusThreePanel.transform.position;
        IfMeatPlusThreePanelPosition = IfMeatPlusThreePanel.transform.position;

        IfElseCheesePlusOnePanel = GameObject.Find("IfElseCheesePlusOnePanel");
        IfElseCheesePlusOnePanel.SetActive(false);
        IfElseLettucePlusThreePanel = GameObject.Find("IfElseLettucePlusThreePanel");
        IfElseLettucePlusThreePanel.SetActive(false);
        IfElseMeatPlusThreePanel = GameObject.Find("IfElseMeatPlusThreePanel");
        IfElseMeatPlusThreePanel.SetActive(false);

        IfElseCheesePlusOnePanelPosition = IfElseCheesePlusOnePanel.transform.position;
        IfElseLettucePlusThreePanelPosition = IfElseLettucePlusThreePanel.transform.position;
        IfElseMeatPlusThreePanelPosition = IfElseMeatPlusThreePanel.transform.position;


        ForTwoPanel = GameObject.Find("ForTwoPanel");
        ForTwoPanel.SetActive(false);
        ForTwoPanelPosition = ForTwoPanel.transform.position;

        smokeSystem = GameObject.Find("SmokeSystem").GetComponent<ParticleSystem>();
        smokeSystem.Stop();

        
        if (userControl.getWaiterLevel() == 1)
        {
            controllerMenus.activeTutorialText(true);
            controllerMenus.activeButtonText(true);
        }

        else if(userControl.getWaiterLevel() == 2)
        {
            LettucePanel.SetActive(true);
            orderGenerator.unlocklevels(2);
        }
        else if (userControl.getWaiterLevel() == 3)
        {
            LettucePanel.SetActive(true);
            IfCheesePlusOnePanel.SetActive(true);
            IfLettucePlusThreePanel.SetActive(true);
            IfMeatPlusThreePanel.SetActive(true);
            orderGenerator.unlocklevels(2);
            orderGenerator.unlocklevels(3);
        }
        else if (userControl.getWaiterLevel() == 4)
        {
            LettucePanel.SetActive(true);
            IfCheesePlusOnePanel.SetActive(true);
            IfLettucePlusThreePanel.SetActive(true);
            IfMeatPlusThreePanel.SetActive(true);
            IfElseCheesePlusOnePanel.SetActive(true);
            IfElseLettucePlusThreePanel.SetActive(true);
            IfElseMeatPlusThreePanel.SetActive(true);
            orderGenerator.unlocklevels(2);
            orderGenerator.unlocklevels(3);
            orderGenerator.unlocklevels(4);
        }
        else if (userControl.getWaiterLevel() >4)
        {
            LettucePanel.SetActive(true);
            IfCheesePlusOnePanel.SetActive(true);
            IfLettucePlusThreePanel.SetActive(true);
            IfMeatPlusThreePanel.SetActive(true);
            IfElseCheesePlusOnePanel.SetActive(true);
            IfElseLettucePlusThreePanel.SetActive(true);
            IfElseMeatPlusThreePanel.SetActive(true);
            ForTwoPanel.SetActive(true);
            orderGenerator.unlocklevels(2);
            orderGenerator.unlocklevels(3);
            orderGenerator.unlocklevels(4);
            orderGenerator.unlocklevels(5);
        }


    }

    private void Update()
    {
        if (isDayStarted && !gamePaused)
        {
            this.timePerDay = calculateTime(timePerDay);
        }
        else if (OVRInput.GetDown(OVRInput.Button.Two) && gamePaused)
        {
            restartGame();
            controllerMenus.deactivatePauseMenu();
            gamePaused = false;
        }


        //ACTIVACIÓN DEL MENÚ DE PAUSA
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            pauseGame();
            controllerMenus.showPauseMenu();
            gamePaused = true;
        }


        if (OVRInput.GetDown(OVRInput.Button.One) && !gamePaused)
        {
            //Si pulsem el botó A quan el día a finalitzat, aleshores entrem a un nou día.
            if (isEndOfTheDay)
            {
                safeUser();
                selectMenuIniciWaiter();
                //Aquí deberíamos volver a activar el botón

            }
            if (!isComandaActive  && !isNearTable)
            {

                if (!isDayStarted)
                {
                    isDayStarted = true;
                }

                //Este if sirve para que no podamos ir clicando la A hasta que se active la mesa que queramos
                if (!isTableChoosed)
                {
                    //Escogemos una mesa random para así tenerla fijada hasta que no hayamos acabado el pedido
                    //Activamos el collider de la mesa random
                    var random = new System.Random();
                    randomIndex = random.Next(textsClient.Count);
                    textsClient[randomIndex].SetActive(true);
                    textsClient[randomIndex].GetComponent<TextMeshProUGUI>().SetText("Here!");
                    textsClient[randomIndex].GetComponent<TextMeshProUGUI>().fontSize = 0.15f;
                    tables[randomIndex].GetComponent<BoxCollider>().enabled = true;
                    isTableChoosed = true;
                    controllerMenus.activeTutorialText(false);

                }


            }

        }
        if (isNearTable && !isComandaActive)
        {

            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Audio/orderSound"), player.transform.position, 1.0f);
            if (userControl.getCurrentDay() == 1)
            {
                arrowPointer.SetActive(true);
            }
            orderGenerator.generateOrder();
            textsClient[randomIndex].GetComponent<TextMeshProUGUI>().fontSize = 0.08f;
            textsClient[randomIndex].GetComponent<TextMeshProUGUI>().SetText(orderGenerator.getActualOrder().sentence);
            controllerMenus.orderMenuWaiter(orderGenerator.getActualOrder().sentence);
            tables[randomIndex].GetComponent<BoxCollider>().enabled = false;
            isComandaActive = true;
            isNearTable = false;

        }

    }

    public void selectMenuIniciWaiter()
    {

        if (isJustUnlockedLevel2)
        {

            controllerMenus.menuStartOfTheDayUnlockedLettuce(currentDay);
            unlockLettuce();
            isJustUnlockedLevel2 = false;
            controllerMenus.showImageSmall(true, "Lettuce");
            isEndOfTheDay = false;

        }

        else if (isJustUnlockedLevel3)
        {

            controllerMenus.menuStartOfTheDayUnlockedCondicionalsIf(currentDay);
            //Activamos los paneles condicionales
            unlockConditionalsIf();
            controllerMenus.showImageBig(true, "CheeseHigherOne");
            isJustUnlockedLevel3 = false;
            isEndOfTheDay = false;

        }
        else if (isJustUnlockedLevel4)
        {

            controllerMenus.menuStartOfTheDayUnlockedCondicionalsIfElse(currentDay);
            //Activamos los paneles condicionales
            unlockConditionalsIfElse();
            controllerMenus.showImageBig(true, "BasicIf1");
            isJustUnlockedLevel4 = false;
            isEndOfTheDay = false;


        }
        else if (isJustUnlockedLevel5)
        {
            controllerMenus.menuIniciDeDiaUnlockedBucle(currentDay);

            //Desbloqueamos los paneles iterativos
            unlockIteratives();
            controllerMenus.showImageSmall(true, "ForOrder0");
            isJustUnlockedLevel5 = false;
            isEndOfTheDay = false;

        }
        else
        {
            controllerMenus.menuStartOfTheDay(currentDay);
            isEndOfTheDay = false;
        }


        if(!this.isUnlockedLevel2 && this.userControl.getFirstBasicOrderWaiterAchievement())
        {
            //Añadimos los pedidos condicionales a los posibles pedidos
            this.isUnlockedLevel2 = true;
            orderGenerator.unlocklevels(2);
        }

        if (!this.isUnlockedLevel3 && this.userControl.getTwentyfiveOrdersWaiterAchievement())
        {
            //Añadimos los pedidos condicionales a los posibles pedidos
            this.isUnlockedLevel3 = true;
            orderGenerator.unlocklevels(3);
        }

        if (!this.isUnlockedLevel4 && this.userControl.getFirstConditionalIfOrderWaiterAchievement())
        {
            //Añadimos los pedidos condicionales a los posibles pedidos
            this.isUnlockedLevel4 = true;
            orderGenerator.unlocklevels(4);
        }
        if (!this.isUnlockedLevel5 && this.userControl.getFirstConditionalIfElseOrderWaiterAchievement())
        {
            //Añadimos los pedidos iterativos a los posibles pedidos
            this.isUnlockedLevel5 = true;
            orderGenerator.unlocklevels(5);
        }


    }

    public void unlockLettuce()
    {
        LettucePanel.SetActive(true);
    }
    public void unlockConditionalsIfElse()
    {
        IfElseCheesePlusOnePanel.SetActive(true);
        IfElseLettucePlusThreePanel.SetActive(true);
        IfElseMeatPlusThreePanel.SetActive(true);
    }

    public void unlockConditionalsIf()
    {
        IfCheesePlusOnePanel.SetActive(true);
        IfLettucePlusThreePanel.SetActive(true);
        IfMeatPlusThreePanel.SetActive(true);
    }
    public void unlockIteratives()
    {
        ForTwoPanel.SetActive(true);
    }
    //Metodo para iniciar el dia, activa el texto del nuevo día y genera las nuevas cardinalidades mientras que desactiva la flag de final de día. 
    public float calculateTime(float temps)
    {
        temps -= Time.deltaTime; // Restamos el tiempo a lo que queda de día. 
        this.timeCurrentOrder += Time.deltaTime; // Sumamos el tiempo a lo que llevamos con la comanda. 

        updateClock(temps);
        // Si el tiempo es menor que 0 quiere decir que la cuenta atrás del día ha finalizado. 
        if (temps <= 0f)
        {
            isTableChoosed = false;
            textsClient[randomIndex].SetActive(false);
            tables[randomIndex].GetComponent<BoxCollider>().enabled = false;
            controllerMenus.orderMenuWaiter("");
            isComandaActive = false;
            deleteInstructions();

            //Aquí deberíamos bloquear el botón hasta que volvamos a clicar A

            controllerMenus.menuEndOfTheDay(dayExp);//Mostrem el menú que indica la finalització del dia. 
            isEndOfTheDay = true; // Activamos la flag que indica el final del día.
            temps = 96f; // Reiniciamos el tiempo para el día siguiente.
            currentDay += 1; // Incrementamos el día actual para avanzar al siguiente. 
            this.userControl.setCurrentDay(currentDay);
            totalExp += dayExp;
            this.userControl.setTotalExp((int)totalExp);
            this.timeCurrentOrder = 0f; // Reiniciamos el tiempo de la comanda.
        }
        return temps; // Retornamos el tiempo actualizado. 
    }

    public void updateClock(float temps)
    {
        this.currentHour = (int)((96 - temps) / 12) + 9;
        controllerMenus.updateTemps(this.currentHour + "");
    }

    //Metodo que permite pausar el juego, activando la flag correspondiente. 
    private void pauseGame()
    {
        Time.timeScale = 0;
        this.gamePaused = true;
    }

    //Metodo que permite reanudar el juego, desactivando la flag correspondiente. 
    private void restartGame()
    {
        Time.timeScale = 1;
        this.gamePaused = false;
    }

    public void deleteInstructions()
    {
        //Cuando enviamos el pedido y este es correcto destruimos las instrucciones que hemos utilizado y eliminamos el pedido actual.
        panel = GameObject.Find("BasicPanel");
        panel.GetComponent<AttInstructions>().deleteHamburguer();

    }

    public void arriveOrder(OrderWaiter order)
    {

        //Este if lo utilizamos por si el día acaba a mitad del pedido ("explicado" en el tutorial)     
        if (isComandaActive)
        {
            feedbackGenerator.setComandaActual(orderGenerator.getActualOrder()); //Definimos la comanda generada en la clase que generará el feedback. 
            (string, float, bool) feedback = feedbackGenerator.getFeedback(order, this.timeCurrentOrder); // Obtenemos una string con el feedback, un entero correspondiente a la puntuación recibida y un booleano para saber si la entrega ha sido correcta
            controllerMenus.mostrarFeedback(feedback.Item1); //Mostramos en pantalla el mensaje de texto del feedback. 

            if (!feedback.Item3)
            {
                //El pedido ha sido incorrecto, ponemos el mensaje por pantalla y hacemos que vuelva a salir el pedido que hay que realizar
                AudioSource.PlayClipAtPoint(ButtonSoundIncorrect, Button.transform.position, 1.0f);
                OVRInput.SetControllerVibration(5, 200, OVRInput.Controller.RTouch);
                OVRInput.SetControllerVibration(5, 200, OVRInput.Controller.LTouch);
                Button.GetComponent<MeshRenderer>().material = redMatButton;
                smokeSystem.Play();
                StartCoroutine("Waiting");
                StartCoroutine("Again");
            }
            else if (feedback.Item3)
            {
                Button.GetComponent<MeshRenderer>().material = greenMatButton;
                AudioSource.PlayClipAtPoint(ButtonSoundCorrect, Button.transform.position, 1.0f);
                deleteInstructions();
                isComandaActive = false; // Desactivamos la flag de mostrar la comanda.
                isTableChoosed = false;
                textsClient[randomIndex].SetActive(false);
                
            }


            if (feedback.Item2 > 1)
            {
                if (this.orderGenerator.getActualOrder().level == "1" || this.orderGenerator.getActualOrder().level == "2")
                {
                    userControl.setNumBasicOrdersWaiter(userControl.getNumBasicOrdersWaiter() + 1);
                    userControl.setNumOrdersWaiter(userControl.getNumOrdersWaiter() + 1);
                }
                else if (this.orderGenerator.getActualOrder().level == "3")
                {
                    userControl.setNumConditionalIfOrdersWaiter(userControl.getNumConditionalIfOrdersWaiter() + 1);
                    userControl.setNumOrdersWaiter(userControl.getNumOrdersWaiter() + 1);
                    userControl.setNumConditionalOrdersWaiter(userControl.getNumConditionalOrdersWaiter() + 1);
                }
                
                else if (this.orderGenerator.getActualOrder().level == "4")
                {
                    userControl.setNumConditionalIfElseOrdersWaiter(userControl.getNumConditionalIfElseOrdersWaiter() + 1);
                    userControl.setNumOrdersWaiter(userControl.getNumOrdersWaiter() + 1);
                    userControl.setNumConditionalOrdersWaiter(userControl.getNumConditionalOrdersWaiter() + 1);
                }
                else
                {
                    userControl.setNumIterativeOrdersWaiter(userControl.getNumIterativeOrdersWaiter() + 1);
                    userControl.setNumOrdersWaiter(userControl.getNumOrdersWaiter() + 1);
                }


                if (this.userControl.getNumBasicOrdersWaiter() == 1 && !this.userControl.getFirstOrderWaiterAchievement())
                {
                    this.userControl.setFirstOrderWaiterAchievement(true);
                    isJustUnlockedLevel2 = true;
                    AchievementUnlockedClip = Resources.Load<AudioClip>("Audio/FirstBasicOrderAudio");
                    AudioSource.PlayClipAtPoint(AchievementUnlockedClip, Button.transform.position, 1.0f);
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: First Basic Order Done! Congratulations, keep going on");
                    controllerMenus.showImageBig(true, "LogroNormal2");
                    controllerMenus.activeButtonText(false);
                    arrowPointer.SetActive(false);

                    hatTv.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/green");
                }
                else if (this.userControl.getNumBasicOrdersWaiter() == 10 && !this.userControl.getTwentyfiveBasicOrdersWaiterAchievement())
                {
                    isJustUnlockedLevel3 = true;
                    this.userControl.setTwentyfiveBasicOrdersWaiterAchievement(true);
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Ten Basic Orders Done! Congratulations, level up!");

                    controllerMenus.showImageBig(true, "LogroNormal3");

                    hatTv.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/blue");
                    //Activar logro 20 comandas normales
                }

                else if (this.userControl.getNumConditionalIfOrdersWaiter() == 1 && !this.userControl.getFirstConditionalIfOrderWaiterAchievement())
                {
                    this.userControl.setFirstConditionalIfOrderWaiterAchievement(true);
                    isJustUnlockedLevel4 = true;
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: First Basic Conditional Order Done! Congratulations, leveled up!");
                    controllerMenus.showImageBig(true, "LogroCondicional1");

                    hatTv.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/red");

                    //Activar logro 1 comandas condicional
                }
                else if (this.userControl.getNumConditionalIfOrdersWaiter() == 10 && !this.userControl.getTenConditionalIfOrdersWaiterAchievement())
                {
                    this.userControl.setTenConditionalIfOrdersWaiterAchievement(true);
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Ten Basic Conditional Orders Done! Congratulations, keep it up!");
                    controllerMenus.showImageBig(true, "LogroCondicional2");
                    //Activar logro 10 comandas condicional
                }
                else if (this.userControl.getNumConditionalIfOrdersWaiter() == 30 && !this.userControl.getThirtyConditionalIfOrdersWaiterAchievement())
                {

                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Thirty Basic Conditional Orders Done! Congratulations, keep it up!");
                    this.userControl.setThirtyConditionalIfOrdersWaiterAchievement(true);
                    controllerMenus.showImageBig(true, "LogroCondicional3");
                    //Activar logro 30 comandas condicionals
                }
                else if (this.userControl.getNumConditionalIfElseOrdersWaiter() == 1 && !this.userControl.getFirstConditionalIfElseOrderWaiterAchievement())
                {
                    this.userControl.setFirstConditionalIfElseOrderWaiterAchievement(true);
                    isJustUnlockedLevel5 = true;
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: First Double Conditional Order Done! Congratulations, leveled up!");
                    controllerMenus.showImageBig(true, "LogroCondicional1");

                    hatTv.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/gold");
                    //Activar logro 1 comandas condicional
                }
                else if (this.userControl.getNumConditionalIfElseOrdersWaiter() == 10 && !this.userControl.getTenConditionalIfElseOrdersWaiterAchievement())
                {
                    this.userControl.setTenConditionalIfElseOrdersWaiterAchievement(true);
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Ten Double Conditional Orders Done! Congratulations, keep it up!");
                    controllerMenus.showImageBig(true, "LogroCondicional2");
                    //Activar logro 10 comandas condicional
                }
                else if (this.userControl.getNumConditionalIfElseOrdersWaiter() == 30 && !this.userControl.getThirtyConditionalIfElseOrdersWaiterAchievement())
                {

                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Thirty Double Conditional Order Done! Congratulations, keep it up!");
                    this.userControl.setThirtyConditionalIfElseOrdersWaiterAchievement(true);
                    controllerMenus.showImageBig(true, "LogroCondicional3");
                    //Activar logro 30 comandas condicionals
                }
                else if (this.userControl.getNumIterativeOrdersWaiter() == 1 && !this.userControl.getFirstConditionalIfElseOrderWaiterAchievement())
                {

                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: First Iterative Order Done! Congratulations, keep it up!");
                    controllerMenus.showImageBig(true, "LogroFOR1");
                    //Activar logro 1 comandas iterativas
                    this.userControl.setFirstConditionalIfElseOrderWaiterAchievement(true);

                }
                else if (this.userControl.getNumIterativeOrdersWaiter() == 10 && !this.userControl.getTenConditionalIfElseOrdersWaiterAchievement())
                {
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Ten Iterative Orders Done! Congratulations, keep it up!");
                    controllerMenus.showImageBig(true, "LogroFOR2");
                    //Activar logro 10 comandas iterativas
                    this.userControl.setTenIterativeOrdersWaiterAchievement(true);

                }
                else if (this.userControl.getNumIterativeOrdersWaiter() == 30 && !this.userControl.getThirtyConditionalIfElseOrdersWaiterAchievement())
                {
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Thirty Iterative Orders Done! Congratulations, keep it up!");
                    this.userControl.setThirtyConditionalIfElseOrdersWaiterAchievement(true);
                    controllerMenus.showImageBig(true, "LogroFOR3");
                    //Activar logro 30 comandas condicionals
                }
                dayExp += feedback.Item2; // Sumamos la experiencia de esta comanda a la experiencia del día.         

                this.timeCurrentOrder = 0f; // Reiniciamos el tiempo del pedido. 
            }

        }
        
    }

    public void createPanel(string name)
    {
        //Creamos otro panel de la instrucción cada vez que incorporamos uno al panel. Habría que hacerlo también cuando toca el suelo
        //Hemos creado una variable para cada panel para así poder adjudicar el nombre cuando lo instanciamos ya que aunque el prefab se llamase como debía, cuando se instanciaba no lo detectaba
        if (name == "CheesePanel")
        {
            CheesePanel = Instantiate(Resources.Load("Panels/CheesePanel") as GameObject, CheesePanelPosition, Quaternion.identity);
            CheesePanel.name = "CheesePanel";

        }
        else if (name == "MeatPanel")
        {
            MeatPanel = Instantiate(Resources.Load("Panels/MeatPanel") as GameObject, MeatPanelPosition, Quaternion.identity);
            MeatPanel.name = "MeatPanel";

        }
        else if (name == "LettucePanel")

        {
            LettucePanel = Instantiate(Resources.Load("Panels/LettucePanel") as GameObject, LettucePanelPosition, Quaternion.identity);
            LettucePanel.name = "LettucePanel";

        }
        else if (name == "TopBreadPanel")
        {
            TopBreadPanel = Instantiate(Resources.Load("Panels/TopBreadPanel") as GameObject, TopBreadPanelPosition, Quaternion.identity);
            TopBreadPanel.name = "TopBreadPanel";

        }
        else if (name == "DownBreadPanel")
        {
            DownBreadPanel = Instantiate(Resources.Load("Panels/DownBreadPanel") as GameObject, DownBreadPanelPosition, Quaternion.identity);
            DownBreadPanel.name = "DownBreadPanel";

        }

        else if (name == "KetchupPanel")
        {
            KetchupPanel = Instantiate(Resources.Load("Panels/KetchupPanel") as GameObject, KetchupPanelPosition, Quaternion.identity);
            KetchupPanel.name = "KetchupPanel";

        }
        else if (name == "IfLettucePlusThreePanel")
        {
            IfLettucePlusThreePanel = Instantiate(Resources.Load("Panels/IfLettucePlusThreePanel") as GameObject, IfLettucePlusThreePanelPosition, Quaternion.identity);
            IfLettucePlusThreePanel.name = "IfElseLettucePlusThreePanel";
        }


        else if (name == "IfCheesePlusOnePanel")
        {
            IfCheesePlusOnePanel = Instantiate(Resources.Load("Panels/IfCheesePlusOnePanel") as GameObject, IfCheesePlusOnePanelPosition, Quaternion.identity);
            IfCheesePlusOnePanel.name = "IfElseCheesePlusOnePanel";

        }


        else if (name == "IfMeatPlusThreePanel")
        {
            IfMeatPlusThreePanel = Instantiate(Resources.Load("Panels/IfMeatPlusThreePanel") as GameObject, IfMeatPlusThreePanelPosition, Quaternion.identity);
            IfMeatPlusThreePanel.name = "IfMeatPlusThreePanel";
            
        }

        else if (name == "IfElseLettucePlusThreePanel")
        {
            IfElseLettucePlusThreePanel = Instantiate(Resources.Load("Panels/IfElseLettucePlusThreePanel") as GameObject, IfElseLettucePlusThreePanelPosition, Quaternion.identity);
            IfElseLettucePlusThreePanel.name = "IfElseLettucePlusThreePanel";
        }


        else if (name == "IfElseCheesePlusOnePanel")
        {
            IfElseCheesePlusOnePanel = Instantiate(Resources.Load("Panels/IfElseCheesePlusOnePanel") as GameObject, IfElseCheesePlusOnePanelPosition, Quaternion.identity);
            IfElseCheesePlusOnePanel.name = "IfElseCheesePlusOnePanel";

        }


        else if (name == "IfElseMeatPlusThreePanel")
        {
            IfElseMeatPlusThreePanel = Instantiate(Resources.Load("Panels/IfElseMeatPlusThreePanel") as GameObject, IfElseMeatPlusThreePanelPosition, Quaternion.identity);
            IfElseMeatPlusThreePanel.name = "IfElseMeatPlusThreePanel";
;
        }

        else if (name == "ForTwoPanel")
        {
            ForTwoPanel = Instantiate(Resources.Load("Panels/ForTwoPanel") as GameObject, ForTwoPanelPosition, Quaternion.identity);
            ForTwoPanel.name = "ForTwoPanel";
        }

    }

    public void setIsNearTable(bool isNearTable)
    {
        this.isNearTable = isNearTable;
    }

    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(3);
        isWaiting = false;
    }


    IEnumerator Again()
    {
        while (isWaiting)
        {
            yield return new WaitForSeconds(0.1f);
        }
        smokeSystem.Stop();
        controllerMenus.orderMenuWaiter(orderGenerator.getActualOrder().sentence);
    }
    public UserController current_user()
    {
        return this.userControl;
    }

    public bool getIsComandaActive()
    {
        return isComandaActive;
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
    }




}
