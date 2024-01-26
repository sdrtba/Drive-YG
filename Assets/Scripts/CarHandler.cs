using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject settingsCanvas;

    [SerializeField] private Animation jumpAnimation;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float force;

    private bool _isOnFloor = true;
    private bool _isFocus = false;
    private bool _isWin = false;
    private Rigidbody2D _rb;
    private float _axis;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        YandexGame.FullscreenShow();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnFloor)
        {
            StartCoroutine(Jump());
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !_isWin)
        {
            TogglePause();
        }
        if (Input.anyKey)
        {
            _isFocus = true;
        }
    }

    private void TogglePause()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        else Time.timeScale = 0;
        settingsCanvas.SetActive(!settingsCanvas.activeSelf);
    }

    private IEnumerator Jump()
    {
        _isOnFloor = false;
        _rb.AddForce(transform.up * force);
        jumpAnimation.Play();
        yield return new WaitForSeconds(1f);
        _isOnFloor = true;
    }

    void FixedUpdate()
    {
        _axis = Input.GetAxisRaw("Horizontal") * rotationSpeed;

        if (_axis != 0) _rb.AddTorque(-_axis);
        if (_isFocus) _rb.AddForce(transform.up * speed);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Trap" || collider.tag == "WeakFloor" && _isOnFloor)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collider.tag == "Finish")
        {
            YandexGame.savesData.maxLevel = YandexGame.savesData.maxLevel + 1;
            YandexGame.SaveProgress();

            Time.timeScale = 0;
            winCanvas.SetActive(true);
        }
    }
}
