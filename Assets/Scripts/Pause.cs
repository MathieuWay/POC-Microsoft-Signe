using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour

    
{
    public int index;
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject didacticiel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) { 
        if (isPaused == false)
        {
            isPaused = true;
        }
        else if (isPaused == true)
        {
            isPaused = false;
        }
        }

        if (isPaused == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if (isPaused == false)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Reprendre()
    {
        if (isPaused == true)
        {
            isPaused = false;
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(index);
    }


    public void OpenHelp()
    {
        didacticiel.SetActive(true);
    }
    public void CloseHelp()
    {
        didacticiel.SetActive(false);
    }
    
    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    
}
