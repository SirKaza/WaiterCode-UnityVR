using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JSONReaderChef : MonoBehaviour
{

    public TextAsset jsonFileCocinero;
    private ConjuntComandesChef comandaInJsonCocinero;
    private ArrayList comandesJSONCocinero;

    void Start()
    {
        this.comandaInJsonCocinero = JsonUtility.FromJson<ConjuntComandesChef>(jsonFileCocinero.text);
        var comandesMostrar = comandaInJsonCocinero.comandes.Where((value) => value.level == "1").ToList();
        this.comandesJSONCocinero = new ArrayList(comandesMostrar);

    }


    public ArrayList getComandaInJSON()
    {

        return this.comandesJSONCocinero;     

    }

    public void unlockLevel(int level)
    {

        var arrayListNova = new ArrayList(this.comandaInJsonCocinero.comandes.Where((value) => value.level == "" + level).ToList());
        this.comandesJSONCocinero.AddRange(arrayListNova);
        
    }


    public ComandaJSONChef randomComanda()
    {

        var random = new System.Random();
        var index = random.Next(this.comandesJSONCocinero.Count);
        return this.comandesJSONCocinero[index] as ComandaJSONChef;
        
    }









}
