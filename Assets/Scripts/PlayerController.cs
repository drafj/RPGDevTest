using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Human
{
    public int 
        potions,
        swords;
    public GameObject swordPrefab;
    public Transform hand;
    private bool attackColdown;
    private float enemyDistance;
    [SerializeField] private int isMovementDisabled;
    private CharacterController controller;
    private Movement movement;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = GetComponent<Movement>();
        manager = GameManager.instance;
    }

    public IEnumerator ThrowSword(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(0.5f);
        hand.gameObject.SetActive(false);
        Quaternion swordRotation = Quaternion.Euler(90f, transform.eulerAngles.y, transform.eulerAngles.z);
        Instantiate(swordPrefab, spawnPosition, swordRotation);
        yield return new WaitForSeconds(0.3f);
        hand.gameObject.SetActive(true);
    }

    public IEnumerator EnableMovement(bool justMove, float delay)
    {
        isMovementDisabled++;
        yield return new WaitForSeconds(delay);
        if (isMovementDisabled == 1)
        {
            if (life > 0)
            {
                movement.enabled = true;
                attackColdown = false;
                if (justMove)
                    yield return null;
                controller.enabled = true;
            }
        }
        isMovementDisabled--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Attack>() != null)
        {
            if (other.gameObject.GetComponent<Attack>().target == gameObject)
            {
                StartCoroutine(EnableMovement(false, 1f));
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
        if (life > 0)
        {
            if (Input.GetMouseButtonDown(0) && !attackColdown && movement.enabled)
            {
                attackColdown = true;
                movement.enabled = false;
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
                StartCoroutine(EnableMovement(true, 1f));
                Attack(enemyCloser, 3, hand);
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
            if (Input.GetKeyDown(KeyCode.E) && swords > 0 && !attackColdown && !movement.enabled)
            {
                attackColdown = true;
                movement.enabled = false;
                anim.SetBool("Running", false);
                anim.SetTrigger("Attacking");
                swords--;
                StartCoroutine(EnableMovement(true, 1f));
                StartCoroutine(ThrowSword(hand.position));
            }
        }
    }
}
