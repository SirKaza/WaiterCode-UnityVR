using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorMenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameObject teclat;
    private static GameObject LogInMenu;
    private static GameObject OpcioMenu;
    private static GameObject IniciarMenu;
    private static GameObject RegisterMenu;
    private static GameObject MainMenu;
    private GameObject GameModeMenu;
    void Start()
    {
        teclat = GameObject.Find("Teclado").gameObject;
        teclat.SetActive(false);
        LogInMenu = GameObject.Find("LogInMenu").gameObject;
        LogInMenu.SetActive(false);
        OpcioMenu = GameObject.Find("OpcionsMenu").gameObject;
        OpcioMenu.SetActive(false);
        IniciarMenu = GameObject.Find("IniciarMenu").gameObject;
        IniciarMenu.SetActive(false);
        RegisterMenu = GameObject.Find("RegisterMenu").gameObject;
        RegisterMenu.SetActive(false);
        MainMenu = GameObject.Find("Menu").gameObject;
        this.GameModeMenu = GameObject.Find("MenuGameMode");
        GameModeMenu.SetActive(false);
    }

    public void btn_jugar()
    {
        MainMenu.SetActive(false);
        LogInMenu.SetActive(true);
    }
    public void btn_opciones()
    {
        MainMenu.SetActive(false);
        OpcioMenu.SetActive(true);

    }
    public void btn_atras_opciones()
    {
        OpcioMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void btn_iniciarSesion()
    {
        LogInMenu.SetActive(false);
        IniciarMenu.SetActive(true);


    }

    public void btn_registrar()
    {
        LogInMenu.SetActive(false);
        RegisterMenu.SetActive(true);

    }

    public void btn_atras_logIn()
    {
        LogInMenu.SetActive(false);
        MainMenu.SetActive(true);


    }

    public void text_field()
    {
        teclat.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
