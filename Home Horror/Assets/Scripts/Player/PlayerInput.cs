using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
{
    public PlayerControls PlayerControls { get; private set; }
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    [SerializeField] private bool holdToSprint = true;
    public bool SprintToggleOn { get; private set; }
    
    public bool JumpPressed { get; private set; }
    
    public bool InteractPressed { get; private set; }
    
    public bool RepairMoneyPressed { get; private set; }
    
    public bool RepairMaterialsPressed { get; private set; }

    public SubtitleManager subtitleManager; //keep here for later use 

    private void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.PlayerLocomotionMap.Enable();
        PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        PlayerControls.PlayerLocomotionMap.Disable();
        PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
    }

    private void LateUpdate()
    {
        JumpPressed = false;
        InteractPressed = false;
        RepairMoneyPressed = false;
        RepairMaterialsPressed = false;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        JumpPressed = true;
    }

    public void OnToggleSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SprintToggleOn = holdToSprint || !SprintToggleOn;
        }
        else if (context.canceled)
        {
            SprintToggleOn = !holdToSprint && SprintToggleOn;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractPressed = true;
        }
    }

    public void OnRepairMoney(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RepairMoneyPressed = true;
        }
    }

    public void OnRepairMaterial(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RepairMaterialsPressed = true;
        }
    }
}
