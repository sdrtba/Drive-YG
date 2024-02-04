using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Button[] buyBtns;
    [SerializeField] private Button[] pickBtns;
    [SerializeField] private int priceCount;
    [SerializeField] private int adCount;

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
                pickBtns[i].gameObject.SetActive(true);
                if (i == YandexGame.savesData.curSprite) pickBtns[i].interactable = false;
                else pickBtns[i].interactable = true;
            }
            else
            {
                buyBtns[i].gameObject.SetActive(true);
                if (i * priceCount <= YandexGame.savesData.coins) buyBtns[i].interactable = true;
            }
        }
    }

    public void Buy(int index)
    {
        buyBtns[index].gameObject.SetActive(false);

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
        UpdateUI();
    }

    public void ShowRewAd()
    {
        YandexGame.RewVideoShow(0);
    }

    void Rewarded(int id)
    {
        YandexGame.savesData.coins += adCount;
        UpdateUI();
    }
}
