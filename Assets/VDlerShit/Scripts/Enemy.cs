using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    //public int enemyDamage;
    public NavMeshAgent NavMeshAgent;
    public Transform PlayerTransform;
    public LayerMask GroundLayerMask, PlayerLayerMask;
    //public PlayerAttack PlayerAttackScript;
    public float MinDistanceSqr;

    // TODO: determine ranged threshold
    public bool IsRanged;
    public float meleeAttackRange;
    public int damage;
    //public GameObject attackVFXObject;
    //private GameObject instantiatedAttackVFX;

    // patrolling
    public Vector3 WalkPoint;
    public float WalkRadius;
    
    private bool walkPointSet;
    
    // attacking
    public float TimeBetweenAttacks;
    
    private bool alreadyAttacked = false;

    // states
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    public GameObject projectile;
    public GameObject player;

    public void Awake()
    {
        PlayerTransform = GameObject.Find("Player Character").transform;
        NavMeshAgent = GetComponent<NavMeshAgent>();
        damage = this.gameObject.GetComponent<Damage>().damageAmount;
        
    }
   

    private void Update()
    {
        //check sight and attack range
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, PlayerLayerMask);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayerMask);
       

        if (!PlayerInSightRange && !PlayerInAttackRange) Patrolling();
        if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
        if (PlayerInSightRange && PlayerInAttackRange) AttackPlayer();


        //Debug.Log(enemyHealth);
    }
    private void Patrolling()
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            NavMeshAgent.SetDestination(WalkPoint);

        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-WalkRadius, WalkRadius);
        float randomX = Random.Range(-WalkRadius, WalkRadius);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(WalkPoint, -transform.up, 2f, GroundLayerMask)) 
            walkPointSet = true;
    }
    
    private void ChasePlayer()
    {
        var destPosition = PlayerTransform.position;
        var sqrDistance = (transform.position - destPosition).sqrMagnitude;
        NavMeshAgent.SetDestination(destPosition);
        NavMeshAgent.isStopped = sqrDistance <= MinDistanceSqr;
    }
    
    private void AttackPlayer()
    { 
        NavMeshAgent.SetDestination(transform.position);    
        transform.LookAt(PlayerTransform);
 
        Debug.Log(this.gameObject.name + " is attacking!");
        //Debug.Log(playerAttackScript.playerHealth);

        if (!alreadyAttacked)
        {
            //Debug.Log("Enemy hit for " + enemyDamage);
            //instantiatedAttackVFX = Instantiate(attackVFXObject, transform.position, transform.rotation * Quaternion.Euler (0f, 180f, 0f));
            //Destroy(instantiatedAttackVFX, 1);
            if(IsRanged)
            {
                Vector3 offset = new Vector3(0, 2, 0);
                Rigidbody rb = Instantiate(projectile, transform.position + offset, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
                rb.AddForce(transform.up * 1f, ForceMode.Impulse);

            }else if(!IsRanged)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, meleeAttackRange, PlayerLayerMask);
                foreach (var hitCollider in hitColliders)
                {
                    hitCollider.GetComponent<Health>().TakeDamage(damage);
                }
                //other.gameObject.GetComponent<Health>().TakeDamage(playerDamage);
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), TimeBetweenAttacks);
            }
            

            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    
}
