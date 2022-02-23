using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float speed = 10f;
    public float jumpVelocity = 10f;
    public Vector3 velocity;
    public Transform cam = null;

    Animator animator = null;

    CharacterController cc;
    Vector2 moveInput = new Vector2();
    bool jumpInput = false;
    bool crouchInput = false;
    bool isGrounded = true;

    public Vector3 hitDirection;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        velocity = new Vector3(0, 0, 0);
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        jumpInput = Input.GetButton("Jump");
        crouchInput = Input.GetKey(KeyCode.LeftControl);

        animator.SetFloat("Forwards", moveInput.y);
        animator.SetBool("Jump", !isGrounded);
        animator.SetBool("Crouch", crouchInput);
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
        Vector3 delta = (moveInput.x * camRight + moveInput.y * camForward) * speed * Time.fixedDeltaTime;
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

        if (isGrounded)
            hitDirection = Vector3.zero;

        // slide objects off surface they're hanging on to
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            Vector3 horizontalHitDirection = hitDirection;

            horizontalHitDirection.y = 0;
            float displacement = horizontalHitDirection.magnitude;
            if (displacement > 0)
                velocity -= 0.2f * horizontalHitDirection / displacement;
        }

        // check if we've hit ground from falling. If so, remove our velocity
        if (isGrounded && velocity.y < 0)
            velocity.y = 0;

        // apply this to our positional update this frame
        delta += velocity * Time.fixedDeltaTime;

        transform.forward = camForward;

        cc.Move(delta);
        isGrounded = cc.isGrounded;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitDirection = hit.point - transform.position;
    }
}