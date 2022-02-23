using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 360f;
    public float distance = 6f;
    public float zoomSpeed = 2f;

    float currentDistance = 6f;
    float distanceBack = 6f;

    float heightOffset = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // right drag rotates the camera
        if (Input.GetMouseButton(1))
        {
            Vector3 angles = transform.eulerAngles;
            float dx = Input.GetAxis("Mouse Y");
            float dy = Input.GetAxis("Mouse X");

            // look up and down by rotating around x axis
            angles.x = Mathf.Clamp(angles.x + -dx * speed * Time.deltaTime, 0, 70);
            // spin the camera around
            angles.y += dy * speed * Time.deltaTime;

            transform.eulerAngles = angles;
        }

        distanceBack = Mathf.Clamp(distanceBack - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, 2, 10);

        RaycastHit hit;
        if (Physics.Raycast(GetTargetPosition(), -transform.forward, out hit, distance))
        {
            // snap the camera right into where the collision happened
            currentDistance = hit.distance;
        }
        else
        {
            // relax th camera back to the desired distance
            currentDistance = Mathf.MoveTowards(currentDistance, distance + distanceBack, Time.deltaTime);
        }

        // look at the target position
        transform.position = GetTargetPosition() - currentDistance * transform.forward;
    }

    Vector3 GetTargetPosition()
    {
        return target.position + heightOffset * Vector3.up;
    }
}