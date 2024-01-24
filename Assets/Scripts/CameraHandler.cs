using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    void FixedUpdate()
    {
        Vector3 _target = new Vector3(target.position.x, target.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, _target, speed);
    }
}
