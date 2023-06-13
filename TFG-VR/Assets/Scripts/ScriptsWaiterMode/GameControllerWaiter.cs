using Oculus.Platform.Samples.VrHoops;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UI;
using Newtonsoft.Json;

/// <summary>
///  Clase principal del juego. Controla todas las variables y el loop principal del juego. 
///  Mediante un patrón singleton, el resto de clases pueden acceder a ella para consultar el estado actual del juego. 
/// </summary>
[Serializable] //Rasa
public class MessageSenderRasa
{
    public string sender, message;
}
public class MessageRasa //Rasa
{
    public string recipient_id, text, image;
}
public class GameControllerWaiter : MonoBehaviour
{
    [SerializeField]
    private OVRPlayerController player;

    private float timePerDay = 144f; // Variable para controlar la durabilidad de cada jornada. 
    // private float timePerDay = 10f; // testing rasa
    private float timeCurrentOrder = 0f; // Variable para medir el tiempo en cocinar la comanda actual. 
    private float actualTimeDay = 144f;  // actualizar valor timePerDay (144f/194f y 96f)

    private float dayExp = 0f; // Experiencia acumulada en todo el día. 
    private float totalExp = 0f; // Experiencia acumulada en toda la partida. 

    private int currentDay = 1; // Dia actual de inicio del juego
    private int currentHour = 9; // Hora del día actual
    private int lastHour = 9; // Hora anterior
    private bool isEndOfTheDay = false; // true => Estamos en los dialogos de final de día. 

    private bool gamePaused = false; // Variable para conocer si el juego esta pausado o no. 
    private bool isNearTable = false; //Variable para saber si el jugador está dentro del collider de la mesa y puede "apuntar" el pedido del cliente
    private bool isComandaActive = false; //Variable para saber si en ese momento hay algun pedido activo
    private bool isDayStarted = false;
    private bool deliverTry = false; //Variable para saber si el jugador ha intentado entregar la comanda

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

    private GameObject sun; // Variables para controlar la rotación del sol.
    private Quaternion sunRotation; 

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

    private GameObject subtable1;
    private GameObject subtable2;
    private GameObject subtable3;
    private GameObject subtable4;
    private GameObject subtable5;

    private List<GameObject> textsClient = new List<GameObject>();
    private List<int> chosenIndexes = new List<int>(); // Crear una lista para mantener los índices ya seleccionados
    private List<GameObject> tables = new List<GameObject>();
    private List<GameObject> subtables = new List<GameObject>();
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

    private GameObject Cola;
    private GameObject Beer;
    private GameObject Water;
    public string [] extras = { "Cola", "Beer", "Water" }; // list of extras apart of Hamburger

    public GameObject Tray;

    private List<List<GameObject>> tray_items = new List<List<GameObject>>(); //Lista de listas de objetos que contiene los objetos que hay en cada bandeja para cada mesa
    private List<GameObject> tray_items1 = new List<GameObject>();
    private List<GameObject> tray_items2 = new List<GameObject>();
    private List<GameObject> tray_items3 = new List<GameObject>();
    private List<GameObject> tray_items4 = new List<GameObject>();
    private List<GameObject> tray_items5 = new List<GameObject>();

    public bool delivering = false; //publica porque la usa el script de DestroyTray
    public bool recreatingDeliver = false; 

    private Vector3 localScaleOriginal = new Vector3(0.2f, 0.05f, 1.5f); // La escala original de los paneles

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

    private Vector3 ColaPosition;
    private Vector3 BeerPosition;
    private Vector3 WaterPosition;

    public Vector3 TrayPosition;

    private GameObject Button;
    private ButtonPushClick ButtonPushClick;
    private Vector3 ButtonPosition;

    private AudioClip ButtonSoundCorrect;
    private AudioClip ButtonSoundIncorrect;
    private AudioClip DeliverSound;
    private AudioClip AchievementUnlockedClip;

    private Material redMatButton;
    private Material greenMatButton;
    private Material blueMatButton;
    private GameObject hatTv;
    private ParticleSystem smokeSystem;

    private GameObject arrowPointer;
    private GameObject tutorialText;
    private GameObject buttonText;
    private GameObject deliverText;

    //RASA part -------------------------
    // public TxtToSpeech tts1; // se queria hacer un array de tts para cada mesa
    // public TxtToSpeech tts3;
    // public TxtToSpeech tts4;
    // public TxtToSpeech tts5;
    //private List<TxtToSpeech> tts = new List<TxtToSpeech>();

    private string text;
    public TextMeshProUGUI displayIncomingText;
    public TextMeshProUGUI inputField;
    //public TextMeshProUGUI displayOutgoingText;    

    public static int counter;
    public string tmp;
    
    public string[] names ;
    public bool trigger;
    public bool saved;
    public bool paused;
    public bool restart;
    //public bool timeoutTrigger;
    //public TextToSpeech tts;
    public TxtToSpeech tts2;
    public int time = 0; 

    private bool firstTime;
    private bool rasaFinish;
    private bool onlyOnce;

    private void Awake()
    {
        current = this;
    }

