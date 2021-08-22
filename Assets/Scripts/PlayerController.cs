using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Human
{
    private Rigidbody rgbd;
    private float enemyDistance;
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        manager = GameManager.instance;
    }

    public void EnableMovement()
    {
        GetComponent<CharacterController>().enabled = true;
        GetComponent<Movement>().enabled = true;
        rgbd.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Attack>() != null)
        {
            Invoke("EnableMovement", 1f);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject enemyCloser = null;
            enemyDistance = 550;

            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                Vector3 temporalUbication = enemy.transform.position;
                float temporalDistance = (temporalUbication - transform.position).magnitude;

                if (temporalDistance < enemyDistance)
                {
                    enemyDistance = temporalDistance;
                    enemyCloser = enemy.gameObject;
                }
            }
            Attack(enemyCloser, 3);
        }
    }
}
