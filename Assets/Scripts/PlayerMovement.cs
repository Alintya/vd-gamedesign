using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;

    
    private Camera _cam;
  
    private Rigidbody _rb;
    private Vector2 _movement;
    private Vector2 _mousePos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        //_mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        var playerPlane = new Plane(Vector3.up, transform.position);
        var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            var hitPoint = ray.GetPoint(hitDist);
            var targetRotation = Quaternion.LookRotation(hitPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, 7f * Time.deltaTime);
        }
    }
    
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + new Vector3(_movement.x, 0, _movement.y) * MoveSpeed * Time.fixedDeltaTime);
        
        //Vector2 lookDir = _mousePos - new Vector2(_rb.position.x, _rb.position.y);
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //_rb.rotation = Quaternion.Euler(0, angle, 0);
    }
}
