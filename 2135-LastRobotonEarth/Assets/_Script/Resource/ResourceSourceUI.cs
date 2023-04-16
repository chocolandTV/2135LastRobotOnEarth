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

    // Update is called once per frame
    void OnMouseEnter() {
        popupPanel.SetActive(true);
    }
    void OnMouseExit(){
        popupPanel.SetActive(false);
    }
    
}
