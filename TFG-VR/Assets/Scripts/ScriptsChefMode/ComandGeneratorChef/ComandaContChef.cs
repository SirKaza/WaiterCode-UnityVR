using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComandaContChef : MonoBehaviour
{
    private ComandaJSONChef comanda_actual;
    private JSONReaderChef comandesJSON;

    public ComandaContChef()
    {
        comandesJSON = GameObject.Find("ControllerPartida").GetComponent<JSONReaderChef>();
    }

    public void generarComanda()
    {
        comanda_actual = comandesJSON.randomComanda();
    }

    public void unlockNivells(int nivell)
    {
        comandesJSON.unlockLevel(nivell);
    }

    public ComandaJSONChef getComandaActual()
    {
        return this.comanda_actual;
    }

    public void setComanda(ComandaJSONChef comanda)
    {
        comanda_actual = comanda;
    }


}
