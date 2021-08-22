using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Human
{
    public float 
        sightRange,
        attackRange;
    public List<GameObject> patrolPoints = new List<GameObject>();
    public NavMeshAgent agent;
    [SerializeField] private int patrolIndex = 0;
    private Vector3 actualPatrolPoint;
    private Vector3 playerPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = GameManager.instance;

        StartCoroutine(EnemyBehaviour());
    }

    public void MoveToPoint(Vector3 point)
    {
        if (agent.enabled)
        agent.SetDestination(point);
    }

    private IEnumerator EnemyBehaviour()
    {
        while (true)
        {
            playerPosition = manager.player.transform.position;

            if (DistanceTo(playerPosition) <= sightRange && agent.isOnNavMesh)
            {
                if (DistanceTo(playerPosition) <= attackRange && agent.isOnNavMesh)
                {
                    agent.isStopped = true;
                    Vector3 targetPlayer = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
                    transform.LookAt(targetPlayer);
                    Attack(manager.player, 1);
                    yield return new WaitForSeconds(2.5f);
                }
                else
                {
                    yield return agent.isOnNavMesh && !knockedUp;
                    agent.isStopped = false;
                    MoveToPoint(playerPosition);
                }
            }
            else
            {
                if (patrolPoints.Count <= 0)
                    yield return null;

                actualPatrolPoint = patrolPoints[patrolIndex].transform.position;
                if (DistanceTo(actualPatrolPoint) > 1.25 && agent.isOnNavMesh)
                {
                    agent.isStopped = false;
                    agent.stoppingDistance = 0.5f;
                    MoveToPoint(actualPatrolPoint);
                }
                else
                {
                    patrolIndex++;
                    patrolIndex = patrolIndex == patrolPoints.Count ? 0 : patrolIndex;
                }
            }
            yield return null;
        }
    }

    public void EnebleNormalBehaviour()
    {
        knockedUp = false;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Attack>() != null)
        {
            knockedUp = true;
            Invoke("EnebleNormalBehaviour", 2f);
        }
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            inGround = false;
            GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log("saliendo del piso");
        }
    }*/

    /*void Update()
    {
        if (knockedUp)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 8f);
    }
}
