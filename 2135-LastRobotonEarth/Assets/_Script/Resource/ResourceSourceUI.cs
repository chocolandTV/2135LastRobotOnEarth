using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceSourceUI : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI resourceQuantitiyText;
    [SerializeField] private ResourceSource resource;
    // Start is called before the first frame update
    void Start()
    {
        resourceQuantitiyText.text = resource.quantity.ToString();
    }

    // Update is called once per frame
    void OnMouseEnter() {
        popupPanel.SetActive(true);
    }
    void OnMouseExit(){
        popupPanel.SetActive(false);
    }
    
}