    // Inicializamos todas las clases y los diferentes gameobjects que tengamos. 
    private void Start()
    {   
        // RASA Part
        tts2= GetComponent<TxtToSpeech>();
        names = new string[] {"hey","restore","q1 ", "q2 ", "q3 ","q4 "
        ,"q5 ","q6 ","q7 ",
        "q8 ","q9 ","q10 ","q11 ","q12 ","q13 ","q14 ","q15 q15 ", "q16 q16 "};
        counter = 0;
        tmp = " ";        
        trigger = false;
        saved = false;
        paused = false;
        restart = false;
        firstTime = true;
        rasaFinish = false;
        onlyOnce = false;
        //timeoutTrigger = false;

        // tts1 = GetComponent<TxtToSpeech>();
        // tts3 = GetComponent<TxtToSpeech>();
        // tts4 = GetComponent<TxtToSpeech>();
        // tts5 = GetComponent<TxtToSpeech>();
        // tts.Add(tts1);
        // tts.Add(tts2);
        // tts.Add(tts3);
        // tts.Add(tts4);
        // tts.Add(tts5);

        //Si es nuestro primer día, activamos el texto que nos guiará durante el primer pedido
        arrowPointer = GameObject.Find("Arrow");
        arrowPointer.SetActive(false);
        tutorialText = GameObject.Find("CanvasTextStart");
        tutorialText.SetActive(false);
        buttonText = GameObject.Find("ButtonText");
        buttonText.SetActive(false);
        deliverText = GameObject.Find("DeliverText");
        deliverText.SetActive(false);

        hatTv = GameObject.Find("hatTV");
        ButtonSoundCorrect = Resources.Load<AudioClip>("Audio/AplauseAudio");
        ButtonSoundIncorrect = Resources.Load<AudioClip>("Audio/ErrorAudio");
        DeliverSound = Resources.Load<AudioClip>("Audio/deliverSound");
        redMatButton = Resources.Load<Material>("Materials/red");
        greenMatButton = Resources.Load<Material>("Materials/green");
        blueMatButton = Resources.Load<Material>("Materials/blue");
        Button = GameObject.Find("MainButton");
        ButtonPosition = Button.transform.position;
        sun = GameObject.Find("Sun");
        sunRotation = sun.transform.rotation;
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

        table1 = GameObject.Find("Table1Waiter");
        table1.GetComponent<BoxCollider>().enabled = false;
        table2 = GameObject.Find("Table2Waiter");
        table2.GetComponent<BoxCollider>().enabled = false;
        table3 = GameObject.Find("Table3Waiter");
        table3.GetComponent<BoxCollider>().enabled = false;
        table4 = GameObject.Find("Table4Waiter");
        table4.GetComponent<BoxCollider>().enabled = false;
        table5 = GameObject.Find("Table5Waiter");
        table5.GetComponent<BoxCollider>().enabled = false;

        tables.Add(table1);
        tables.Add(table2);
        tables.Add(table3);
        tables.Add(table4);
        tables.Add(table5);

        subtable1 = GameObject.Find("Table1");
        subtable2 = GameObject.Find("Table2");
        subtable3 = GameObject.Find("Table3");
        subtable4 = GameObject.Find("Table4");
        subtable5 = GameObject.Find("Table5");

        subtables.Add(subtable1);
        subtables.Add(subtable2);
        subtables.Add(subtable3);
        subtables.Add(subtable4);
        subtables.Add(subtable5);

        tray_items.Add(tray_items1); // añade cada lista individual a la lista principal
        tray_items.Add(tray_items2);
        tray_items.Add(tray_items3);
        tray_items.Add(tray_items4);
        tray_items.Add(tray_items5);


        panel = GameObject.Find("BasicPanel");

        DownBreadPanel = GameObject.Find("DownBreadPanel");
        TopBreadPanel = GameObject.Find("TopBreadPanel");
        MeatPanel = GameObject.Find("MeatPanel");
        CheesePanel = GameObject.Find("CheesePanel");
        KetchupPanel = GameObject.Find("KetchupPanel");

        LettucePanel = GameObject.Find("LettucePanel");
        LettucePanel.SetActive(false);

        Cola = GameObject.Find("Cola");
        Beer = GameObject.Find("Beer");
        Water = GameObject.Find("Water");

        ColaPosition = Cola.transform.position;
        BeerPosition = Beer.transform.position;
        WaterPosition = Water.transform.position;

        Tray = GameObject.Find("Tray");
        TrayPosition = Tray.transform.position;
        Destroy(Tray);
        Tray = Instantiate(Resources.Load("Tray") as GameObject, TrayPosition, Quaternion.identity);
        Tray.name = "Tray";
        // Después de recrear el objeto Tray
        TrayPosition = Tray.transform.position;

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
            // controllerMenus.activeTutorialText(true);
            // controllerMenus.activeButtonText(true);
            tutorialText.SetActive(true);
            buttonText.SetActive(true);
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
        if(chosenIndexes.Count == 5) // Todas las mesas tienen comida
        {   
            dayExp += 40; // Extra experiencia por completar todas las mesas
            this.timePerDay = calculateTime(0); // Finalizamos el día
        }

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

        if(isEndOfTheDay && !onlyOnce && !rasaFinish){ // Activamos RASA solo una vez cada dia y cuando no se ha terminado la encuesta
            onlyOnce = true;
            pauseGame(); // Pausamos el juego
            controllerMenus.showChatbot(); // Mostramos el chatbot
            if (firstTime && !trigger){  //fisrt time bot appears
                // tts2.Play("Hello! Do you want to answer some questions about the game?");
                CallPostRequest(names[0]); //automatically send hi to bot to activate it
                counter += 2;
                trigger = true;
                firstTime = false;
            
            }else if (!firstTime && !trigger){  // trigger is for when stop/take a break
                //send the internal command to wake up the bot
                // tts2.Play("Hello again! Do you want to continue?");
                CallPostRequest(names[1]);
                trigger = true;
            }
            //when we wake up the bot, we remind him the question left behind
            if (restart){  // when else if
                StartCoroutine(coroutineCall(8,tmp));
                //CallPostRequest(tmp);
            }
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
            if (!isComandaActive  && !isNearTable) // Si no tenemos ninguna comanda activa y no estamos cerca de ninguna mesa
            {

                //Este if sirve para que no podamos ir clicando la A hasta que se active la mesa que queramos
                if (!isTableChoosed)
                {
                    //Escogemos una mesa random para así tenerla fijada hasta que no hayamos acabado el pedido
                    //Activamos el collider de la mesa random
                    var random = new System.Random();
                    
                    do { 
                        randomIndex = random.Next(textsClient.Count); // Elegir un nuevo índice hasta que encuentre uno que no haya sido elegido
                    } while (chosenIndexes.Contains(randomIndex));

                    chosenIndexes.Add(randomIndex); // Agregar el nuevo índice a la lista de seleccionados

                    textsClient[randomIndex].SetActive(true);
                    textsClient[randomIndex].GetComponent<TextMeshProUGUI>().SetText("Here!");
                    // tts2.Play("Here!"); // Reproducir audio de "Here!"
                    textsClient[randomIndex].GetComponent<TextMeshProUGUI>().fontSize = 0.15f;
                    tables[randomIndex].GetComponent<BoxCollider>().enabled = true;
                    isTableChoosed = true;
                    // controllerMenus.activeTutorialText(false);
                    tutorialText.SetActive(false);
                }
            }
        }
        if (isNearTable && !isComandaActive) 
        {
            if (!isDayStarted)
            {
                isDayStarted = true;
            }
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Audio/orderSound"), player.transform.position, 1.0f);
            if (userControl.getCurrentDay() == 1)
            {
                arrowPointer.SetActive(true);
            }
            orderGenerator.generateOrder();
            textsClient[randomIndex].GetComponent<TextMeshProUGUI>().fontSize = 0.08f;
            textsClient[randomIndex].GetComponent<TextMeshProUGUI>().SetText(orderGenerator.getActualOrder().sentence.ToString());
            // tts2.Play(orderGenerator.getActualOrder().sentence.ToString()); // Reproducir audio de la comanda
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
            controllerMenus.showImageSmall(true, "Panels/images/Lettuce");
            isEndOfTheDay = false;

        }

        else if (isJustUnlockedLevel3)
        {

            controllerMenus.menuStartOfTheDayUnlockedCondicionalsIf(currentDay);
            //Activamos los paneles condicionales
            unlockConditionalsIf();
            controllerMenus.showImageBig(true, "Panels/images/CheeseHigherOne");
            isJustUnlockedLevel3 = false;
            isEndOfTheDay = false;

        }
        else if (isJustUnlockedLevel4)
        {
            controllerMenus.menuStartOfTheDayUnlockedCondicionalsIfElse(currentDay);
            //Activamos los paneles condicionales
            unlockConditionalsIfElse();
            controllerMenus.showImageBig(true, "Panels/images/BasicIf1");
            isJustUnlockedLevel4 = false;
            isEndOfTheDay = false;


        }
        else if (isJustUnlockedLevel5)
        {
            controllerMenus.menuIniciDeDiaUnlockedBucle(currentDay);

            //Desbloqueamos los paneles iterativos
            unlockIteratives();
            controllerMenus.showImageSmall(true, "Panels/images/ForOrder0");
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
            deleteInstructions(); // Eliminamos las instrucciones del panel de instrucciones

            clearTables(); // Limpiamos las mesas.

            for (int i = 0; i < subtables.Count; i++) { // Activamos los colliders de las submesas.
                subtables[i].GetComponent<BoxCollider>().enabled = true;
            }

            //Aquí deberíamos bloquear el botón hasta que volvamos a clicar A

            controllerMenus.menuEndOfTheDay(dayExp);//Mostrem el menú que indica la finalització del dia. 
            isEndOfTheDay = true; // Activamos la flag que indica el final del día.
            temps = 96f; // Reiniciamos el tiempo para el día siguiente.
            actualTimeDay = temps; // Actualizamos el tiempo actual del día. / jornada
            sun.transform.rotation = sunRotation; // Reiniciamos la rotación del sol.
            lastHour = (int)((actualTimeDay - temps) / 12) + 9; // Actualizamos la última hora del día.
            currentDay += 1; // Incrementamos el día actual para avanzar al siguiente. 
            this.userControl.setCurrentDay(currentDay);
            totalExp += dayExp;
            this.userControl.setTotalExp((int)totalExp);
            this.timeCurrentOrder = 0f; // Reiniciamos el tiempo de la comanda.

            onlyOnce = false; // Reiniciamos la variable para que se pueda volver a activar RASA al siguiente dia
        }
        return temps; // Retornamos el tiempo actualizado. 
    }

