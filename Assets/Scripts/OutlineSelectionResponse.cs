using UnityEngine;

public class OutlineSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField, Range(0f, 10f)]
    private float activationMaxDist = 5f;
    
    public void OnSelect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        if (outline != null && Vector3.Distance(Camera.main.gameObject.transform.position, selection.position) < activationMaxDist) 
            outline.OutlineWidth = 10;
    }
    
    public void OnDeselect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        if (outline != null) outline.OutlineWidth = 0;
    }
}
