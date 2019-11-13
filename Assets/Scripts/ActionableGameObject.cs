using UnityEngine;

public class ActionableGameObject : MonoBehaviour
{
    public MovingPlatform target;        // MovingPlatform to move
    public RotatingPlatform targetRotate;// RotatingPlatforms to move
    public bool reversible;              // Can the moving platform be activated and deactivated ?
    private bool _targetInitialState;    // Initial state of the target (bool), only used for non-reversible object

    private void Start()
    {
        _targetInitialState = target.activated;
    }

    public void ActivateObject()
    {
        if (reversible)
        {
            target.activate(!target.activated);
            targetRotate.activate(!targetRotate.activated);
        }
        else
            target.activate(!_targetInitialState);
        
    }
}
