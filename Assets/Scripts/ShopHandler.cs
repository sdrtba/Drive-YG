using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private GameObject[] prices;
    [SerializeField] private GameObject[] previews;
    [SerializeField] private GameObject[] cars;
    [SerializeField] private Button[] buyBtns;
    [SerializeField] private Button[] pickBtns;
    [SerializeField] private int[] price;
    [SerializeField] private int adCount;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    void Start()
    {
        YandexGame.savesData.isBought[0] = true;
        coinsText.text += " " + YandexGame.savesData.coins;
    }

    public void OpenPreview(int index)
    {
        foreach (GameObject preview in previews)
        {
            preview.SetActive(false);
        }

        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
            pickBtns[i].gameObject.SetActive(false);
            buyBtns[i].gameObject.SetActive(false);
            pickBtns[i].interactable = false;
            buyBtns[i].interactable = false;
        }

        previews[index].SetActive(true);
    }

    public void OpenCar(int index)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
            pickBtns[i].gameObject.SetActive(false);
            buyBtns[i].gameObject.SetActive(false);
            pickBtns[i].interactable = false;
            buyBtns[i].interactable = false;
        }

        cars[index].SetActive(true);

        if (YandexGame.savesData.isBought[index])
        {
            pickBtns[index].gameObject.SetActive(true);
            if (YandexGame.savesData.curSprite != index)
            {
                pickBtns[index].interactable = true;
            }
        }
        else
        {
            buyBtns[index].gameObject.SetActive(true);
            prices[index].SetActive(true);
            if (YandexGame.savesData.coins >= price[index])
            {
                buyBtns[index].interactable = true;
            }
        }
    }

    public void Buy(int index)
    {
        YandexGame.savesData.isBought[index] = true;
        YandexGame.savesData.coins -= price[index];
        YandexGame.savesData.curSprite = index;
        YandexGame.SaveProgress();

        buyBtns[index].gameObject.SetActive(false);
        prices[index].SetActive(false);
        for (int i = 0; i < pickBtns.Length; i++)
        {
            if (YandexGame.savesData.curSprite == i)
            {
                pickBtns[i].interactable = false;
            }
            else
            {
                pickBtns[i].interactable = true;
            }
        }

        coinsText.text = "Coins: " + YandexGame.savesData.coins;
    }
    public void Pick(int index)
    {
        YandexGame.savesData.curSprite = index;
        YandexGame.SaveProgress();

        for (int i = 0; i < pickBtns.Length; i++)
        {
            if (YandexGame.savesData.curSprite == i)
            {
                pickBtns[i].interactable = false;
            }
            else
            {
                pickBtns[i].interactable = true;
            }
        }
    }

    public void ShowRewAd()
    {
        YandexGame.RewVideoShow(0);
    }

    void Rewarded(int id)
    {
        YandexGame.savesData.coins += adCount;
        YandexGame.SaveProgress();

        for (int i = 0; i < buyBtns.Length; i++)
        {
            if (YandexGame.savesData.coins >= price[i])
            {
                buyBtns[i].interactable = true;
            }
            else
            {
                buyBtns[i].interactable = false;
            }
        }

        coinsText.text = "Coins: " + YandexGame.savesData.coins;
    }
}
