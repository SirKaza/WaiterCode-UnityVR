using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCardinalitat : MonoBehaviour
{
    [SerializeField] string current;
    private ControlCardinalitats current_contador;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (controladorPartidaChef.current.getCardinalityController().getNewGenerated())
        {
            this.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = controladorPartidaChef.current.getCardinalityController().getCardinality(current) + "";

            current_contador.setNewGenerator(false);
        }
    }
}
