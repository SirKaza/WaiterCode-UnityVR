using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class panelScriptTutorial : MonoBehaviour

{
    private bool isInPanel = false;
    private bool itWasInPanel = false;
    private int panelsInside;
    private GameObject isInside = null;


    private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {

        panel = GameObject.Find("BasicPanel");

    }

    // Update is called once per frame
    void Update()
    {

        if (!this.GetComponent<OVRGrabbable>().isGrabbed && itWasInPanel && !isInPanel)
        {
            Destroy(gameObject, 3);
        }
        //I want to do this only one time, when I have all the panel freeze
        if (this.GetComponent<OVRGrabbable>().isGrabbed && this.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            isInPanel = false;
            itWasInPanel = true;

            //Esto habría que cambiarlo y hacer que lo detecte el AttInstructions.
            panel.GetComponent<AttInstructionsTutorial>().GetHamburguer().removeInstruction(this.gameObject);

        }

    }

    public void setIsInPanel(bool inPanel)
    {
        isInPanel = inPanel;
    }

    public bool getIsInPanel()
    {
        return isInPanel;

    }
    public void setItWasInPanel(bool inPanel)
    {
        this.itWasInPanel = inPanel;
    }

    public bool getItWasInPanel()
    {
        return itWasInPanel;
    }



    public void setIsInside(GameObject panel)
    {
        this.isInside = panel;
    }

    public GameObject getIsInside()
    {
        return isInside;
    }

    public void setPanelsInside(int panelsInside)
    {
        this.panelsInside = panelsInside;

    }

    public int getPanelsInside()
    {
        return panelsInside;
    }

}
