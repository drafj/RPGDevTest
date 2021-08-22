using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Human
{
    public int 
        potions,
        knifes;
    public GameObject knifePrefab;
    public Transform throwPosition;
    private Rigidbody rgbd;
    private float enemyDistance;
    private CharacterController controller;
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        manager = GameManager.instance;
    }

    public void ThrowKnife(Vector3 spawnPosition)
    {
        Instantiate(knifePrefab, spawnPosition, transform.rotation);
    }

    public void EnableMovement()
    {
        rgbd.isKinematic = true;
        controller.enabled = true;
        GetComponent<Movement>().enabled = true;
        knockedUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Attack>() != null)
        {
            if (other.gameObject.GetComponent<Attack>().target == gameObject)
            {
                knockedUp = true;
                Invoke("EnableMovement", 1f);
            }
        }

        if (other.gameObject.GetComponent<InteractableItem>() != null)
        {
            InteractableItem item = other.gameObject.GetComponent<InteractableItem>();
            if (item.type == ObjectType.Potion)
                potions++;
            else
                knifes += 3;
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
        if (Input.GetKeyDown(KeyCode.Q) && potions > 0)
        {
            potions--;
            life += 4;
        }
        if (Input.GetKeyDown(KeyCode.E) && knifes > 0)
        {
            knifes--;
            ThrowKnife(throwPosition.position);
        }
    }
}
