using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject ResumeButton;
    [SerializeField]private GameVariables gameVariables;

    public void OnChangeSoundVolume(float value)
    {
        SoundManager.Instance.OnChangeSoundVolume(value);

    }
    public void OnChangeMusicVolume(float value)
    {
        SoundManager.Instance.OnChangeMusicVolume(value);
    }
    public void OnChangeInvertMouse(bool value)
    {
        if(value){
        PlayerController.Instance.mouseVerticalVector = Vector3.right;
        }
        else{
           PlayerController.Instance.mouseVerticalVector = -Vector3.right; 
        }
    }
    public void OnChangeMouseSensitivity(float value)
    {
        // fast 3.0f
        PlayerController.Instance.rotationPowerX  = value * 3;
        PlayerController.Instance.rotationPowerY  = value * 3;
        gameVariables.MouseSensitivity  = value;
    }
    public void OnChangeCredits()
    {
        // SETTINGS OFF
        // CREDITS ON  - > BACK BUTTON
    }
    public void OnChangeGameStart()
    {

    }
    public void OnChangeGamePaused()
    {

    }
    public void OnChangeGameQuit()
    {
        
    }
}
