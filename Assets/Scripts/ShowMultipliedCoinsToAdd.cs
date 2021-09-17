using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowMultipliedCoinsToAdd : MonoBehaviour
{
    private TextMeshProUGUI _coinsTMP;

    void Awake()
    {
        _coinsTMP = this.GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        ShowMultipliedCoinToAdd();
    }
    void OnEnable()
    {
        ShowMultipliedCoinToAdd();
    }

    public void ShowMultipliedCoinToAdd()
    {
        var result = CurrencyManager.Instance.CoinsToAdd * CurrencyManager.Instance.RewardMultiplier;
        _coinsTMP.text = "+" + result.ToString();
    }
}
