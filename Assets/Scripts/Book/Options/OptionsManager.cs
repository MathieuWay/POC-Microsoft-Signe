using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{

    public AudioMixer mixer;

    public GameObject options;
    public GameObject keymap;
    public GameObject credits;
    public GameObject objectif;
    public Button quit;

    public Dropdown dropResolution;

    private Resolution[] resolutions;

    void Start()
    {
        #region dropdown
        resolutions = Screen.resolutions;
        dropResolution.ClearOptions();

        int currentRes = 0;

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentRes = i;
        }

        dropResolution.AddOptions(options);
        dropResolution.value = currentRes;
        dropResolution.RefreshShownValue();
        #endregion

    }

    public void SetActive(bool state)
    {
        if (options.activeSelf != state)
        {
            options.SetActive(state);
            SetActiveQuit(state);
            
        }
    }

    public void ChangePage(int page)
    {
        
        switch (page)
        {
            case 0:
                credits.SetActive(true);
                objectif.SetActive(false);
                break;
            case 1:
                objectif.SetActive(true);
                credits.SetActive(false);
                break;
            default:
                Debug.LogError("Case non asigned (OptionsManager");
                break;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void SetActiveQuit(bool state)
    {
        quit.gameObject.SetActive(state);
        //quit.transform.GetChild(0).gameObject.SetActive(state);
    }

    #region Boutons des options
    public void SetResolution(Dropdown drop)
    {
        Resolution resolution = resolutions[drop.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(Slider slider)
    {
        mixer.SetFloat("volume", slider.value);
    }

    public void Mute(bool state)
    {
        
    }

    public void SetFullscreen(bool state)
    {
        Screen.fullScreen = state;
    }

    public void ToggleKeyMap()
    {
        keymap.SetActive(!keymap.activeSelf);
    }
    #endregion
}
