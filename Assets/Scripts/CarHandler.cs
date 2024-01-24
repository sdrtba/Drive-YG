using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private Animation jumpAnimation;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;
    private bool _isJumpEnable = true;
    private Rigidbody2D _rb;
    private float _axis;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isJumpEnable)
        {
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        _isJumpEnable = false;
        _rb.AddForce(transform.up * 100);
        jumpAnimation.Play();
        yield return new WaitForSeconds(1f);
        _isJumpEnable = true;
    }

    void FixedUpdate()
    {
        _axis = Input.GetAxisRaw("Horizontal") * rotationSpeed;

        if (_axis != 0) _rb.AddTorque(-_axis);
        _rb.AddForce(transform.up * speed);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Trap" || collider.tag == "WeakFloor" && _isJumpEnable)
        {
            SceneManager.LoadScene(0);
        }
        else if (collider.tag == "Finish")
        {
            SceneManager.LoadScene(0);
        }
    }
}
