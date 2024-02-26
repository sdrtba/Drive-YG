using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private GameObject jumpEffect;
    [SerializeField] private GameObject boomEffect;
    [SerializeField] private GameObject winCanvas;

    [SerializeField] private Animation jumpAnimation;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float force;

    private float jumpTime = 1f;
    private List<string> coinsListId = new List<string>();
    private bool _isOnFloor = true;
    private bool _isFocus = false;
    private Rigidbody2D _rb;
    private float _axis;


    [Space]
    [SerializeField] private Material material;
    private Vector3 defOffset = new Vector3(-0.1f, -0.1f, 0);
    private Vector3 offset;
    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprRndShadow;
    private Transform transCaster;
    private Transform transShadow;
    private Vector2 _scale;
    private float _time = 0;
    private float _offsetCoef = 0.002f;
    private float _scaleCoef = 0.002f;
    private void Shadow()
    {
        offset = defOffset;
        _scale = transform.localScale;

        transCaster = transform;
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "Shadow";
        transShadow.localRotation = Quaternion.identity;

        sprRndCaster = GetComponent<SpriteRenderer>();
        sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();
        sprRndShadow.sprite = sprRndCaster.sprite;
        sprRndShadow.sortingOrder = 2;
        sprRndShadow.material = material;
        sprRndShadow.color = new Color(0, 0, 0, 0.5f);
    }

    void LateUpdate()
    {
        if (!_isOnFloor)
        {
            _time += Time.deltaTime;
            if (_time <= jumpTime / 2)
            {
                offset -= new Vector3(_offsetCoef, _offsetCoef);
                sprRndShadow.transform.localScale -= new Vector3(_scaleCoef, _scaleCoef);
            }
            else
            {
                offset += new Vector3(_offsetCoef, _offsetCoef);
                sprRndShadow.transform.localScale += new Vector3(_scaleCoef, _scaleCoef);

            }
        }
        else
        {
            _time = 0;
            offset = defOffset;
            sprRndShadow.transform.localScale = _scale;
        }
        transShadow.position = transform.position + offset;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = sprites[YandexGame.savesData.curSprite];
        YandexGame.FullscreenShow();
        _rb.centerOfMass = new Vector2(0, 0.4f);

        Shadow();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnFloor) StartCoroutine(Jump());
        if (Input.GetKeyDown(KeyCode.Tab)) SceneManager.LoadScene(0);
        if (Input.anyKey) _isFocus = true;
    }

    private IEnumerator Jump()
    {
        _isOnFloor = false;
        _rb.AddForce(transform.up * force);
        jumpAnimation.Play();
        Instantiate(jumpEffect, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(jumpTime);
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
        if (collider.tag == "Saw" || collider.tag == "Spikes" && _isOnFloor)
        {
            gameObject.SetActive(false);
            Instantiate(boomEffect, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collider.tag == "Coin")
        {
            coinsListId.Add($"{collider.gameObject.name}-{SceneManager.GetActiveScene().buildIndex}");
            Destroy(collider.gameObject);
        }
        else if (collider.tag == "Finish")
        {
            if (SceneManager.GetActiveScene().buildIndex - 1 > YandexGame.savesData.maxLevel && YandexGame.savesData.maxLevel <= 24) YandexGame.savesData.maxLevel += 1;
            foreach (string coinId in coinsListId) YandexGame.savesData.idCoinsList.Add(coinId);
            YandexGame.savesData.coins += coinsListId.Count;
            YandexGame.SaveProgress();

            winCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
