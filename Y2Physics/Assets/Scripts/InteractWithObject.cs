using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    Raycaster ray = null;
    GameObject objHit;
    Rigidbody rb;

    private Vector3 mOffset;
    private float mZCoord;

    // Start is called before the first frame update
    void Start()
    {
        ray = GetComponent<Raycaster>();
    }

    private Vector3 GetMouseWorldPos()
    {
        // pixel coordinates
        Vector3 mousePoint = Input.mousePosition;
        
        // z coordinate of the game object on screen
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objHit = ray.GetObjectHit();

            if (objHit.GetComponent<Rigidbody>() != null)
            {
                rb = objHit.GetComponent<Rigidbody>();

                rb.isKinematic = true;

                mZCoord = Camera.main.WorldToScreenPoint(objHit.transform.position).z;

                // Store offset
                mOffset = objHit.transform.position - GetMouseWorldPos();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            objHit = ray.GetObjectHit();
            objHit.SetActive(false);
            Destroy(objHit);
            objHit = null;
        }

        if (rb != null)
        {
            if (Input.GetMouseButton(0))
            {
                objHit.transform.position = GetMouseWorldPos() + mOffset;
            }

            if (Input.GetMouseButtonUp(0))
            {
                rb.isKinematic = false;
            }
        }
    }
}