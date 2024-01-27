using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private GameObject[] buyBtns;
    [SerializeField] private GameObject[] pickBtns;
    [SerializeField] private int priceCount = 10;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    void Start()
    {
        YandexGame.savesData.isBought[0] = true;
        UpdateUI();
    }

    void UpdateUI()
    {
        coinsText.text = "Coins: " + YandexGame.savesData.coins;


        for (int i = 0; i < buyBtns.Length; i++)
        {
            if (YandexGame.savesData.isBought[i])
            {
                pickBtns[i].SetActive(true);
            }
            else
            {
                buyBtns[i].SetActive(true);
                if (Int32.Parse(buyBtns[i].name) * priceCount <= YandexGame.savesData.coins) buyBtns[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Buy(int index)
    {
        YandexGame.savesData.isBought[index] = true;
        YandexGame.savesData.coins -= index * priceCount;
        YandexGame.savesData.curSprite = index;
        YandexGame.SaveProgress();

        UpdateUI();

    }
    public void Pick(int index)
    {
        YandexGame.savesData.curSprite = index;
        YandexGame.SaveProgress();
    }

    public void ShowRewAd()
    {
        YandexGame.RewVideoShow(0);
    }

    void Rewarded(int id)
    {
        YandexGame.savesData.coins += 5;
        UpdateUI();
    }
}
