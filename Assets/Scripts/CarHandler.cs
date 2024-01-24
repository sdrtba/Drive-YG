using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

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
        YandexGame.FullscreenShow();
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
        if (collider.tag == "Trap" && _isJumpEnable || collider.tag == "WeakFloor" && _isJumpEnable)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collider.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            YandexGame.savesData.maxLevel = YandexGame.savesData.maxLevel + 1;
            YandexGame.SaveProgress();
        }
    }
}