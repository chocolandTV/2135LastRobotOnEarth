using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    
    private Transform _selection;
    private ISelectionResponse _selectionResponse;
    private void Awake()
    {
        _selectionResponse = GetComponent<ISelectionResponse>();
        Debug.Log(Camera.main);
        
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Application is focussed");
        }
        else
        {
            Debug.Log("Application lost focus");
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (_selection != null)
        {
            _selectionResponse.OnDeselect(_selection);
        }
        // CREATING RAY
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f,0f));
        // Selection Determination
        _selection = null;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag("Scrap"))
            {
                _selection = selection;
            }
        }
        // END DEFINE
        if (_selection != null)
        {
            _selectionResponse.OnSelect(_selection);
        }
    }
    
}


