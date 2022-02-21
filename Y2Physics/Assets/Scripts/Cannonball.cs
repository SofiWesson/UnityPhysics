using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cannonball : MonoBehaviour
{
    public float forceOfFire = 400;

    private bool canFire = true;

    Rigidbody rb = null;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && canFire)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * forceOfFire);
            canFire = false;
        }
    }
}
