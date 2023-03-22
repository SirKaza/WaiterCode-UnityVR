using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    public static UndoManager current;

    private ArrayList currentObject = new ArrayList();
    private GameObject highlightedObject;

    private void Awake()
    {
        current = this;
    }

    public ArrayList getCurrentObject()
    {
        return this.currentObject;
    }

    public GameObject getHighlightedObject()
    {
        return this.highlightedObject;
    }

    public void setHighlightedObject(GameObject go)
    {
        this.highlightedObject = go;
    }

    public void addCurrentObject(GameObject current_game)
    {
        this.currentObject.Add(current_game);
    }

    public void destroyCurrentGameObject()
    {
        int count = currentObject.Count;
        controladorPartidaChef.current.getCardinalityController().AddCardinalitat((currentObject[count - 1] as GameObject).name);
        (currentObject[count - 1] as GameObject).transform.parent.gameObject.GetComponent<AttIngredients>().notifyUndo((currentObject[count - 1] as GameObject));
        Destroy(currentObject[count-1] as GameObject);
        currentObject.RemoveAt(count - 1);

    }

    private void Update()
    {
        //Buscar cuando se pulsa el botoón b
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            destroyCurrentGameObject();
            //Notificar a los contadores correspondientes.
        }
       
    }
}
