using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class LockedElementActivateManager : MonoBehaviour
{
    private enum eSkinType {Cube, InGameBackground, MainMenuBackground, Backlight}
    private enum ePlayerPrefs {False, True}

    [SerializeField] private eSkinType type;
    [SerializeField] private TextMeshProUGUI costValue;
    [SerializeField] private GameObject notEnoughCurrencyAlert;
    [SerializeField] private Image[] checkmarks;
    [SerializeField] private AnimationClip notEnoughCurrencyAlertAnimationClip;
    [SerializeField] [Range(0, 4)] private int achievementIndex;

    private Image[] _images;
    private Button _button;
    private string _index;

    void Awake()
    {
        _button = gameObject.GetComponentInChildren<Button>();
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
    private void OnEnable()
    {
        notEnoughCurrencyAlert.gameObject.SetActive(false);
        _button.interactable = true;
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

    public void ActivateSkinForAchievement()
    {
        if (PlayerPrefs.GetInt(gameObject.name) == (int)ePlayerPrefs.False)
        {
            if (AchievementConditionManager.Instance.isAchievementRewardTaken(achievementIndex) == true)
            {
                PlayerPrefs.SetInt((gameObject.name), (int)ePlayerPrefs.True);
                SetIndex();             
                ChangeColor(Color.white);
                SetCheckmark();
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
    public void PreUnlockAchievement()
    {
        foreach (var element in _images)
        {
            if (element.GetComponent<ColorChangeable>())
            {
                element.color = Color.white;
            }
        }

        costValue.color = Color.green;
    }

    IEnumerator ActivateNotEnoughCurrencyScreen()
    {
        notEnoughCurrencyAlert.gameObject.SetActive(true);
        _button.interactable = false;
        AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Alert);
        yield return new WaitForSeconds(notEnoughCurrencyAlertAnimationClip.length);

        notEnoughCurrencyAlert.gameObject.SetActive(false);
        _button.interactable = true;

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
