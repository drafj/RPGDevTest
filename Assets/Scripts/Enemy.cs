using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public float range;
    public List<GameObject> patrolPoints = new List<GameObject>();
    [SerializeField] private int patrolIndex = 0;
    private Vector3 actualPatrolPoint;
    private Gamemanager manager = Gamemanager.instance;
    private NavMeshAgent agent;
    private Vector3 playerPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = Gamemanager.instance;
    }

    private float DistanceTo(Vector3 obj)
    {
        Vector3 distance = (obj - transform.position);
        float objectDistance = distance.magnitude;
        return objectDistance;
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    private void EnemyBehaviour()
    {
        playerPosition = manager.player.transform.position;

        if (DistanceTo(playerPosition) < range)
        {
            Debug.Log("moving towards player");
            MoveToPoint(playerPosition);
        }
        else
        {
            actualPatrolPoint = patrolPoints[patrolIndex].transform.position;
            if (DistanceTo(actualPatrolPoint) > 1.25)
            {
                MoveToPoint(actualPatrolPoint);
            }
            else
            {
                Debug.Log("change patrol point");
                patrolIndex++;
                patrolIndex = patrolIndex == patrolPoints.Count ? 0 : patrolIndex;
            }
        }
    }

    void Update()
    {
        EnemyBehaviour();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 8f);
    }
}
