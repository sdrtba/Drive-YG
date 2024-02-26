using UnityEngine.SceneManagement;
using UnityEngine;
using YG;

public class CoinHandler : MonoBehaviour
{

    void Start()
    {
        if (YandexGame.savesData.idCoinsList.Contains($"{gameObject.name}-{SceneManager.GetActiveScene().buildIndex}")) Destroy(gameObject);
    }
}
