using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ContinueForCoins : MonoBehaviour
{
    [SerializeField] private int quantity;
    [SerializeField] private TextMeshProUGUI costValue;
    [SerializeField] private GameObject notEnoughCurrencyAlert;
    [SerializeField] private AnimationClip NotEnoughCurrencyAlertAnimationClip;
    [SerializeField] private Continue Continue;
    [SerializeField] private ClearRows ClearRows;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Speed speed;

    private Button _button;

    void Start()
    {
        _button = gameObject.GetComponentInChildren<Button>();        
    }

    public void ActivateContinueForCoins()
    {
        if (CurrencyManager.Instance.CanBuyWithCoins(Int32.Parse(costValue.text)) == true)
        {
            CurrencyManager.Instance.BuyWithCoins(Int32.Parse(costValue.text));
            ClearRows.ClearNumberOfRows(quantity);
            _button.interactable = false;
            gameOverUI.gameObject.SetActive(false);
            Continue.ContinueGame();
            speed.ResetSpeed();
        }
        else
        {
            ActivateNotEnoughCurrencyScreen();
        }
        
    }

    public void ActivateNotEnoughCurrencyScreen()
    {
        notEnoughCurrencyAlert.gameObject.SetActive(true);
        _button.interactable = false;
        Invoke(nameof(DeactivateNotEnoughCurrencyScreen), NotEnoughCurrencyAlertAnimationClip.length);
    }

    public void DeactivateNotEnoughCurrencyScreen()
    {
        notEnoughCurrencyAlert.gameObject.SetActive(false);
        _button.interactable = true;
    }

}
