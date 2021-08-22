using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Human
{
    public int 
        potions,
        swords;
    public GameObject swordPrefab;
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

    public void ThrowSword(Vector3 spawnPosition)
    {
        Quaternion swordRotation = Quaternion.Euler(90f, transform.eulerAngles.y, transform.eulerAngles.z);
        Instantiate(swordPrefab, spawnPosition, swordRotation);
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
                swords += 3;

            Destroy(other.gameObject);
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
            if (life < 10)
            {
                potions--;
                life += 4;
                life = life > 10 ? 10 : life;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && swords > 0)
        {
            swords--;
            ThrowSword(throwPosition.position);
        }
    }
}
