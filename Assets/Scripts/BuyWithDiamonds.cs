using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuyWithDiamonds : MonoBehaviour
{
    [SerializeField] private int costValue;
    [SerializeField] private GameObject notEnoughCurrencyAlert;
    [SerializeField] private AnimationClip NotEnoughCurrencyAlertAnimationClip;

    public void HideForDiamonds()
    {
        if (CurrencyManager.Instance.CanBuyWithDiamonds(costValue) == true)
        {
            CurrencyManager.Instance.BuyWithDiamonds(costValue);
            this.gameObject.SetActive(false);
        }
        else
        {
            ActivateNotEnoughCurrencyScreen();
        }
    }

    public void ActivateNotEnoughCurrencyScreen()
    {
        notEnoughCurrencyAlert.gameObject.SetActive(true);
        Invoke(nameof(DeactivateNotEnoughCurrencyScreen), NotEnoughCurrencyAlertAnimationClip.length);
    }

    public void DeactivateNotEnoughCurrencyScreen()
    {
        notEnoughCurrencyAlert.gameObject.SetActive(false);
    }

}

