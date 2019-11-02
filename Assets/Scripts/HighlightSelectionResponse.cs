using UnityEngine;

internal class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField] public Material highlightMaterial; 
    [SerializeField] public Material defaultMaterial;
    
    public void OnSelect(Transform selection)
    {
        var SelectionRenderer = selection.GetComponent<Renderer>();
        if (SelectionRenderer != null)
        {
            SelectionRenderer.material = this.highlightMaterial;
        }
    }
    
    public void OnDeselect(Transform selection)
    {
        var SelectionRenderer = selection.GetComponent<Renderer>();
        if (SelectionRenderer != null)
        {
            SelectionRenderer.material = this.defaultMaterial;
        }
    }
}
