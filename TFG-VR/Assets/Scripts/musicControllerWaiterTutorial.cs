using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class musicControllerWaiterTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setVolume(float vol){
        this.GetComponentInParent<AudioSource>().volume = vol;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged(){
        var sliderActual = GameObject.Find("Slider").GetComponent<Slider>();
        var newValue = sliderActual.value;
        controladorTutorialWaiter.current.current_user().setVolume(newValue+"");
        this.GetComponentInParent<AudioSource>().volume = newValue;
    }
}
