using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDiamondsToAdd : MonoBehaviour
{
    private TextMeshProUGUI _diamondsTMP;

    void Awake()
    {
        _diamondsTMP = this.GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        ShowDiamondToAdd();
    }
    void OnEnable()
    {
        ShowDiamondToAdd();
    }

    public void ShowDiamondToAdd()
    {
        _diamondsTMP.text = "+" + CurrencyManager.Instance.DiamondsToAdd.ToString();
    }
}
