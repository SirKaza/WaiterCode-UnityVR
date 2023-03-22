using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttIngredients : MonoBehaviour
{
    private ComandaChef current_hamburguer;
    private Collision current_collision;
    private bool haveCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        current_hamburguer = new ComandaChef();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "ingredient" && collision.name != "ketchup")
        {
            if (collision.gameObject.GetComponent<Rigidbody>())
            {
                Destroy(collision.gameObject.GetComponent<Rigidbody>());
                Destroy(collision.gameObject.GetComponent<OVRGrabbable>());             
                                
                var pos_y = collision.gameObject.transform.position.y;           

                collision.gameObject.transform.position = new Vector3(this.transform.position.x, pos_y, this.transform.position.z);
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                collision.gameObject.transform.SetParent(this.gameObject.transform);


                if (current_hamburguer.getIngredients().Count == 0)
                {

                    this.transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    current_hamburguer.getLastIngredient().transform.GetChild(0).gameObject.SetActive(false);
                }
                if (collision.gameObject.transform.childCount > 0)
                {
                    collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                }
                current_hamburguer.addIngredients(collision.gameObject);
                UndoManager.current.addCurrentObject(collision.gameObject);


                //collision.gameObject.transform.localPosition = new Vector3(0, collision.gameObject.transform.localPosition.y, 0);
                //this.gameObject.GetComponent<BoxCollider>().size = new Vector3(this.gameObject.GetComponent<BoxCollider>().size.x, this.gameObject.GetComponent<BoxCollider>().size.y + collision.gameObject.GetComponent<BoxCollider>().size.y, this.gameObject.GetComponent<BoxCollider>().size.z);
                //this.gameObject.GetComponent<BoxCollider>().center = new Vector3(this.gameObject.GetComponent<BoxCollider>().center.x, this.gameObject.GetComponent<BoxCollider>().center.y + collision.gameObject.GetComponent<BoxCollider>().size.y / 2, this.gameObject.GetComponent<BoxCollider>().center.z);
            }
        }
    

    }

    public void addKetchup(GameObject ketchupGen){
        Destroy(ketchupGen.GetComponent<Rigidbody>());
        Destroy(ketchupGen.GetComponent<OVRGrabbable>()); 
        ketchupGen.transform.SetParent(this.gameObject.transform);
        current_hamburguer.getLastIngredient().transform.GetChild(0).gameObject.SetActive(false);
        current_hamburguer.addIngredients(ketchupGen);
        UndoManager.current.addCurrentObject(ketchupGen);
        current_hamburguer.getLastIngredient().transform.GetChild(0).gameObject.SetActive(true);

    }

    public void notifyUndo(GameObject removedObject)
    {
        current_hamburguer.removeIngredients(removedObject);

        if (current_hamburguer.getIngredients().Count == 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);

        }
        else
        {
            current_hamburguer.getLastIngredient().transform.GetChild(0).gameObject.SetActive(true);

        }

    }

    private void Update()
    {
        if (current_hamburguer.getLastIngredient().name == "ketchup" && !current_hamburguer.getLastIngredient().transform.GetChild(0).gameObject.active){
            current_hamburguer.getLastIngredient().transform.GetChild(0).gameObject.SetActive(true);
        }
    }




    public ComandaChef GetHamburguer()
    {
        return this.current_hamburguer;
    }

   


    

    
}
