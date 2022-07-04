using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject Projectile;
    public int AttackDamage = 1;
    public float AttackRange = 10;
    public Vector3 ProjectileOffset = new Vector3(0, 0, 0);
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
        projectileScript.Instingator = gameObject;
        projectileScript.Range = AttackRange;
        projectileScript.Damage = AttackDamage;
    }
}
