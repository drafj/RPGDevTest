using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    public int life;
    public Animator anim;
    [HideInInspector] public GameManager manager;

    public void Attack(GameObject target, int damage, Transform hand)
    {
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
        if (GetComponent<PlayerController>() != null)
            manager.PlayerHealthBar.SetHealth(life);
        else
            GetComponent<Enemy>().knocked = true;
        if (life <= 0)
        {
            anim.SetTrigger("Death");
            if (GetComponent<PlayerController>() != null)
            {
                GetComponent<Movement>().enabled = false;
                GetComponent<CharacterController>().enabled = false;
                manager.uiController.End("you lose");
            }
            if (GetComponent<Enemy>() != null)
            {
                GetComponent<Collider>().enabled = false;
                GetComponent<Enemy>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                manager.player.GetComponent<PlayerController>().remainingEnemies--;
                if (manager.player.GetComponent<PlayerController>().remainingEnemies == 0)
                    manager.uiController.End("you win");
            }
        }
        else
        {
            anim.SetTrigger("TakeDamage");
        }
    }
}
