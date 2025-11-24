using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera playerCamera;

    [Header("Movement Settings")]
    public float walkAcceleration = 35f;
    public float walkSpeed = 4f;
    public float sprintAcceleration = 50f;
    public float sprintSpeed = 7f;
    public float drag = 20f;
    public float gravity = 25f;
    public float jumpSpeed = 1.0f;
    public float movingThreshold = 0.01f;

    [Header("Camera Settings")]
    public float lookSenseH = 0.1f;
    public float lookSenseV = 0.1f;
    public float lookLimitV = 89f;
    
    private PlayerInput input;
    private PlayerState state;
    
    private Vector2 camRotation = Vector2.zero;
    private Vector2 playerTargetRotation = Vector2.zero;
    
    private float verticalVelocity = 0f;
    
    private Interactable currentInteractable;
    private PlayerInventory inventory;
    private GameUI gameUI;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        input = GetComponent<PlayerInput>();
        state = GetComponent<PlayerState>();
        inventory = GetComponent<PlayerInventory>();
        gameUI = FindFirstObjectByType<GameUI>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateState();
        HandleLateralMovement();
        HandleVerticalMovement();
        HandleInteraction();
    }

    private void LateUpdate()
    {
        HandleLook();
    }

    private void UpdateState()
    {
        bool isMovementInput = input.MoveInput != Vector2.zero; 
        bool isMovingLaterally = IsMovingLaterally(); 
        bool isSprinting = input.SprintToggleOn && isMovingLaterally; 
        bool isGrounded = IsGrounded(); 
        
        PlayerMovementState lateralState = isSprinting ? PlayerMovementState.Sprinting : 
            isMovingLaterally || isMovementInput ? PlayerMovementState.Walking : PlayerMovementState.Idling; 
        
        state.SetPlayerMovementState(lateralState); 
        
        // Control Airborne State
        if (!isGrounded && characterController.velocity.y > 0f)
        {
            state.SetPlayerMovementState(PlayerMovementState.Jumping);
        } 
        else if (!isGrounded && characterController.velocity.y <= 0f)
        {
            state.SetPlayerMovementState(PlayerMovementState.Falling);
        }
    }

    private void HandleLateralMovement()
    {
        // Create quick references for current state
        bool isSprinting = state.CurrentPlayerMovementState == PlayerMovementState.Sprinting; 
        bool isGrounded = state.InGroundedState(); 
        
        // State dependent acceleration and speed
        float lateralAcceleration = isSprinting ? sprintAcceleration : walkAcceleration; 
        float clampLateralMagnitude = isSprinting ? sprintSpeed : walkSpeed; 
        
        Vector3 camForward = new Vector3(playerCamera.transform.forward.x, 0f, playerCamera.transform.forward.z).normalized; 
        Vector3 camRight = new Vector3(playerCamera.transform.right.x, 0f, playerCamera.transform.right.z).normalized; 
        
        Vector3 moveDirection = camRight * input.MoveInput.x + camForward * input.MoveInput.y; 
        Vector3 moveDelta = moveDirection * lateralAcceleration * Time.deltaTime; 
        Vector3 newVelocity = characterController.velocity + moveDelta; 
        
        // Apply drag
        Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime; 
        newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero; newVelocity = Vector3.ClampMagnitude(newVelocity, clampLateralMagnitude); 
        newVelocity.y += verticalVelocity; 
        
        characterController.Move(newVelocity * Time.deltaTime);
    }

    private void HandleVerticalMovement()
    {
        bool isGrounded = state.InGroundedState();

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0f;
        }
        
        verticalVelocity -= gravity * Time.deltaTime;

        if (input.JumpPressed && isGrounded)
        {
            verticalVelocity += Mathf.Sqrt(jumpSpeed * 3 * gravity);
        }
    }

    private void HandleLook()
    {
        camRotation.x += lookSenseH * input.LookInput.x;
        camRotation.y = Mathf.Clamp(camRotation.y - lookSenseV * input.LookInput.y, -lookLimitV, lookLimitV);

        playerTargetRotation.x += transform.eulerAngles.x + lookSenseH * input.LookInput.x;
        transform.rotation = Quaternion.Euler(0f, playerTargetRotation.x, 0f);
        
        playerCamera.transform.rotation = Quaternion.Euler(camRotation.y, camRotation.x, 0f);
    }

    private bool IsMovingLaterally()
    {
        Vector3 lateralVelocity = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z);
        
        return lateralVelocity.magnitude > movingThreshold;
    }

    private bool IsGrounded()
    {
        return characterController.isGrounded;
    }
    
    private void HandleInteraction()
    {
        if (input.InteractPressed && currentInteractable != null && currentInteractable.CompareTag("Pickup"))
        {
            currentInteractable.Interact();
            gameUI.HideMaterialInfoPopup();
        }
        
        if (input.RepairMoneyPressed && currentInteractable is DegradationController degradation && currentInteractable.CompareTag("RepairBox"))
        {
            bool repaired = degradation.TryRepairSystem(inventory, DegradationController.RepairMethod.Money);
            if (!repaired) Debug.Log("Not enough money.");
        }

        if (input.RepairMaterialsPressed && currentInteractable is DegradationController degradation2 && currentInteractable.CompareTag("RepairBox"))
        {
            bool repaired = degradation2.TryRepairSystem(inventory, DegradationController.RepairMethod.Material);
            if (!repaired) Debug.Log("Not enough resources.");
        }

        if (input.InteractPressed&&currentInteractable is DoorController)
        {
            currentInteractable.Interact();
        }

        if (input.InteractPressed && currentInteractable is DayEndController)
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            currentInteractable = interactable;
            Debug.Log("Triggered Interactable");
            
            if (other.CompareTag("Pickup") && other.TryGetComponent(out MaterialController materialController))
            {
                gameUI.HideRepairInfoPopup();
                
                string name = materialController.MaterialType;
                int amount = materialController.MaterialAmount;

                gameUI.ShowMaterialInfoPopup(name, amount);
            }

            if (other.CompareTag("RepairBox") && other.TryGetComponent(out DegradationController degradation))
            {
                gameUI.HideMaterialInfoPopup();
                
                var stage = degradation.CurrentStage;

                if (stage != null)
                {
                    int moneyCost = stage.moneyCost;
                    string materialName = stage.materialType;
                    int materialAmount = stage.materialAmount;

                    gameUI.ShowRepairInfoPopup(moneyCost, materialName, materialAmount);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentInteractable != null && other.gameObject == currentInteractable.gameObject)
        {
            currentInteractable = null;
            
            if (other.CompareTag("Pickup"))
            {
                gameUI.HideMaterialInfoPopup();
            }

            if (other.CompareTag("RepairBox"))
            {
                gameUI.HideRepairInfoPopup();
            }
        }
    }
}
