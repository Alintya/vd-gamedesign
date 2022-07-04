using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 5f;
    public float Range = 500f;


    private float _travelTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Speed * Time.fixedDeltaTime);
        _travelTime += Time.fixedDeltaTime;

        if (_travelTime * Speed >= Range)
        {
            Destroy(gameObject);
        }
    }
}
