using UnityEngine;

public class ActivateSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField, Range(0f, 10f)]
    private float activationMaxDist = 5f;
    
    public void OnSelect(Transform selection)
    {
        var actionable = selection.GetComponent<ActionableGameObject>();
        if (Input.GetMouseButtonDown(0) && Vector3.Distance(Camera.main.gameObject.transform.position, selection.position) < activationMaxDist)
            actionable.ActivateObject();
    }

    public void OnDeselect(Transform selection)
    {
    }
}
