using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowMultipliedDiamondsToAdd : MonoBehaviour
{
    private TextMeshProUGUI _diamondsTMP;

    void Awake()
    {
        _diamondsTMP = this.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        ShowMultipliedDiamondToAdd();
    }
    void OnEnable()
    {
        ShowMultipliedDiamondToAdd();
    }

    public void ShowMultipliedDiamondToAdd()
    {
        var result = CurrencyManager.Instance.DiamondsToAdd * CurrencyManager.Instance.RewardMultiplier;
        _diamondsTMP.text = "+" + result.ToString();
    }
}
