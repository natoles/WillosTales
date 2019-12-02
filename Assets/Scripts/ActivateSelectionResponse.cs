using UnityEngine;

public class ActivateSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField, Range(0f, 10f)] public float activationMaxDist = 5f;

    public void OnSelect(Transform selection)
    {
        var actionable = selection.GetComponent<ActionableGameObject>();
        var outline = selection.GetComponent<Outline>();

        if (actionable != null)
        {
            if (outline != null && Vector3.Distance(Camera.main.gameObject.transform.position, selection.position) <
                activationMaxDist)
                outline.OutlineWidth = 10;

            if (Input.GetMouseButtonDown(0) &&
                Vector3.Distance(Camera.main.gameObject.transform.position, selection.position) < activationMaxDist)
                actionable.ActivateObject();
        }
    }

    public void OnDeselect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        if (outline != null) outline.OutlineWidth = 0;
    }
}