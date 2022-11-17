using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

[SerializeField] Transform target;
[SerializeField] Vector3 offset;

[SerializeField] float smoothSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion rot = target.rotation;
        Vector3 computedOffset = new Vector3(offset.x, offset.y, offset.z );
        
        Vector3 desiredPosition = target.position + computedOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
