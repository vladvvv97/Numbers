using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    public event Action OnCurrencyChange;

    [Header("Set In Inspector")]
    [SerializeField] private int clearRowsCost;
    [SerializeField] private int coinsBaseAddedValue;
    [SerializeField] private int diamondsBaseAddedValue;
    [SerializeField] private int minCoinsAddedValue;
    [SerializeField] private int minDiamondsAddedValue;
    [SerializeField] private int rewardMultiplier;
    [SerializeField] private int scoreGap;                   // Интервал счета, после которого награда будет увеличиваться 

    [SerializeField] private GameObject notEnoughCurrencyAlert;
    [SerializeField] private AnimationClip NotEnoughCurrencyAlertAnimationClip;

    [SerializeField] private GameObject coinsChangeEffect;
    [SerializeField] private TextMeshProUGUI coinsChangeEffectTMP;
    [SerializeField] private AnimationClip coinsChangeEffectAnimationClip;

    [SerializeField] private GameObject diamondsChangeEffect;
    [SerializeField] private TextMeshProUGUI diamondsChangeEffectTMP;
    [SerializeField] private AnimationClip diamondsChangeEffectAnimationClip;

    private int _coinsToAdd;
    private int _diamondsToAdd;

    public int CoinsToAdd { get => _coinsToAdd; private set => _coinsToAdd = value; }
    public int DiamondsToAdd { get => _diamondsToAdd; private set => _diamondsToAdd = value; }
    public int ClearRowsCost { get => clearRowsCost; private set => clearRowsCost = value; }
    public int RewardMultiplier { get => rewardMultiplier; private set => rewardMultiplier = value; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 0);
        }

        if (!PlayerPrefs.HasKey("Diamonds"))
        {
            PlayerPrefs.SetInt("Diamonds", 0);
        }

    }

    public void AddCoins(int value)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + value);
        CurrencyManager.Instance.OnCurrencyChange?.Invoke();
        CurrencyManager.Instance.StartCoroutine(ActivateCoinsChangeEffect(true, value));
    }

    public void AddDiamonds(int value)
    {
        PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + value);
        CurrencyManager.Instance.OnCurrencyChange?.Invoke();
        CurrencyManager.Instance.StartCoroutine(ActivateDiamondsChangeEffect(true, value));
    }
    public void ResetRewardToAdd()
    {
        CoinsToAdd = 0;
        DiamondsToAdd = 0;
    }
    public void AmountRewardCalculation(int rewardMult = 1)
    {
        for (int i = 1; i <= 15; i++)
        {
            if (GameManager.Instance.Score >= scoreGap * i)
            {
                CoinsToAdd += rewardMult * coinsBaseAddedValue;
                DiamondsToAdd += rewardMult * diamondsBaseAddedValue;
            }
            else if (GameManager.Instance.Score < scoreGap)
            {
                CoinsToAdd = rewardMult * minCoinsAddedValue;
                DiamondsToAdd = rewardMult * minDiamondsAddedValue;
                return;
            }
        }
    }
    public void AddRewardAfterGame()
    {
        AmountRewardCalculation();

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CoinsToAdd);
        PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + DiamondsToAdd);
        PlayerPrefs.Save();

        CurrencyManager.Instance.StartCoroutine(ActivateCoinsChangeEffect(true, CoinsToAdd));
        CurrencyManager.Instance.StartCoroutine(ActivateDiamondsChangeEffect(true, DiamondsToAdd));

        ResetRewardToAdd();

        CurrencyManager.Instance.OnCurrencyChange?.Invoke();
    }

    public void AddMultipliedRewardAfterGame()
    {
        AmountRewardCalculation(RewardMultiplier);

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CoinsToAdd);
        PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + DiamondsToAdd);
        PlayerPrefs.Save();

        CurrencyManager.Instance.StartCoroutine(ActivateCoinsChangeEffect(true, CoinsToAdd));
        CurrencyManager.Instance.StartCoroutine(ActivateDiamondsChangeEffect(true, DiamondsToAdd));

        ResetRewardToAdd();

        CurrencyManager.Instance.OnCurrencyChange?.Invoke();
    }

    public void BuyWithCoins(int value)
    {
        if (PlayerPrefs.GetInt("Coins") - value >= 0)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - value);
            CurrencyManager.Instance.StartCoroutine(ActivateCoinsChangeEffect(false, value));
            CurrencyManager.Instance.OnCurrencyChange?.Invoke();
        }
        else
        {
            StartCoroutine(ActivateNotEnoughCurrencyScreen());
        }
        
    }
    public void BuyWithDiamonds(int value)
    {
        if (PlayerPrefs.GetInt("Diamonds") - value >= 0)
        {
            PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") - value);
            CurrencyManager.Instance.StartCoroutine(ActivateDiamondsChangeEffect(false, value));
            CurrencyManager.Instance.OnCurrencyChange?.Invoke();
        }
        else
        {
            StartCoroutine(ActivateNotEnoughCurrencyScreen());
        }        
    }

    public bool CanBuyWithCoins(int value)
    {
        if (PlayerPrefs.GetInt("Coins") - value >= 0)
        {
            return true;
        }
        else
        {
            StartCoroutine(ActivateNotEnoughCurrencyScreen());
            return false;
        }

    }
    public bool CanBuyWithDiamonds(int value)
    {
        if (PlayerPrefs.GetInt("Diamonds") - value >= 0)
        {
            return true;
        }
        else
        {
            StartCoroutine(ActivateNotEnoughCurrencyScreen());
            return false;
        }
    }
    IEnumerator ActivateNotEnoughCurrencyScreen()
    {
        notEnoughCurrencyAlert.gameObject.SetActive(true);

        AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Alert);

        yield return new WaitForSeconds(NotEnoughCurrencyAlertAnimationClip.length);

        notEnoughCurrencyAlert.gameObject.SetActive(false);

        yield return null;
    }
    IEnumerator ActivateCoinsChangeEffect(bool isPositive, int value)
    {
        if (isPositive)
        {
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Add);
            CurrencyManager.Instance.coinsChangeEffectTMP.text = "+" + value.ToString();
            CurrencyManager.Instance.coinsChangeEffectTMP.color = Color.green;
        }
        else
        {
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Spend);
            CurrencyManager.Instance.coinsChangeEffectTMP.text = "-" + value;
            CurrencyManager.Instance.coinsChangeEffectTMP.color = Color.red;
        }

        CurrencyManager.Instance.coinsChangeEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(CurrencyManager.Instance.coinsChangeEffectAnimationClip.length);
        CurrencyManager.Instance.coinsChangeEffect.gameObject.SetActive(false);

        yield return null;
    }
    IEnumerator ActivateDiamondsChangeEffect(bool isPositive, int value)
    {
        if (isPositive)
        {
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Add);
            CurrencyManager.Instance.diamondsChangeEffectTMP.text = "+" + value.ToString();
            CurrencyManager.Instance.diamondsChangeEffectTMP.color = Color.green;
        }
        else
        {
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Spend);
            CurrencyManager.Instance.diamondsChangeEffectTMP.text = "-" + value;
            CurrencyManager.Instance.diamondsChangeEffectTMP.color = Color.red;
        }

        CurrencyManager.Instance.diamondsChangeEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(CurrencyManager.Instance.diamondsChangeEffectAnimationClip.length);
        CurrencyManager.Instance.diamondsChangeEffect.gameObject.SetActive(false);

        yield return null;
    }
}
