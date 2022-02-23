using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycaster : MonoBehaviour
{
    public Text output;
    Ray ray;
    RaycastHit hit;

    public RaycastHit GetHit() { return hit; }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 500) == true)
        {
            output.text = hit.transform.gameObject.name;
        }
    }
}
