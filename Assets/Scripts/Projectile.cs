using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Velocity = new Vector3(0, 0, 5);
    public float Range = 10f;
    public float Damage = 1;
    public bool IsEnemy, FriendlyFire, Collides, Penetrates;
    public GameObject Instingator;

    private float _travelTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        ColliderType type = ColliderType.OBSTACLE;
        if((other.tag == "Player" && !IsEnemy) || (other.tag == "Enemy" && IsEnemy)) type = ColliderType.FRIENDLY;
        if((other.tag == "Player" && IsEnemy) || (other.tag == "Enemy" && !IsEnemy)) type = ColliderType.OPPONENT;
        var projectile = other.gameObject.GetComponent<Projectile>();
        if(projectile && projectile.IsEnemy == IsEnemy) type = ColliderType.FRIENDLYPROJECTILE;
        if(projectile && projectile.IsEnemy != IsEnemy) type = ColliderType.OPPONENTPROJECTILE;

        if (other.gameObject == Instingator 
            || !FriendlyFire && type == ColliderType.FRIENDLY
            || !FriendlyFire && type == ColliderType.FRIENDLYPROJECTILE 
            || !Collides && (type == ColliderType.FRIENDLYPROJECTILE || type == ColliderType.OPPONENTPROJECTILE)
        )
            return;

        var health = other.gameObject.GetComponent<Health>();
        if (health) health.TakeDamage(Damage);

        if (Penetrates && (type == ColliderType.FRIENDLY || type == ColliderType.OPPONENT))
            return;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.Translate(Velocity * Time.fixedDeltaTime, Space.World);
        _travelTime += Time.fixedDeltaTime;

        if (_travelTime * Velocity.magnitude >= Range)
        {
            Destroy(gameObject);
        }
    }

    private enum ColliderType 
    {
        FRIENDLY, OPPONENT, FRIENDLYPROJECTILE, OPPONENTPROJECTILE, OBSTACLE
    }
}
