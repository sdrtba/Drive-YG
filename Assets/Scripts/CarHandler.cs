using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private Animation jumpAnimation;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float force;
    private bool _isJumpEnable = true;
    private bool _isFocus = false;
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
        if (Input.anyKey)
        {
            _isFocus = true;
        }
    }

    private IEnumerator Jump()
    {
        _isJumpEnable = false;
        _rb.AddForce(transform.up * force);
        jumpAnimation.Play();
        yield return new WaitForSeconds(1f);
        _isJumpEnable = true;
    }



    void FixedUpdate()
    {
        _axis = Input.GetAxisRaw("Horizontal") * rotationSpeed;

        if (_axis != 0) _rb.AddTorque(-_axis);
        if (_isFocus) _rb.AddForce(transform.up * speed);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Trap" && _isJumpEnable || collider.tag == "WeakFloor" && _isJumpEnable)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collider.tag == "Finish")
        {
            YandexGame.savesData.maxLevel = YandexGame.savesData.maxLevel + 1;
            YandexGame.SaveProgress();

            YandexGame.FullscreenShow();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
