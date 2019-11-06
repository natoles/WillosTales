using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    private ISelectionResponse[] _selectionResponse;
    private Transform _selection;

    private void Awake()
    {
        _selectionResponse = GetComponents<ISelectionResponse>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Deselection
        if (_selection != null)
            foreach (var response in _selectionResponse)
                response.OnDeselect(_selection);


        // Creating a Ray
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Selection determination
        _selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
                _selection = selection;
            
        }

        // Selection
        if (_selection != null)
            foreach (var response in _selectionResponse)
                response.OnSelect(_selection);
    }
}