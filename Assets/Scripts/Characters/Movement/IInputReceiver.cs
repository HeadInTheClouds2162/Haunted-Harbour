using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputReceiver
{
    public void BindControls(PlayerInput reference);
    public void UnbindControls(PlayerInput reference);
}