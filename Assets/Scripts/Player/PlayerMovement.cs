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

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _fireAction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;

        PlayerControls = new PlayerInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        _targetPosition = _moveAction.ReadValue<Vector2>();
        var screenPos = _lookAction.ReadValue<Vector2>();

        var playerPlane = new Plane(Vector3.up, transform.position);  
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
        _rb.MovePosition(_rb.position + MoveSpeed * Time.fixedDeltaTime * new Vector3(_targetPosition.x, 0, _targetPosition.y));
        _rb.rotation = Quaternion.Slerp(_rb.rotation, _targetRotation, 7f * Time.fixedDeltaTime);
    }

    private void Fire(InputAction.CallbackContext ctx)
    {
        Debug.Log("Fire");
    }

    void OnEnable()
    {
        _moveAction = PlayerControls.Player.Move;
        _moveAction.Enable();

        _lookAction = PlayerControls.Player.Look;
        _lookAction.Enable();

        _fireAction = PlayerControls.Player.Fire;
        _fireAction.Enable();

        _fireAction.performed += Fire;
    }

    void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
        _fireAction.Disable();
    }
}
