using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform prefab;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && timer >= 0.3f)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
