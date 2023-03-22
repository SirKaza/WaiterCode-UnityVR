using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class returnScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "GeneratorIng")
        {
            if (collision.gameObject.name.Contains(this.gameObject.name))
            {
                if (SceneManager.GetActiveScene().name == "Tutorial"){
                    ControllerTutorial.current.getCardinalityController().AddCardinalitat(this.gameObject.name);
                }else{
                    controladorPartidaChef.current.getCardinalityController().AddCardinalitat(this.gameObject.name);

                }
                Destroy(this.gameObject);

            }
            else
            {
                Destroy(this.gameObject);

            }
        }
    }
}
