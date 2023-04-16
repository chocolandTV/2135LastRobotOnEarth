using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField] public Material highlightMaterial;
    [SerializeField] public Material oldMaterial;
    public void OnSelect(Transform selection)
    {
        
            var selectionRenderer = selection.GetComponent<Renderer>();
            if(selectionRenderer != null)
            {
                
                selectionRenderer.material = highlightMaterial;
                selection.GetComponent<ResourceSourceUI>().OnCrosshairEnter();
            }
        
    }
    public void OnDeselect(Transform selection)
    {
        
            var selectionRenderer = selection.GetComponent<Renderer>();
            if(selectionRenderer != null)
            {
                
                selectionRenderer.material = oldMaterial;
                selection.GetComponent<ResourceSourceUI>().OnCrosshairExit();
            }
        
    }

}
