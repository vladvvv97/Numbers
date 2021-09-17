using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class LockedElementActivateManager : MonoBehaviour
{
    enum eSkinType {Cube, InGameBackground, MainMenuBackground, Backlight}
    enum ePlayerPrefs {False, True}

    [SerializeField] private eSkinType type;
    [SerializeField] private TextMeshProUGUI costValue;
    [SerializeField] private GameObject notEnoughCurrencyAlert;
    [SerializeField] private Image[] checkmarks;
    [SerializeField] private AnimationClip NotEnoughCurrencyAlertAnimationClip;

    private Image[] _images;
    private string _index;

    void Awake()
    {
        if (PlayerPrefs.HasKey(gameObject.name))
        {
            return;
        }
        else
        {
            PlayerPrefs.SetInt((gameObject.name), (int)ePlayerPrefs.False);
        }
    }
    void Start()
    {
        _images = gameObject.GetComponentsInChildren<Image>();
        _index = new string(this.gameObject.name.Where(Char.IsDigit).ToArray());

        if (PlayerPrefs.GetInt(gameObject.name) == (int)ePlayerPrefs.False)
        {
            ChangeColor(Color.gray);
        }
        else
        {
            ChangeColor(Color.white);
        }
    }
    
    public void ActivateSkinForCoins()
    {
        if (PlayerPrefs.GetInt(gameObject.name) == (int)ePlayerPrefs.False)
        {
            if (CurrencyManager.Instance.CanBuyWithCoins(Int32.Parse(costValue.text)) == true)
            {
                SetIndex();
                PlayerPrefs.SetInt((gameObject.name), (int)ePlayerPrefs.True);
                ChangeColor(Color.white);

                SetCheckmark();

                CurrencyManager.Instance.BuyWithCoins(Int32.Parse(costValue.text));
            }
            else
            {
                StartCoroutine(ActivateNotEnoughCurrencyScreen());
            }
        }
        else if (PlayerPrefs.GetInt(gameObject.name) == (int)ePlayerPrefs.True)
        {
            SetIndex();
            SetCheckmark();
        }
        else
        {
            return;
        }     
    }

    public void ActivateSkinForDiamonds()
    {
        if (PlayerPrefs.GetInt(gameObject.name) == (int)ePlayerPrefs.False)
        {
            if (CurrencyManager.Instance.CanBuyWithDiamonds(Int32.Parse(costValue.text)) == true)
            {
                SetIndex();
                PlayerPrefs.SetInt((gameObject.name), (int)ePlayerPrefs.True);
                ChangeColor(Color.white);

                SetCheckmark();

                CurrencyManager.Instance.BuyWithDiamonds(Int32.Parse(costValue.text));
            }
            else
            {
                StartCoroutine(ActivateNotEnoughCurrencyScreen());
            }
        }
        else if (PlayerPrefs.GetInt(gameObject.name) == (int)ePlayerPrefs.True)
        {
            SetIndex();
            SetCheckmark();
        }
        else
        {
            return;
        }
    }

    public void SetIndex()
    {
        switch(type)
        {
            case eSkinType.Cube:
                SkinsManager.Instance.SetCubeIndex(Int32.Parse(_index));
                break;

            case eSkinType.InGameBackground:
                SkinsManager.Instance.SetInGameBackgroundIndex(Int32.Parse(_index));
                break;

            case eSkinType.MainMenuBackground:
                SkinsManager.Instance.SetInGameBackgroundIndex(Int32.Parse(_index));
                break;

            case eSkinType.Backlight:
                SkinsManager.Instance.SetBacklightIndex(Int32.Parse(_index));
                break;
        }           
    }

    public void ChangeColor(Color color)
    {
        foreach (var element in _images)
        {
            if (element.GetComponent<ColorChangeable>())
            {
                element.color = color;
            }
        }
    }

    IEnumerator ActivateNotEnoughCurrencyScreen()
    {
        notEnoughCurrencyAlert.gameObject.SetActive(true);
        yield return new WaitForSeconds(NotEnoughCurrencyAlertAnimationClip.length);
        notEnoughCurrencyAlert.gameObject.SetActive(false);
        yield return null;
    }

    public void SetCheckmark()
    {
        foreach (var element in checkmarks)
        {
            if (element.name.Contains(_index))
            {
                element.enabled = true;
            }
            else
            {
                element.enabled = false;
            }
        }
    }
}
