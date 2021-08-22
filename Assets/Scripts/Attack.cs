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
        rg.velocity = transform.forward * 580f * Time.deltaTime;

        StartCoroutine(AutoDestruction());
    }

    IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(0.4f);
        if (!didHit)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            didHit = true;
            if (target.GetComponent<Enemy>() != null)
            {
                target.GetComponent<NavMeshAgent>().enabled = false;
            }
            else if (target.GetComponent<PlayerController>() != null)
            {
                target.GetComponent<CharacterController>().enabled = false;
                target.GetComponent<Movement>().enabled = false;
            }
            Vector3 direction = (transform.position - target.transform.position) * -1;
            direction = direction + new Vector3(0, 2.2f, 0);
            Rigidbody rgbd = target.GetComponent<Rigidbody>();
            rgbd.isKinematic = false;
            rgbd.velocity = Vector3.zero;
            rgbd.AddForce(direction * 150);
            Destroy(gameObject);
        }
    }
}
