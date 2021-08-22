using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowableWeapon : MonoBehaviour
{
    private Rigidbody rgbd;

    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        rgbd.velocity = (transform.up * 10f) + new Vector3(0f, 3f, 0f);
        Destroy(gameObject, 3f);
    }
}
