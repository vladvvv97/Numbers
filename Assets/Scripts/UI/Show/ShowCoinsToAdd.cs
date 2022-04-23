using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowCoinsToAdd : MonoBehaviour
{
    private TextMeshProUGUI _coinsTMP;

    void Awake()
    {
        _coinsTMP = this.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        ShowCoinToAdd();
    }
    void OnEnable()
    {
        ShowCoinToAdd();
    }
    public void ShowCoinToAdd()
    {
        var result = CurrencyManager.Instance.CoinsToAdd;
        _coinsTMP.text = "+" + result.ToString();
    }
}
