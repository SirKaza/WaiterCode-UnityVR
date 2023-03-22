using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendedScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<AttIngredients>())
        {
            ComandaChef send_comanda = collision.gameObject.GetComponent<AttIngredients>().GetHamburguer();
            controladorPartidaChef.current.arriveComanda(send_comanda);
            Destroy(collision.gameObject,0.5f);
            this.gameObject.SetActive(false);
            //Efectos de explosión
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
