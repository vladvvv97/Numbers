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
    void OnEnable()
    {
        ShowCoinToAdd();
    }
    public void ShowCoinToAdd()
    {
        _coinsTMP.text = "+" + CurrencyManager.Instance.CoinsToAdd.ToString();
    }
}
