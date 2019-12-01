using UnityEngine;

public class GrapplingHookSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField, Range(0f, 50f)] public float activationMaxDist = 20f;

    public void OnSelect(Transform selection)
    {
        var grapplingHook = selection.GetComponent<GraspableGameObject>();
        var outline = selection.GetComponent<Outline>();
        
        if (outline != null && Vector3.Distance(Camera.main.gameObject.transform.position, selection.position) <
            activationMaxDist)
            outline.OutlineWidth = 10;

        if (Input.GetKeyDown(KeyCode.J) && grapplingHook != null &&
            Vector3.Distance(Camera.main.gameObject.transform.position, selection.position) <
            activationMaxDist)
        {
            grapplingHook.Trigger(selection);
        }
    }

    public void OnDeselect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        if (outline != null) outline.OutlineWidth = 0;
    }
}