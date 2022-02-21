using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float speed = 10f;
    public float jumpVelocity = 10f;
    public Vector3 velocity;
    public Transform cam = null;

    CharacterController cc;
    Vector2 moveInput = new Vector2();
    bool jumpInput = false;
    bool isGrounded = true;

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        jumpInput = Input.GetButton("Jump");
    }

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // find the horizontal unit vector facing forward from the camera
        Vector3 camForward = cam.forward;
        camForward.y = 0; // Vector3.ProjectOnPlane(camForward, Vector3.up);
        camForward.Normalize();

        // use cameras right vector, which is always horizontal
        Vector3 camRight = cam.right;

        // Player movement using WASD or arrow keys
        Vector3 delta = (moveInput.x * camRight + moveInput.y * camForward) * speed;
        if (isGrounded || moveInput.x != 0 || moveInput.y != 0)
        {
            velocity.x = delta.x;
            velocity.z = delta.z;
        }
        // check for jumping
        if (jumpInput && isGrounded)
            velocity.y = jumpVelocity;

        // apply gravity
        velocity += Physics.gravity * Time.fixedDeltaTime;

        if (isGrounded && velocity.y < 0)
            velocity.y = 0;

        // apply this to our positional update this frame
        delta += velocity * Time.fixedDeltaTime;
        
        cc.Move(delta);
        isGrounded = cc.isGrounded;
    }
}