using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuOptions : MonoBehaviour
{
    private GameObject pauseMenuDisplay;

    void Start(){
        pauseMenuDisplay = gameObject.transform.GetChild(0).gameObject;
    }
    public void Pause(float gameTime){
        if(pauseMenuDisplay.activeSelf)
        {
            pauseMenuDisplay.SetActive(false);
            Time.timeScale = gameTime;
        }
        else{
            pauseMenuDisplay.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public bool IsPause(){
        return pauseMenuDisplay.activeSelf;
    }

    public void Quit(string scenename)
    {
        Application.Quit();
    }
    
}