    private void clearTables()
    {
        // Destruir objetos dentro de tray_items
        for (int i = 0; i < tray_items.Count; i++) {
            foreach (GameObject item in tray_items[i]) {
                Destroy(item);
            }
            tray_items[randomIndex].Clear();
        }

        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>()
        .Where(obj => obj.name == "Tray")
        .ToArray();
        foreach (GameObject obj in objects) // Destruimos las bandejas
        {
            Destroy(obj);
        }
        chosenIndexes.Clear();

        Tray = Instantiate(Resources.Load("Tray") as GameObject, TrayPosition, Quaternion.identity);
        Tray.name = "Tray";
        // Después de recrear el objeto Tray
        TrayPosition = Tray.transform.position;
    }

    private void updateSunPosition() {
        float xRotation = sun.transform.rotation.eulerAngles.x + 15;
        Quaternion newRotation = Quaternion.Euler(xRotation, 0f, 0f);
            
        // Asignamos la nueva rotación a la luz del sol
        sun.transform.rotation = newRotation;
    }

    public void updateClock(float temps) {   
        this.currentHour = (int)((actualTimeDay - temps) / 12) + 9;
        controllerMenus.updateTemps(this.currentHour + "");

        // Actualizamos la posición del sol
        if(this.currentHour != lastHour){
            updateSunPosition();
            lastHour = this.currentHour;
        }
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

    public void deliverOrder(string table_name, GameObject bandeja)
    {   
        if(delivering == true){ // Solo acabamos si estamos en estado de entrega, sino simplemente se borra
            if(subtables[randomIndex].name.Equals(table_name)){ // si bandeja esta en mesa correcta
                deliverText.SetActive(false);
                if (userControl.getWaiterLevel() == 1){
                    deliverText.SetActive(false);
                }

                Tray = Instantiate(Resources.Load("Tray") as GameObject, TrayPosition, Quaternion.identity);
                Tray.name = "Tray";
                // Después de recrear el objeto Tray
                TrayPosition = Tray.transform.position;

                controllerMenus.mostrarFeedback("Succes! Let's go for another order!");
                AudioSource.PlayClipAtPoint(DeliverSound, player.transform.position, 1.0f); // sonido de acabado la entrega
                isComandaActive = false; // Desactivamos la flag de mostrar la comanda.
                isTableChoosed = false;
                textsClient[randomIndex].SetActive(false);
                this.timeCurrentOrder = 0f; // Reiniciamos el tiempo del pedido. 
                delivering = false; // desactivamos estado de delivery/entrega del pedido
                // Button.GetComponent<MeshRenderer>().material = blueMatButton; // cambiamos color boton a azul
                // ButtonPushClick.isButtonPressed = false; // desactivamos flag de boton pulsado
                // Button.transform.position = ButtonPosition; // reseteamos posicion boton
                // controllerMenus.activeDeliverText(false);

                subtables[randomIndex].GetComponent<BoxCollider>().enabled = false; // desactivamos collider de mesa para bandeja

                int trayExp = 15; // Experiencia de la bandeja. 10xp por bandeja +5 por cada ingrediente extra
                trayExp += tray_items[randomIndex].Count * 5; // Sumamos la experiencia de los ingredientes extra.
                dayExp += trayExp; // Sumamos la experiencia de esta comanda a la experiencia del día.   
            }else{
                if(deliverTry == false){ // si no se ha intentado entregar
                    deliverTry = true;
                    AudioSource.PlayClipAtPoint(ButtonSoundIncorrect, player.transform.position, 1.0f); // error al entregar
                    OVRInput.SetControllerVibration(5, 200, OVRInput.Controller.RTouch);
                    OVRInput.SetControllerVibration(5, 200, OVRInput.Controller.LTouch);
                    controllerMenus.mostrarFeedback("Wrong table! Try again!");
                    DestroyTray(bandeja); // Destruimos la bandeja.
                }
            }
        }
    }

    public void createDelivery(){ //metodo para crear menu en bandeja

        if(recreatingDeliver == true){ // si estamos recreando pedido por mala entrega
            Tray = Instantiate(Resources.Load("Tray") as GameObject, TrayPosition, Quaternion.identity);
            Tray.name = "Tray";
            // Después de recrear el objeto Tray
            TrayPosition = Tray.transform.position;
            recreatingDeliver = false;
        }
        string [] instrucciones = orderGenerator.getActualOrder().instructions;
        string [] drinks = { "Cola", "Beer", "Water" };
        string [] ingredients = { "LettucePanel", "CheesePanel", "MeatPanel", "KetchupPanel" };
        bool hamburger = false;
        float cont_hamburger = TrayPosition.y + 0.02f;

        int i = 0;
        while (i < instrucciones.Length){
            if(Array.IndexOf(drinks, instrucciones[i]) >= 0){ // existe bebida / -1 if not found
                GameObject drink = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(-0.1f, TrayPosition.y, 0.1f), Quaternion.identity);
                 
                tray_items[randomIndex].Add(drink);
                i += 1;
            }
            else if(instrucciones[i] == "DownBreadPanel"){ // empezamos hamburguesa
                hamburger = true;
                //Instanciar y agregar el objeto DownBreadPanel
                GameObject DownBreadPanel = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(0f, cont_hamburger, 0f), Quaternion.identity);
                cont_hamburger += DownBreadPanel.GetComponent<Renderer>().bounds.size.y;
                tray_items[randomIndex].Add(DownBreadPanel);
                i += 1;
                while(hamburger == true){  // mientras no lleguemos al final de la hamburguesa
                    if(instrucciones[i] == "TopBreadPanel"){
                        hamburger = false;
                        //Instanciar y agregar el objeto TopBreadPanel
                        GameObject TopBreadPanel = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(0f, cont_hamburger, 0f), Quaternion.identity);
                        cont_hamburger += TopBreadPanel.GetComponent<Renderer>().bounds.size.y;
                        tray_items[randomIndex].Add(TopBreadPanel);
                        i += 1;
                    }
                    else if(Array.IndexOf(ingredients, instrucciones[i]) >= 0){ //Si es un ingrediente
                        if(instrucciones[i] == "CheesePanel"){
                            GameObject ingredient = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(-0.1f, cont_hamburger + 0.17f, -0.15f), Quaternion.Euler(90f, 0f, 0f));
                            cont_hamburger += ingredient.GetComponent<Renderer>().bounds.size.y;
                            tray_items[randomIndex].Add(ingredient);
                            i += 1;
                        }else{
                            //Instanciar y agregar el objeto ingrediente x (LettucePanel, CheesePanel, MeatPanel, KetchupPanel)
                            GameObject ingredient = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(0f, cont_hamburger, 0f), Quaternion.identity);
                            cont_hamburger += ingredient.GetComponent<Renderer>().bounds.size.y;
                            tray_items[randomIndex].Add(ingredient);
                            i += 1;
                        }
                    }
                    else if(instrucciones[i] == "ForTwoPanel"){ //panel for two
                        i += 1;
                        //Instanciar y agregar el objeto ingrediente del ForTwoPanel
                        GameObject for1 = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(0f, cont_hamburger, 0f), Quaternion.identity);
                        cont_hamburger += for1.GetComponent<Renderer>().bounds.size.y;
                        tray_items[randomIndex].Add(for1);
                        GameObject for2 = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(0f, cont_hamburger, 0f), Quaternion.identity);
                        cont_hamburger += for2.GetComponent<Renderer>().bounds.size.y;
                        tray_items[randomIndex].Add(for2);
                        i += 1;
                        while(instrucciones[i] != "ClosingPanel"){
                            //Instanciar y agregar el objeto hasta ClosingPanel, puede haber varios ingredientes dentro del FOR
                            GameObject for3 = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(0f, cont_hamburger, 0f), Quaternion.identity);
                            cont_hamburger += for3.GetComponent<Renderer>().bounds.size.y;
                            tray_items[randomIndex].Add(for3);
                            GameObject for4 = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(0f, cont_hamburger, 0f), Quaternion.identity);
                            cont_hamburger += for4.GetComponent<Renderer>().bounds.size.y;
                            tray_items[randomIndex].Add(for4);
                            i += 1;
                        }
                    }
                    else{
                        i += 1;
                    }
                }
            }
            else{ // futura implementacion de otros elementos
                GameObject extra = Instantiate(Resources.Load("Menu/"+instrucciones[i]) as GameObject, TrayPosition + new Vector3(0.1f, TrayPosition.y + 0.02f, -0.1f), Quaternion.identity);
                tray_items[randomIndex].Add(extra);
                i += 1;
            }
        }
        // foreach (GameObject item in tray_items[randomIndex]){ // asignamos padre a todos los elementos de la bandeja
        //     item.transform.parent = Tray.transform;
        // }
        // tray_items[randomIndex].Add(Tray); // añadimos la bandeja a la lista de elementos de la bandeja
    }

    public void arriveOrder(OrderWaiter order)
    {
        //Este if lo utilizamos por si el día acaba a mitad del pedido ("explicado" en el tutorial)     
        if (isComandaActive)
        {
            feedbackGenerator.setComandaActual(orderGenerator.getActualOrder()); //Definimos la comanda generada en la clase que generará el feedback. 
            (string, float, bool) feedback = feedbackGenerator.getFeedback(order, this.timeCurrentOrder); // Obtenemos una string con el feedback, un entero correspondiente a la puntuación recibida y un booleano para saber si la entrega ha sido correcta
            bool logro = false;

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
                    logro = true;
                    this.userControl.setFirstOrderWaiterAchievement(true);
                    isJustUnlockedLevel2 = true;
                    AchievementUnlockedClip = Resources.Load<AudioClip>("Audio/FirstBasicOrderAudio");
                    AudioSource.PlayClipAtPoint(AchievementUnlockedClip, Button.transform.position, 1.0f);
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: First Basic Order Done! Congratulations, keep going on");
                    controllerMenus.showImageBig(true, "Sprites/images/LogroNormal2");
                    // controllerMenus.activeButtonText(false);
                    buttonText.SetActive(false);
                    deliverText.SetActive(true);
                    arrowPointer.SetActive(false);
                    hatTv.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/green");
                }
                else if (this.userControl.getNumBasicOrdersWaiter() == 10 && !this.userControl.getTwentyfiveBasicOrdersWaiterAchievement())
                {
                    logro = true;
                    isJustUnlockedLevel3 = true;
                    this.userControl.setTwentyfiveBasicOrdersWaiterAchievement(true);
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Ten Basic Orders Done! Congratulations, level up!");
                    controllerMenus.showImageBig(true, "Sprites/images/LogroNormal3");
                    hatTv.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/blue");
                    //Activar logro 20 comandas normales
                }

                else if (this.userControl.getNumConditionalIfOrdersWaiter() == 1 && !this.userControl.getFirstConditionalIfOrderWaiterAchievement())
                {
                    logro = true;
                    this.userControl.setFirstConditionalIfOrderWaiterAchievement(true);
                    isJustUnlockedLevel4 = true;
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: First Basic Conditional Order Done! Congratulations, leveled up!");
                    controllerMenus.showImageBig(true, "Sprites/images/LogroCondicional1");
                    hatTv.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/red");
                    //Activar logro 1 comandas condicional
                }

                else if (this.userControl.getNumConditionalIfOrdersWaiter() == 10 && !this.userControl.getTenConditionalIfOrdersWaiterAchievement())
                {
                    logro = true;
                    this.userControl.setTenConditionalIfOrdersWaiterAchievement(true);
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Ten Basic Conditional Orders Done! Congratulations, keep it up!");
                    controllerMenus.showImageBig(true, "Sprites/images/LogroCondicional2");
                    //Activar logro 10 comandas condicional
                }

                else if (this.userControl.getNumConditionalIfOrdersWaiter() == 30 && !this.userControl.getThirtyConditionalIfOrdersWaiterAchievement())
                {
                    logro = true;
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Thirty Basic Conditional Orders Done! Congratulations, keep it up!");
                    this.userControl.setThirtyConditionalIfOrdersWaiterAchievement(true);
                    controllerMenus.showImageBig(true, "Sprites/images/LogroCondicional3");
                    //Activar logro 30 comandas condicionals
                }

                else if (this.userControl.getNumConditionalIfElseOrdersWaiter() == 1 && !this.userControl.getFirstConditionalIfElseOrderWaiterAchievement())
                {
                    logro = true;
                    this.userControl.setFirstConditionalIfElseOrderWaiterAchievement(true);
                    isJustUnlockedLevel5 = true;
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: First Double Conditional Order Done! Congratulations, leveled up!");
                    controllerMenus.showImageBig(true, "Sprites/images/LogroCondicional1");
                    hatTv.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/gold");
                    //Activar logro 1 comandas condicional
                }

                else if (this.userControl.getNumConditionalIfElseOrdersWaiter() == 10 && !this.userControl.getTenConditionalIfElseOrdersWaiterAchievement())
                {
                    logro = true;
                    this.userControl.setTenConditionalIfElseOrdersWaiterAchievement(true);
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Ten Double Conditional Orders Done! Congratulations, keep it up!");
                    controllerMenus.showImageBig(true, "Sprites/images/LogroCondicional2");
                    //Activar logro 10 comandas condicional
                }

                else if (this.userControl.getNumConditionalIfElseOrdersWaiter() == 30 && !this.userControl.getThirtyConditionalIfElseOrdersWaiterAchievement())
                {
                    logro = true;
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Thirty Double Conditional Order Done! Congratulations, keep it up!");
                    this.userControl.setThirtyConditionalIfElseOrdersWaiterAchievement(true);
                    controllerMenus.showImageBig(true, "Sprites/images/LogroCondicional3");
                    //Activar logro 30 comandas condicionals
                }

                else if (this.userControl.getNumIterativeOrdersWaiter() == 1 && !this.userControl.getFirstConditionalIfElseOrderWaiterAchievement())
                {
                    logro = true;
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: First Iterative Order Done! Congratulations, keep it up!");
                    controllerMenus.showImageBig(true, "Sprites/images/LogroFOR1");
                    //Activar logro 1 comandas iterativas
                    this.userControl.setFirstConditionalIfElseOrderWaiterAchievement(true);
                }

                else if (this.userControl.getNumIterativeOrdersWaiter() == 10 && !this.userControl.getTenConditionalIfElseOrdersWaiterAchievement())
                {
                    logro = true;
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Ten Iterative Orders Done! Congratulations, keep it up!");
                    controllerMenus.showImageBig(true, "Sprites/images/LogroFOR2");
                    //Activar logro 10 comandas iterativas
                    this.userControl.setTenIterativeOrdersWaiterAchievement(true);
                }

                else if (this.userControl.getNumIterativeOrdersWaiter() == 30 && !this.userControl.getThirtyConditionalIfElseOrdersWaiterAchievement())
                {
                    logro = true;
                    controllerMenus.orderMenuWaiter("Success! Achievement Unlocked: Thirty Iterative Orders Done! Congratulations, keep it up!");
                    this.userControl.setThirtyConditionalIfElseOrdersWaiterAchievement(true);
                    controllerMenus.showImageBig(true, "Sprites/images/LogroFOR3");
                    //Activar logro 30 comandas condicionals
                }
                dayExp += feedback.Item2; // Sumamos la experiencia de esta comanda a la experiencia del día.   
            }
            
            if (!feedback.Item3)
            {
                //El pedido ha sido incorrecto, ponemos el mensaje por pantalla y hacemos que vuelva a salir el pedido que hay que realizar
                controllerMenus.mostrarFeedback(feedback.Item1);
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
                if(logro == true){ // si desbloqueamos logro esperamos a enseñar feedback
                    StartCoroutine("EjecutarDespuesDeEsperar", feedback.Item1); //Mostramos en pantalla el mensaje de texto del feedback. tras un tiempo
                }else{
                    controllerMenus.mostrarFeedback(feedback.Item1);
                }
                delivering = true; // Indicamos que se está realizando la entrega
                createDelivery(); // creamos la entrega
                Button.GetComponent<MeshRenderer>().material = greenMatButton;
                AudioSource.PlayClipAtPoint(ButtonSoundCorrect, Button.transform.position, 1.0f);
                deleteInstructions();  // Borramos las instrucciones que se han realizado
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
            CheesePanel.transform.localScale = localScaleOriginal;

        }
        else if (name == "MeatPanel")
        {
            MeatPanel = Instantiate(Resources.Load("Panels/MeatPanel") as GameObject, MeatPanelPosition, Quaternion.identity);
            MeatPanel.name = "MeatPanel";
            MeatPanel.transform.localScale = localScaleOriginal;
        }
        else if (name == "LettucePanel")

        {
            LettucePanel = Instantiate(Resources.Load("Panels/LettucePanel") as GameObject, LettucePanelPosition, Quaternion.identity);
            LettucePanel.name = "LettucePanel";
            LettucePanel.transform.localScale = localScaleOriginal;

        }
        else if (name == "TopBreadPanel")
        {
            TopBreadPanel = Instantiate(Resources.Load("Panels/TopBreadPanel") as GameObject, TopBreadPanelPosition, Quaternion.identity);
            TopBreadPanel.name = "TopBreadPanel";
            TopBreadPanel.transform.localScale = localScaleOriginal;
        }
        else if (name == "DownBreadPanel")
        {
            DownBreadPanel = Instantiate(Resources.Load("Panels/DownBreadPanel") as GameObject, DownBreadPanelPosition, Quaternion.identity);
            DownBreadPanel.name = "DownBreadPanel";
            DownBreadPanel.transform.localScale = localScaleOriginal;
        }

        else if (name == "KetchupPanel")
        {
            KetchupPanel = Instantiate(Resources.Load("Panels/KetchupPanel") as GameObject, KetchupPanelPosition, Quaternion.identity);
            KetchupPanel.name = "KetchupPanel";
            KetchupPanel.transform.localScale = localScaleOriginal;
        }
        else if (name == "IfLettucePlusThreePanel")
        {
            IfLettucePlusThreePanel = Instantiate(Resources.Load("Panels/IfLettucePlusThreePanel") as GameObject, IfLettucePlusThreePanelPosition, Quaternion.identity);
            IfLettucePlusThreePanel.name = "IfElseLettucePlusThreePanel";
            IfLettucePlusThreePanel.transform.localScale = localScaleOriginal;
        }


        else if (name == "IfCheesePlusOnePanel")
        {
            IfCheesePlusOnePanel = Instantiate(Resources.Load("Panels/IfCheesePlusOnePanel") as GameObject, IfCheesePlusOnePanelPosition, Quaternion.identity);
            IfCheesePlusOnePanel.name = "IfElseCheesePlusOnePanel";
            IfCheesePlusOnePanel.transform.localScale = localScaleOriginal;
        }


        else if (name == "IfMeatPlusThreePanel")
        {
            IfMeatPlusThreePanel = Instantiate(Resources.Load("Panels/IfMeatPlusThreePanel") as GameObject, IfMeatPlusThreePanelPosition, Quaternion.identity);
            IfMeatPlusThreePanel.name = "IfMeatPlusThreePanel";
            IfMeatPlusThreePanel.transform.localScale = localScaleOriginal;
        }

        else if (name == "IfElseLettucePlusThreePanel")
        {
            IfElseLettucePlusThreePanel = Instantiate(Resources.Load("Panels/IfElseLettucePlusThreePanel") as GameObject, IfElseLettucePlusThreePanelPosition, Quaternion.identity);
            IfElseLettucePlusThreePanel.name = "IfElseLettucePlusThreePanel";
            IfElseLettucePlusThreePanel.transform.localScale = localScaleOriginal;
        }


        else if (name == "IfElseCheesePlusOnePanel")
        {
            IfElseCheesePlusOnePanel = Instantiate(Resources.Load("Panels/IfElseCheesePlusOnePanel") as GameObject, IfElseCheesePlusOnePanelPosition, Quaternion.identity);
            IfElseCheesePlusOnePanel.name = "IfElseCheesePlusOnePanel";
            IfElseCheesePlusOnePanel.transform.localScale = localScaleOriginal;

        }


        else if (name == "IfElseMeatPlusThreePanel")
        {
            IfElseMeatPlusThreePanel = Instantiate(Resources.Load("Panels/IfElseMeatPlusThreePanel") as GameObject, IfElseMeatPlusThreePanelPosition, Quaternion.identity);
            IfElseMeatPlusThreePanel.name = "IfElseMeatPlusThreePanel";
            IfElseMeatPlusThreePanel.transform.localScale = localScaleOriginal;
        }

        else if (name == "ForTwoPanel")
        {
            ForTwoPanel = Instantiate(Resources.Load("Panels/ForTwoPanel") as GameObject, ForTwoPanelPosition, Quaternion.identity);
            ForTwoPanel.name = "ForTwoPanel";
            ForTwoPanel.transform.localScale = localScaleOriginal;
        }

        else if (name == "Cola")
        {
            Cola = Instantiate(Resources.Load("Panels/Cola") as GameObject, ColaPosition, Quaternion.identity);
            Cola.name = "Cola";
        }
        else if (name == "Beer")
        {
            Beer = Instantiate(Resources.Load("Panels/Beer") as GameObject, BeerPosition, Quaternion.identity);
            Beer.name = "Beer";
        }
        else if (name == "Water")
        {
            Water = Instantiate(Resources.Load("Panels/Water") as GameObject, WaterPosition, Quaternion.identity);
            Water.name = "Water";
        }
    }

    public void setIsNearTable(bool isNearTable)
    {
        this.isNearTable = isNearTable;
    }

    public void DestroyTray(GameObject bandeja)
    {
        Destroy(bandeja);

        // Destruir objetos dentro de tray_items
        foreach (GameObject item in tray_items[randomIndex]) {
            Destroy(item);
        }
        tray_items[randomIndex].Clear();

        recreatingDeliver = true;
        createDelivery();
        deliverTry = false;
    }


    IEnumerator EjecutarDespuesDeEsperar(string feedback)
    {
        yield return new WaitForSeconds(8);

        controllerMenus.mostrarFeedback(feedback);
    }

    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(4);
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
        //StartCoroutine(putRequest());
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


    //RASA PART
     //In fixedUpdate we check the timeout condition
    void FixedUpdate(){
        if (trigger & time == 4500){ // 4500 = 90sec timeout
            // do something if timeout
            CallPostRequest("timeout timeout");
            time = 0;
            
        }else {
            time = time + 1;
        }
    }
     //CallPostRequest is responsible for helping the logic of the game and packaging the requests.
    public void CallPostRequest(string txt)
    {
        time = 0;
        MessageSenderRasa thisMSG = new MessageSenderRasa();
        if (thisMSG!=null){
            thisMSG.sender = "Rasaa";
        }
        if (txt == ""){
            //thisMSG.message = "fairly"; //EJEMPLO CAMBIAR TEST
            thisMSG.message = inputField.text;
            //displayOutgoingText.text = inputField.text;
            //saved = thisMSG.message.ToLower().Contains("stop");
            if (thisMSG.message.ToLower().Contains("stop") || thisMSG.message.ToLower().Contains("take a break") || thisMSG.message.ToLower().Contains("pause")){
                saved = true;
            }else{
                saved = false;
            }
            if (counter>=2 & saved==false){
                tmp = tmp.Replace(tmp, names[counter]+thisMSG.message);
                thisMSG.message = tmp;
                counter+=1;
            }
            if (saved){
                trigger = false;
                restartGame();
                controllerMenus.deactivateChatbot();
                //timeoutTrigger = true;
            }
        }else{
            thisMSG.message = txt;
            //displayOutgoingText.text = txt;
            paused = thisMSG.message.ToLower().Contains("timeout timeout");
            if (paused){
                trigger = false;
                restartGame();
                controllerMenus.deactivateChatbot();
            }
            if (thisMSG.message.ToLower().Contains("restore")){
                restart = true;
            }
            
        }
        string json = JsonUtility.ToJson(thisMSG);
        string address = "http://localhost:5005/webhooks/rest/webhook";
        //string address = "https://rasa-server-parasolw.cloud.okteto.net/webhooks/rest/webhook";
        //string address = "https://rasa-actions-server-sirkaza.cloud.okteto.net/webhook";

        StartCoroutine(PostRequest(address, json));

    }
    //PostRequest handles REST calls
    IEnumerator PostRequest(string uri, string json)
    {
        //var uwr = new UnityWebRequest(uri, "POST");
	using (var uwr = new UnityWebRequest(uri, "POST")){
        byte[] jsonTOSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonTOSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            text = "Network error! Please check your internet connection.";
        }
        else
        {
            text = uwr.downloadHandler.text;
            string ntext = "";
            string prev = "}";
            string now = ",";
            int prevInd = 1;
            int ind = 1;
            string empty = "" +
                "";
            foreach (char s in text)
            {
                if (prev == s.ToString())
                {
                    string m = text.Substring(prevInd, ind - prevInd);
                    //ntext = ntext + JsonConvert.DeserializeObject<Message>(m).text;
                    ntext = ntext + JsonConvert.DeserializeObject<MessageRasa>(m).text+"\n";

                    prevInd = ind + 1;
                    
                    ntext = ntext + empty;
                    
                }
                ind = ind + 1;
            }
            int startInd = text.IndexOf("text") + 6;
            int length = text.Length - 3 - startInd;
            text = ntext;
        }
        displayIncomingText.text = text;
        tts2.Play(displayIncomingText.text);
        yield return new WaitForSeconds(5);
        }
    }
    //this function is used if the first question was not answered
    IEnumerator coroutineCall(int secs, string texts){
        
        restart = false;
        yield return new WaitForSeconds(secs);
        Debug.Log(texts.Length);
        if(texts.Length < 3){
            CallPostRequest("my day was horrible");
        }else{
            CallPostRequest(texts);
        }
        
    }
}
