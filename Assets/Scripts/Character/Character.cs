using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject Projectile;
    public float AttackDamage = 1;
    public float AttackRange = 10;
    public Vector3 ProjectileOffset = new Vector3(0, 0, 0);
    private CharacterController characterController;

    private void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        var projectileInstance = Instantiate(Projectile, transform.position + ProjectileOffset, transform.rotation);
        var projectileScript = projectileInstance.GetComponent<Projectile>();
        projectileScript.Velocity =  characterController.velocity/2 + Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * projectileScript.Velocity;
        projectileScript.Instingator = gameObject;
        projectileScript.Range = AttackRange;
        projectileScript.Damage = AttackDamage;
    }
}
