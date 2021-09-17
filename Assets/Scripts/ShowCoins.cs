using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCoins : MonoBehaviour
{
    private TextMeshProUGUI _coinsTMP;

    void Awake()
    {
        _coinsTMP = this.GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        _coinsTMP.text = PlayerPrefs.GetInt("Coins").ToString();
        CurrencyManager.Instance.OnCurrencyChange += ShowCoin;
    }
    void OnDisable()
    {
        CurrencyManager.Instance.OnCurrencyChange -= ShowCoin;
    }
    public void ShowCoin()
    {
        _coinsTMP.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
