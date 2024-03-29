using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class RotateMap : MonoBehaviour
{
    #region Input Actions
    [SerializeField]
    private InputActionAsset _actions;

    public InputActionAsset actions
    {
        get => _actions;
        set => _actions = value;
    }

    protected InputAction rightClickPressedInputAction { get; set; }

    protected InputAction mouseLookInputAction { get; set; }

    #endregion

    #region Variables

    private bool _rotateAllowed;

    private Camera _camera;

    [SerializeField] private float _rotationSpeed = 1f;

    [SerializeField] private bool _inverted;

    [SerializeField] private bool _turnOffCursor;

    private Quaternion _initialRotation;

    #endregion

    private void Awake()
    {
        InitializeInputSystem();
    }

    private void Start()
    {
        if (_turnOffCursor)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

        _camera = Camera.main;
        _initialRotation = transform.rotation;
    }

    private void InitializeInputSystem()
    {
        rightClickPressedInputAction = actions.FindAction("Right Click");
        if (rightClickPressedInputAction != null)
        {
            rightClickPressedInputAction.started += OnRightClickPressed;
            rightClickPressedInputAction.performed += OnRightClickPressed;
            rightClickPressedInputAction.canceled += OnRightClickPressed;
        }

        mouseLookInputAction = actions.FindAction("Mouse Look");


        actions.Enable();
    }

    protected virtual void OnRightClickPressed(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            _rotateAllowed = true;
        }
        else if (context.canceled)
            _rotateAllowed = false;
    }

    protected virtual Vector2 GetMouseLookInput()
    {
        if (mouseLookInputAction != null)
            return mouseLookInputAction.ReadValue<Vector2>();

        return Vector2.zero;
    }
    private void Update()
    {
        if (!_rotateAllowed)
            return;

        Vector2 mouseDelta = GetMouseLookInput();

        mouseDelta *= _rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * (_inverted ? 1 : -1), mouseDelta.x, Space.World);

    }
}