using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private CharacterController controller = null;
    private Animator animator = null;

    public float speed = 100f;
    public float pushPower = 2f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //controller.SimpleMove(transform.forward * vertical * speed * Time.fixedDeltaTime);
        controller.SimpleMove(transform.up * Time.fixedDeltaTime);
        transform.Rotate(transform.up, horizontal * speed * Time.fixedDeltaTime);
        animator.SetFloat("Speed", vertical * speed * Time.fixedDeltaTime);
        //animator.SetFloat("Forwards", vertical * speed * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null || body.isKinematic)
            return;
        if (hit.moveDirection.y < -0.3f)
            return;
        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDirection * pushPower;
    }
}
