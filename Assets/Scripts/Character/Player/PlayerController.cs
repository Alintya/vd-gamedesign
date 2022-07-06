using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{
    public PlayerInputActions PlayerControls;
    private InputAction _fireAction;

    protected override void Awake()
    {
        base.Awake();
        PlayerControls = new PlayerInputActions();
    }

    void OnEnable()
    {
        _fireAction = PlayerControls.Player.Fire;
        _fireAction.Enable();

        _fireAction.performed += (InputAction.CallbackContext ctx) => { _fireing = true; };
        _fireAction.canceled += (InputAction.CallbackContext ctx) => { _fireing = false; };
    }

    void OnDisable()
    {
        _fireAction.Disable();
    }
}
