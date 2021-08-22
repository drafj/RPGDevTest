using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    public int life;
    public Animator anim;
    [HideInInspector] public GameManager manager;
    [HideInInspector] public bool knockedUp;

    public void Attack(GameObject target, int damage, Transform hand)
    {
        //GameObject go = Instantiate(manager.attackHitbox, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
        anim.SetBool("Running", false);
        anim.SetTrigger("Attacking");
        GameObject go = Instantiate(manager.attackHitbox, hand);
        go.GetComponent<Attack>().target = target;
        go.GetComponent<Attack>().damage = damage;
    }

    public float DistanceTo(Vector3 obj)
    {
        Vector3 distance = (obj - transform.position);
        float objectDistance = distance.magnitude;
        return objectDistance;
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        anim.SetBool("Running", false);
        if (life <= 0)
        {
            anim.SetTrigger("Death");
            if (GetComponent<PlayerController>() != null)
            {
                GetComponent<Movement>().enabled = false;
                GetComponent<CharacterController>().enabled = false;
            }
            if (GetComponent<Enemy>() != null)
            {
                GetComponent<Enemy>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }
        else
        {
            anim.SetTrigger("TakeDamage");
            /*if (GetComponent<PlayerController>() != null)
            {
                GetComponent<Movement>().enabled = false;
                yield return new WaitForSeconds(1f);
                GetComponent<Movement>().enabled = true;
            }*/
        }
    }
}
