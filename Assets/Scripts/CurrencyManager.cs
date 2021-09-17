using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private int rewardMultiplier;
    [SerializeField] private int scoreGap;                   // Интервал счета, после которого награда будет увеличиваться 
                                                             
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
    }

    public void AddDiamonds(int value)
    {
        PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + value);
        CurrencyManager.Instance.OnCurrencyChange?.Invoke();
    }
    public void ResetRewardToAdd()
    {
        CoinsToAdd = 0;
        DiamondsToAdd = 0;
    }
    public void AmountRewardCalculation(int rewardMult = 1)
    {
        for (int i = 0; i <= 10; i++)
        {
            if (GameManager.Instance.Score >= scoreGap * i)
            {
                CoinsToAdd += rewardMult * coinsBaseAddedValue;
                DiamondsToAdd += rewardMult * diamondsBaseAddedValue;
            }
        }
    }
    public void AddRewardAfterGame()
    {
        AmountRewardCalculation();

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CoinsToAdd);
        PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + DiamondsToAdd);
        PlayerPrefs.Save();

        ResetRewardToAdd();

        CurrencyManager.Instance.OnCurrencyChange?.Invoke();
    }

    public void AddMultipliedRewardAfterGame()
    {
        AmountRewardCalculation(RewardMultiplier);

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CoinsToAdd);
        PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + DiamondsToAdd);
        PlayerPrefs.Save();

        ResetRewardToAdd();

        CurrencyManager.Instance.OnCurrencyChange?.Invoke();
    }

    public void BuyWithCoins(int value)
    {
        if (PlayerPrefs.GetInt("Coins") - value >= 0)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - value);
            CurrencyManager.Instance.OnCurrencyChange?.Invoke();
        }
        else
        {
            Debug.Log("Not enough coins to buy");
        }
        
    }
    public void BuyWithDiamonds(int value)
    {
        if (PlayerPrefs.GetInt("Diamonds") - value >= 0)
        {
            PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") - value);
            CurrencyManager.Instance.OnCurrencyChange?.Invoke();
        }
        else
        {
            Debug.Log("Not enough diamonds to buy");
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
            Debug.Log("Not enough coins to buy");
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
            Debug.Log("Not enough diamonds to buy");
            return false;
        }
    }
}
