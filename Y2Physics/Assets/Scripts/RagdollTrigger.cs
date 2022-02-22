using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ragdoll ragdoll = other.GetComponentInParent<Ragdoll>();
        if (ragdoll != null)
            ragdoll.RagdollOn = true;
    }
}