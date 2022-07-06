using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject Projectile;
    public float AttackDamage = 1, FireRate = 2;
    public float AttackRange = 10;
    public Vector3 ProjectileOffset = new Vector3(0, 0, 0);
    private CharacterController _characterController;
    private bool _canFire = true;
    protected bool _fireing = false;

    protected virtual void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_fireing)
        {
            Fire();
        }
    }
    protected virtual void FixedUpdate()
    {

    }

    public bool Fire()
    {
        if (!_canFire)
            return false;

        var projectileInstance = Instantiate(Projectile, transform.position + ProjectileOffset, transform.rotation);
        var projectileScript = projectileInstance.GetComponent<Projectile>();
        projectileScript.Velocity = _characterController.velocity / 2 + Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * projectileScript.Velocity;
        projectileScript.Instigator = gameObject;
        projectileScript.Range = AttackRange;
        projectileScript.Damage = AttackDamage;

        _canFire = false;
        Invoke(nameof(Reload), 1 / FireRate);
        return true;
    }

    public void Reload()
    {
        _canFire = true;
    }
    public virtual void InteractWith(Interactable other)
    {
        other.Interact(this);
    }
}
