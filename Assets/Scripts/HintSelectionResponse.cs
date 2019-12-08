using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField, Range(0f, 10f)] public float activationMaxDist = 5f;
    private Hint hint;

    public void OnSelect(Transform selection)
    {
        hint = selection.GetComponent<Hint>();

        if (hint != null && Vector3.Distance(Camera.main.gameObject.transform.position, selection.position) <
            activationMaxDist)
        {
            hint.trigger();
        }
    }

    public void OnDeselect(Transform selection)
    {
    }
}