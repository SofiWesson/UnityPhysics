using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsObject : MonoBehaviour
{
    public Material awakeMat = null;
    public Material sleepMat = null;

    private Rigidbody rb = null;
    private bool wasSleeping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.IsSleeping() && !wasSleeping && sleepMat != null)
        {
            wasSleeping = true;
            GetComponent<MeshRenderer>().material = sleepMat;
        }
        if (!rb.IsSleeping() && wasSleeping && awakeMat != null)
        {
            wasSleeping = false;
            GetComponent<MeshRenderer>().material = awakeMat;
        }
    }
}
