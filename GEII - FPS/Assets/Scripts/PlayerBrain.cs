using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField] private PlayerMotor playerMotor;
    [SerializeField] private PlayerCamera playerCamera;

    //Test
    [SerializeField] private Raycaster raycaster;

    private Input_PlayerControls input;
    private Vector2 moveInput;
    private Vector2 lookInput;

    private void Awake()
    {
        input = new Input_PlayerControls();

        Debug.Log("PlayerBrain Awake: Input system initialized.");
    }

    private void OnEnable()
    {
        SubscribeToEvents();
        input.Enable();

        // Optional: lock cursor for FPS testing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        input.Disable();
        UnsubscribeFromEvents();
    }

    private void Update()
    {        
        playerMotor.Move(moveInput);
        playerMotor.Rotate(lookInput.x);
        playerCamera.Pitch(lookInput.y);

        playerMotor.UpdateMotor();

        //Testing
        Camera mainCam = Camera.main;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        raycaster.GetRaycastTarget(ray, 10);
    }

    private void SubscribeToEvents()
    {
        input.Player.Look.performed += OnLook;
        input.Player.Look.canceled += OnLook;

        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;

        input.Player.Jump.performed += OnJump;
    }

    private void UnsubscribeFromEvents()
    {
        input.Player.Look.performed -= OnLook;
        input.Player.Look.canceled -= OnLook;

        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;

        input.Player.Jump.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        playerMotor.Jump();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
