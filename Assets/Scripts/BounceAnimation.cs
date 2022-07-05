using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    public float BounceSpeed = 8f;
    public float BounceAmplitude = 0.05f;
    public float RotationSpeed = 90f;

    private float _startHeight;

    private void Awake()
    {
        _startHeight = transform.position.y;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var finalHeight = _startHeight + BounceAmplitude * Mathf.Sin(Time.time * BounceSpeed);
        var pos = transform.localPosition;

        pos.y = finalHeight;
        transform.localPosition = pos;

        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }
}
