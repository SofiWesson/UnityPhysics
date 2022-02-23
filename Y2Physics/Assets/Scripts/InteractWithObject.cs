using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    Raycaster ray = null;
    GameObject objHit;

    private Vector3 mOffset;
    private float mZCoord;

    // Start is called before the first frame update
    void Start()
    {
        ray = GetComponent<Raycaster>();
        objHit  = ray.GetHit().transform.gameObject;
    }
    
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(objHit.transform.position).z;

        // Store offset
        mOffset = objHit.transform.position - GetMouseWorldPos();
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
            objHit.transform.position = GetMouseWorldPos() + mOffset;
        }
    }
}