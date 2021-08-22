using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public float range;
    public List<GameObject> patrolPoints = new List<GameObject>();
    private GameObject actualPatrolPoint;
    private Gamemanager manager = Gamemanager.instance;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = Gamemanager.instance;
    }

    private float DistanceTo(Vector3 obj)
    {
        Vector3 distance = (obj - transform.position);
        float objectDistance = distance.magnitude;
        Debug.Log("player distance: " + objectDistance);
        return objectDistance;
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    private void EnemyBehaviour()
    {
        if (DistanceTo(manager.player.transform.position) < range)
        {
            Debug.Log("moving towards player");
            MoveToPoint(manager.player.transform.position);
        }
        else
        {
            //patrol
        }
    }

    void Update()
    {
        EnemyBehaviour();
    }
}
