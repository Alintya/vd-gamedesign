using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public PlayerInputActions PlayerControls;

    private Camera _cam;

    private Rigidbody _rb;
    private Vector2 _targetPosition;
    private Quaternion _targetRotation;
    private CharacterController characterController;

    private InputAction _moveAction;
    private InputAction _lookAction;
    private List<string> gamepadBindings = new List<string>();
    private bool _useController;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;

        PlayerControls = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveAction.activeControl != null)
        {
            bool flag = _useController;
            _useController = gamepadBindings.Contains(_moveAction.activeControl?.name);
            if (flag != _useController) OnEnable();
        }

        _targetPosition = _moveAction.ReadValue<Vector2>();

        var screenPos = _lookAction.ReadValue<Vector2>() * (_useController ? new Vector2(Screen.width / 2, Screen.height / 2) : Vector2.one) 
                                                         + (_useController ? new Vector2(Screen.width / 2, Screen.height / 2) : Vector2.zero);
        Debug.Log(_lookAction.ReadValue<Vector2>());

        var playerPlane = new Plane(Vector3.up, transform.position + new Vector3(0, 0.5f, 0));
        var ray = _cam.ScreenPointToRay(screenPos);

        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            var hitPoint = ray.GetPoint(hitDist);
            _targetRotation = Quaternion.LookRotation(hitPoint - transform.position);
            _targetRotation.x = 0;
            _targetRotation.z = 0;
        }
    }

    void FixedUpdate()
    {
        characterController.Move(MoveSpeed * Time.fixedDeltaTime * new Vector3(_targetPosition.x, 0, _targetPosition.y));
        // _rb.MovePosition(_rb.position + MoveSpeed * Time.fixedDeltaTime * new Vector3(_targetPosition.x, 0, _targetPosition.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, 7f * Time.fixedDeltaTime).normalized;
    }

    void OnEnable()
    {

        // _lookAction.Disable();
        _moveAction = PlayerControls.Player.Move;
        _moveAction.Enable();
        foreach (var binding in _moveAction.bindings)
        {
            if (binding.effectivePath.Contains("<Gamepad>"))
            {
                gamepadBindings.Add(binding.effectivePath.Split("/").Last());
                Debug.Log(binding.path);
            }
        }

        _lookAction = PlayerControls.Player.Look;

        if (_useController)
        {
            _lookAction.bindingMask = new InputBinding()
            {
                // Select the keyboard binding based on its specific path.
                path = "<Gamepad>/rightStick"
            };
        }
        else
        {
            _lookAction.bindingMask = null;
        }
        _lookAction.Enable();
    }

    void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
    }
}
