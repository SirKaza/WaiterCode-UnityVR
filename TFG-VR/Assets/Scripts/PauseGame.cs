using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public bool gamePaused = false;

    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (!gamePaused)
            {
                Time.timeScale = 0;
                gamePaused = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
                gamePaused = false;
                Time.timeScale = 1;
            }
        }
    }
}
