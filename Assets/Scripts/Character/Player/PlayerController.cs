using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputActions PlayerControls;
    private Character _character;
    private InputAction _fireAction;
    
    private void Awake()
    {
        _character = GetComponent<Character>();
        PlayerControls = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Fire(InputAction.CallbackContext ctx)
    {
        _character.Fire();
    }
    
    void OnEnable()
    {
        _fireAction = PlayerControls.Player.Fire;
        _fireAction.Enable();

        _fireAction.performed += Fire;
    }

    void OnDisable()
    {
        _fireAction.Disable();
    }
}
