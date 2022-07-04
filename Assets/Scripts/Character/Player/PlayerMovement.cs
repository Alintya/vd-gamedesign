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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;

        PlayerControls = new PlayerInputActions();
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _targetPosition = _moveAction.ReadValue<Vector2>();
        var screenPos = _lookAction.ReadValue<Vector2>();

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
        _moveAction = PlayerControls.Player.Move;
        _moveAction.Enable();

        _lookAction = PlayerControls.Player.Look;
        _lookAction.Enable();
    }

    void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
    }
}
