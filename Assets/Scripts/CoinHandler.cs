using UnityEngine.SceneManagement;
using UnityEngine;
using YG;

public class CoinHandler : MonoBehaviour
{
    [SerializeField] private GameObject particle;

    void Start()
    {
        if (YandexGame.savesData.idCoinsList.Contains($"{gameObject.name}-{SceneManager.GetActiveScene().buildIndex}")) Destroy(gameObject);
    }

    void OnTriggerEnter2D()
    {
        Instantiate(particle, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
