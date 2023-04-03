using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Handler", menuName = "Input Handler")]
public class InputHandler : ScriptableObject, CustomInput.IGameplayActions
{
    private CustomInput input;

    public UnityAction<Vector2> OnMoveAction;
    public UnityAction OnPauseAction;

    private void OnEnable()
    {
        if (input == null)
        {
            input = new CustomInput();
        }

        input.Gameplay.SetCallbacks(this);

        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.Disable();
    }

    //public void OnSetDirection(InputAction.CallbackContext context)
    //{
    //    //Debug.Log("Direction Key Pressed: " + context.ReadValue<Vector2>() + " " + context.phase);

    //    if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Canceled)
    //    {
    //        OnSetDirectionAction?.Invoke(context.ReadValue<Vector2>());
    //    }
    //}

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnPauseAction?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log("Direction Key Pressed: " + context.ReadValue<Vector2>() + " " + context.phase);

        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Canceled)
        {
            OnMoveAction?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
