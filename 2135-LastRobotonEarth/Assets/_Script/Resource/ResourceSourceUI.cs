using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceSourceUI : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;

    // Start is called before the first frame update
    void Start()
    {
        popupPanel.SetActive(false);
    }

    
    public void OnCrosshairEnter() {
        popupPanel.SetActive(true);
        popupPanel.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    public void OnCrosshairExit(){
        popupPanel.SetActive(false);
    }
    
}
