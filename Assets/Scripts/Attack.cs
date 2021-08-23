using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Attack : MonoBehaviour
{
    public int damage;
    public GameObject target;
    private Rigidbody rg;
    private bool didHit;

    private void Start()
    {
        rg = GetComponent<Rigidbody>();
        //rg.velocity = transform.forward * 480f * Time.deltaTime;

        StartCoroutine(AutoDestruction());
    }

    IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(0.8f);
        if (!didHit)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            if (target.GetComponent<Human>().life > 0)
            {
                didHit = true;
                if (target.GetComponent<Enemy>() != null)
                {
                    target.GetComponent<NavMeshAgent>().isStopped = true;
                }
                else if (target.GetComponent<PlayerController>() != null)
                {
                    target.GetComponent<CharacterController>().enabled = false;
                    target.GetComponent<Movement>().enabled = false;
                }
                target.GetComponent<Human>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
