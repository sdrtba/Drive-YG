using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform target;

    void Start()
    {
        target = GameObject.Find("Car/CarBody").transform;
    }

    void FixedUpdate()
    {
        Vector3 _targetVector = new Vector3(target.position.x, target.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, _targetVector, speed);
    }
}
