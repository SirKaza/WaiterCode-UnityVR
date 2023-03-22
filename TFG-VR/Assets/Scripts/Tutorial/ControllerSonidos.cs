using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSonidos : MonoBehaviour
{
    private int step = 0;
    private ControllerMenuTutorial controladorMenu;
    AudioSource audio;
    public AudioClip[] audios;


    void Start(){
        this.audio = this.GetComponentInParent<AudioSource>();
    }

    public void nextStep(){
        
        this.step += 1;
        this.audio.clip = audios[step];
        this.audio.Play();

    }

    public int getStep(){
        return this.step;
    }

    public bool isEnded(){
        if (step > audios.Length){
            return true;
        }
        return false;
    }
}
