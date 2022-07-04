using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float SmoothingFactor = 0.3f;
    public float Height = 10f;

    
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
        var pos = _player.position;
        pos.y += Height;
        pos.z -= 10f;

        transform.position = Vector3.SmoothDamp(transform.position, pos, ref _velocity, SmoothingFactor);
    }
}
