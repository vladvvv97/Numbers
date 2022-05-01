using UnityEngine;

public class BuyWithCoins : MonoBehaviour
{
    [SerializeField] private int costValue;
    [SerializeField] private GameObject notEnoughCurrencyAlert;
    [SerializeField] private AnimationClip NotEnoughCurrencyAlertAnimationClip;

    public void HideForCoins()
    {
        if (CurrencyManager.Instance.CanBuyWithCoins(costValue) == true)
        {
            CurrencyManager.Instance.BuyWithCoins(costValue);
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

