using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class panelScript : MonoBehaviour

{
    private bool isInPanel = false;
    private bool itWasInPanel = false;
    private bool isInIf = false;
    private bool isInElse = false;
    private bool isFor = false;
    private bool isIf = false;

    private bool isInFor = false;
    private int reescaleSize;
    private int panelsInside;
    private GameObject isInside = null;

    private bool isIfElse;
    private bool isInIfElse;
    private int panelsInsideIf;
    private int panelsInsideElse;
    private int reescaleSizeIf;
    private int reescaleSizeElse;

    private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {

        panel = GameObject.Find("BasicPanel");
        if (this.name == "ForTwoPanel")
        {
            isFor = true;
            panelsInside = 0;
            reescaleSize = 1;
        }

        if (this.name == "IfElseMeatPlusThreePanel" || this.name == "IfElseLettucePlusThreePanel" || this.name == "IfElseCheesePlusOnePanel")
        {
            isIfElse = true;
            panelsInsideIf = 0;
            panelsInsideElse = 0;
            reescaleSizeIf = 1;
            reescaleSizeElse = 1;
        }

        if (this.name == "IfMeatPlusThreePanel" || this.name == "IfLettucePlusThreePanel" || this.name == "IfCheesePlusOnePanel")
        {
            isIf = true;
            panelsInside = 0;
            reescaleSize = 1;
        }

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

            if (this.isInIf && this.isInIfElse)
            {
                ////Avisamos al If de que le sacamos un panel. Desde el panel deberíamos hacer esto, ya que estamos cambiando un modelo desde otro modelo en vez que desde el controlador
                isInside.GetComponent<panelScript>().setPanelsInsideIf(isInside.GetComponent<panelScript>().getPanelsInsideIf() - 1);

                isInIf = false;
                isInIfElse = false;
                isInside = null;
            }

            else if (this.isInElse)
            {
                //Avisamos al If de que le sacamos un panel. Desde el panel deberíamos hacer esto, ya que estamos cambiando un modelo desde otro modelo en vez que desde el controlador
                isInside.GetComponent<panelScript>().setPanelsInsideElse(isInside.GetComponent<panelScript>().getPanelsInsideElse() - 1);
                isInElse = false;
                isInIfElse = false;
                isInside = null;
            }
            else if (this.isInIf)
            {
                //Avisamos al If de que le sacamos un panel. Desde el panel deberíamos hacer esto, ya que estamos cambiando un modelo desde otro modelo en vez que desde el controlador
                isInside.GetComponent<panelScript>().setPanelsInside(isInside.GetComponent<panelScript>().getPanelsInside() - 1);
                isInIf = false;
                isInside = null;
            }

            else if (this.isInFor)
            {
                isInside.GetComponent<panelScript>().setPanelsInside(isInside.GetComponent<panelScript>().getPanelsInside() - 1);
                isInFor = false;
                isInside = null;
            }
            isInPanel = false;
            itWasInPanel = true;

            //Esto habría que cambiarlo y hacer que lo detecte el AttInstructions. Lo dejaremos para cuando funcione el reescalado


            panel.GetComponent<AttInstructions>().GetHamburguer().removeInstruction(this.gameObject);

            if (this.name == "ForTwoPanel" || this.name == "IfMeatPlusThreePanel" || this.name == "IfLettucePlusThreePanel" || this.name == "IfCheesePlusOnePanel")
            {
                this.transform.SetParent(null);
                panel.GetComponent<AttInstructions>().GetHamburguer().removeInstruction(this.gameObject.transform.GetChild(2).gameObject);
            }

            if (this.name == "IfElseMeatPlusThreePanel" || this.name == "IfElseLettucePlusThreePanel" || this.name == "IfElseCheesePlusOnePanel")
            {
                this.transform.SetParent(null);
                panel.GetComponent<AttInstructions>().GetHamburguer().removeInstruction(this.gameObject.transform.GetChild(2).gameObject);
                panel.GetComponent<AttInstructions>().GetHamburguer().removeInstruction(this.gameObject.transform.GetChild(4).gameObject);
            }

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

    public void setIsInIf(bool inIf)
    {
        isInIf = inIf;
    }

    public bool getIsInIf()
    {
        return isInIf;
    }

    public void setIsInElse(bool inElse)
    {
        isInElse = inElse;
    }

    public bool getIsInElse()
    {
        return isInElse;
    }

    //New
    public void setIsInFor(bool inFor)
    {
        isInFor = inFor;
    }

    public bool getIsInFor()
    {
        return isInFor;
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


    public void setReescaleSize(int reescaleSize)
    {
        this.reescaleSize = reescaleSize;
    }

    public int getReescaleSize()
    {
        return reescaleSize;
    }

    public void setIsFor(bool isFor)
    {
        this.isFor = isFor;
    }

    public bool getIsFor()
    {
        return isFor;
    }

    public void setIsIf(bool isIf)
    {
        this.isIf = isIf;
    }

    public bool getIsIf()
    {
        return isIf;
    }

    public void setIsIfElse(bool isIfElse)
    {
        this.isIfElse = isIfElse;
    }

    public bool getIsIfElse()
    {
        return isIfElse;
    }

    public void setIsInIfElse(bool isInIfElse)
    {
        this.isInIfElse = isInIfElse;
    }

    public bool getIsInIfElse()
    {
        return isInIfElse;
    }

    public void setPanelsInsideIf(int panelsInside)
    {
        this.panelsInsideIf = panelsInside;

    }

    public int getPanelsInsideIf()
    {
        return panelsInsideIf;
    }


    public void setReescaleSizeIf(int reescaleSize)
    {
        this.reescaleSizeIf = reescaleSize;
    }

    public int getReescaleSizeIf()
    {
        return reescaleSizeIf;
    }

    public void setPanelsInsideElse(int panelsInside)
    {
        this.panelsInsideElse = panelsInside;

    }

    public int getPanelsInsideElse()
    {
        return panelsInsideElse;
    }


    public void setReescaleSizeElse(int reescaleSize)
    {
        this.reescaleSizeElse = reescaleSize;
    }

    public int getReescaleSizeElse()
    {
        return reescaleSizeElse;
    }
}
