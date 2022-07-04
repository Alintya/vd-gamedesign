using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    public float SmoothingFactor = 0.3f;
    public Vector3 Offset = new Vector3(0, 10f, -10f);
    public bool Panning = true;
    public float PanningAmmount = 1;
    public float PanningMouseDistance = 0;

    private Transform _player;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: stop if player is ded
        var pos = _player.position + Offset;
        if (Panning && Application.isPlaying)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 mousePosCenter = new Vector3(mousePos.x - Screen.width / 2, mousePos.y - Screen.height / 2, mousePos.z);
            pos.x += PanningAmmount * Mathf.Clamp(mousePosCenter.x / (Screen.width / 2), -1, 1);
            pos.y += PanningAmmount * Mathf.Clamp(mousePosCenter.y / (Screen.height / 2), -1, 1);
        }
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, pos, ref _velocity, SmoothingFactor);
        transform.LookAt(smoothPos-Offset);

        transform.position = smoothPos;

    }
}
