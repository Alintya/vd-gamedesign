
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    public LayerMask WhatIsGround;
    public float WalkPointRange = 5, SightingRange = 15, AttackingRange = 5;
    private Vector3 _walkPoint;
    private bool _walkPointSet;
    private NavMeshAgent _agent;
    private Transform _player;
    private bool _playerInSightRange, _playerInAttackRange;

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        base.Update();
        //Check for sight and attack range
        var playerDist = (transform.position - _player.position).magnitude;
        _playerInSightRange = playerDist <= SightingRange;
        _playerInAttackRange = playerDist <= AttackingRange;

        if (!_playerInSightRange && !_playerInAttackRange) Patroling();
        if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
        if (_playerInAttackRange && _playerInSightRange)
        {
            AttackPlayer();
        }
        else
        {
            _fireing = false;
        }
    }

    private void Patroling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
            _agent.SetDestination(_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, WhatIsGround))
            _walkPointSet = true;
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        _agent.SetDestination(transform.position);

        transform.LookAt(_player);

        _fireing = true;
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, attackRange);
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, sightRange);
    // }
}
