using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target; 
    public float zoom = 10;

    // Update is called once per frame
    void Update()
    {
        float targetX = target.position.x;
        float targetY = target.position.y;

        transform.position = new Vector3(targetX, targetY, -zoom);
    
    }
}
