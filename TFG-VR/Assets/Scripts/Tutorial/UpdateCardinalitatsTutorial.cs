using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCardinalitatsTutorial : MonoBehaviour
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

        if (ControllerTutorial.current.getCardinalityController().getNewGenerated())
        {
            this.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ControllerTutorial.current.getCardinalityController().getCardinality(current) + "";

            current_contador.setNewGenerator(false);
        }
    }
}
