using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 20f;

    private CameraInputActions inputActions;
    private Vector2 moveInput;
    private float zoomInput;

    private void Awake()
    {
        inputActions = new CameraInputActions();
    }

    private void OnEnable()
    {
        inputActions.CameraMovement.Move.Enable();
        inputActions.CameraMovement.Move.performed += OnMove;
        inputActions.CameraMovement.Move.canceled += OnMove;

        inputActions.CameraMovement.Zoom.Enable();
        inputActions.CameraMovement.Zoom.performed += OnZoom;
        inputActions.CameraMovement.Zoom.canceled += OnZoom;
    }

    private void OnDisable()
    {
        inputActions.CameraMovement.Move.performed -= OnMove;
        inputActions.CameraMovement.Move.canceled -= OnMove;
        inputActions.CameraMovement.Move.Disable();

        inputActions.CameraMovement.Zoom.performed -= OnZoom;
        inputActions.CameraMovement.Zoom.canceled -= OnZoom;
        inputActions.CameraMovement.Zoom.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnZoom(InputAction.CallbackContext ctx)
    {
        zoomInput = ctx.ReadValue<float>();
    }

    private void Update()
    {
        // Bewegung
        if (moveInput != Vector2.zero)
        {
            float t = Mathf.InverseLerp(minZoom, maxZoom, Camera.main.orthographicSize);
            float currentMoveSpeed = Mathf.Lerp(moveSpeed, moveSpeed * 2f, t);

            Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
            transform.position += movement * currentMoveSpeed * Time.deltaTime;
        }

        // Zoom
        if (zoomInput != 0f)
        {
            Camera cam = Camera.main;
            float newSize = cam.orthographicSize - zoomInput * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }
}