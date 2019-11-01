using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    [SerializeField] private string selectableTag = "Selectable"; 
    [SerializeField] private Material highlightMaterial; 
    [SerializeField] private Material defaultMaterial; 

    private Transform _selection;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (_selection != null) 
        {
            var SelectionRenderer = _selection.GetComponent<Renderer>();
            SelectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) 
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var SelectionRenderer = selection.GetComponent<Renderer>();
                if (SelectionRenderer != null) 
                {
                    SelectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
            }
        }
    }
}
