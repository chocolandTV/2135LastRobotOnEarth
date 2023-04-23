using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class MainMenuManager : MonoBehaviour
{
    // MENUS
    [SerializeField] private GameObject MainMenu;
    [SerializeField]private GameObject HudMenu;
    // BUTTONS
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject ResumeButton;
    [SerializeField] private GameObject CreditBackButton;
    [SerializeField] private GameObject CreditMainButton;
    // SETTING MENU PANELS
    [SerializeField]private GameObject SettingMenuPanel;
    [SerializeField]private GameObject CreditMenuPanel;
    [SerializeField]private GameVariables gameVariables;
    // SLIDERS AND TOGGLES
    [SerializeField]private Slider SoundSlider;
    [SerializeField]private Slider MusicSlider;
    [SerializeField]private Slider MouseSensitivitySlider;
    [SerializeField] private Toggle MouseInvertToggle;
    
    // GAME VARIABLES
    private bool isGamePaused = false;
    private void Start() {
        SoundSlider.onValueChanged.AddListener(delegate {OnChangeSoundVolume();});
        MusicSlider.onValueChanged.AddListener (delegate{OnChangeMusicVolume();});
        MouseSensitivitySlider.onValueChanged.AddListener(delegate{OnChangeMouseSensitivity();});
        MouseInvertToggle.onValueChanged.AddListener(delegate{OnChangeInvertMouse();});
        InputManager.OnPauseGame +=OnPauseGameInput;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }
    private void OnPauseGameInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(!isGamePaused)
            {
                OnChangeGamePaused();
                isGamePaused =true;
            }
            else{
                OnChangeGameResume();
                isGamePaused = false;
            }
        }
    }
    public void OnChangeSoundVolume()
    {
        SoundManager.Instance.OnChangeSoundVolume(SoundSlider.value);
        Debug.Log( " CHANGE SoundsSlider TO : " + SoundSlider.value);
        SoundManager.Instance.PlaySound(SoundManager.Sound.Robot_Happy, PlayerController.Instance.transform.position);
    }
    public void OnChangeMusicVolume()
    {
        SoundManager.Instance.OnChangeMusicVolume(MusicSlider.value);
        Debug.Log( " CHANGE MusicSlider TO : " + MusicSlider.value);
    }
    public void OnChangeInvertMouse()
    {
        if(MouseInvertToggle.isOn){
        PlayerController.Instance.mouseVerticalVector = Vector3.right;
        Debug.Log( " CHANGE TOGGLE TO : " + MouseInvertToggle.isOn);
        }
        else{
           PlayerController.Instance.mouseVerticalVector = -Vector3.right; 
           Debug.Log( " CHANGE TOGGLE TO : " + MouseInvertToggle.isOn);
        }
    }
    public void OnChangeMouseSensitivity()
    {
        // fast 3.0f
        Debug.Log( " CHANGE MouseSlider TO : " + MouseSensitivitySlider.value);
        PlayerController.Instance.rotationPowerX  = MouseSensitivitySlider.value * 6;
        PlayerController.Instance.rotationPowerY  = MouseSensitivitySlider.value * 6;
        gameVariables.MouseSensitivity  = MouseSensitivitySlider.value;
    }
    public void OnChangeCredits()
    {
        // SETTINGS OFF
        SettingMenuPanel.SetActive(false);
        CreditMenuPanel.SetActive(true);
        CreditBackButton.SetActive(true);
        CreditMainButton.SetActive(false);
        // CREDITS ON  - > BACK BUTTON
    }
    public void OnChangeBackToMenuFromCredits()
    {
        // SETTINGS OFF
        SettingMenuPanel.SetActive(true);
        CreditMenuPanel.SetActive(false);
        CreditBackButton.SetActive(false);
        CreditMainButton.SetActive(true);
        // CREDITS ON  - > BACK BUTTON
    }
    public void OnChangeGameStart()
    {
        StartButton.SetActive(false);
        ResumeButton.SetActive(true);
        HudMenu.SetActive(true);
        MainMenu.SetActive(false);
        PlayerController.Instance.StartGame();
        // UI OFF HUD ON
    }
    public void OnChangeGamePaused()
    {
        HudMenu.SetActive(false);
        MainMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OnChangeGameResume()
    {
        HudMenu.SetActive(true);
        MainMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OnChangeGameQuit()
    {
        Application.Quit();
    }
}
