using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
{
    // [SerializeField] private Material highlightMaterial;
    // [SerializeField] private Material oldMaterial;
    
    public void OnSelect(Transform selection)
    {
        
        selection.GetComponent<ResourceSourceUI>().OnCrosshairEnter();
            // var selectionRenderer = selection.GetComponent<Renderer>();
            // if(selectionRenderer != null)
            // {
                
            //     //selectionRenderer.material = highlightMaterial;
            //     selection.GetComponent<ResourceSourceUI>().OnCrosshairEnter();
            // }
        
    }
    public void OnDeselect(Transform selection)
    {
        
        selection.GetComponent<ResourceSourceUI>().OnCrosshairExit();
            // var selectionRenderer = selection.GetComponent<Renderer>();
            // if(selectionRenderer != null)
            // {
                
            //     //selectionRenderer.material = oldMaterial;
            // }
        
    }

}
