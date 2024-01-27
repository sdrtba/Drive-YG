using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSpeed;

    [SerializeField] private GameObject winCanvas;

    [SerializeField] private Animation jumpAnimation;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float force;

    private List<string> idCoinsList = new List<string>();
    private bool _isOnFloor = true;
    private bool _isFocus = false;
    private Rigidbody2D _rb;
    private float _axis;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = sprites[YandexGame.savesData.curSprite];
        YandexGame.FullscreenShow();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnFloor)
        {
            StartCoroutine(Jump());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.anyKey)
        {
            _isFocus = true;
        }
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

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, new Vector3(transform.position.x, transform.position.y, -10), cameraSpeed);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Spike" || collider.tag == "WeakFloor" && _isOnFloor)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collider.tag == "Coin")
        {
            idCoinsList.Add($"{collider.gameObject.name}-{SceneManager.GetActiveScene().buildIndex}");
            Destroy(collider.gameObject);
        }
        else if (collider.tag == "Finish")
        {
            if (SceneManager.GetActiveScene().buildIndex - 1 > YandexGame.savesData.maxLevel) YandexGame.savesData.maxLevel += 1;
            foreach (string idCoin in idCoinsList) YandexGame.savesData.idCoinsList.Add(idCoin);
            YandexGame.savesData.coins += idCoinsList.Count;
            YandexGame.SaveProgress();

            winCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
