using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public int life;
    [HideInInspector] public GameManager manager;
    [HideInInspector] public bool knockedUp;

    public void Attack(GameObject target, int damage)
    {
        GameObject go = Instantiate(manager.attackHitbox, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
        go.GetComponent<Attack>().target = target;
        go.GetComponent<Attack>().damage = damage;
    }

    public float DistanceTo(Vector3 obj)
    {
        Vector3 distance = (obj - transform.position);
        float objectDistance = distance.magnitude;
        return objectDistance;
    }
}
