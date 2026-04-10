using UnityEngine;

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput action;
    private IInputReceiver[] _receiver;

    private void Awake()
    {
        _receiver = GetComponentsInChildren<IInputReceiver>();
        foreach (IInputReceiver receiver in _receiver)
        {
            receiver.BindControls(action);

        }
    }

    private void OnDestroy()
    {
        foreach (IInputReceiver receiver in _receiver)
            receiver.UnbindControls(action);
    }

    public void SetLookDirection(bool faceRight)
    {
        transform.localScale = new Vector3(faceRight ? -1 : 1, 1, 1);
    }

}