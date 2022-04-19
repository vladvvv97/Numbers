using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDiamonds : MonoBehaviour
{
    private TextMeshProUGUI _diamondsTMP;

    void Awake()
    {
        _diamondsTMP = this.GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        _diamondsTMP.text = PlayerPrefs.GetInt("Diamonds").ToString();
        CurrencyManager.Instance.OnCurrencyChange += ShowDiamond;
    }
    void OnDisable()
    {
        CurrencyManager.Instance.OnCurrencyChange -= ShowDiamond;
    }
    public void ShowDiamond()
    {
        _diamondsTMP.text = PlayerPrefs.GetInt("Diamonds").ToString();
    }
}
