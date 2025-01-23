using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/Input/Reader")]
public class InputReaderSO : ScriptableObject, Control.IPlayerActions
{
    private Control _control;

    public event Action OnKeyPress; 
    public event Action OnKeyRelease; 
    
    #region Initialize section

    private void OnEnable()
    {
        _control ??= new Control();
        
        _control.Player.SetCallbacks(this);
        _control.Enable();
    }

    private void OnDisable()
    {
        _control.Disable();
    }

    #endregion

    public void OnPress(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnKeyPress?.Invoke();
        if(context.canceled)
            OnKeyRelease?.Invoke();
    }
}
