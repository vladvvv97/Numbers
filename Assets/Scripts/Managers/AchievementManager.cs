using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] [Tooltip("No./#/¹ Achievements in descending order")][Range(1,4)] private int rewardIndex;   
    [SerializeField] private int rewardValue;
    [SerializeField] private Color rewardValueTextColor;
    [SerializeField] private eNumSystem.eCurrencyType rewardType;
    [SerializeField] private Sprite[] rewardImages;
    [SerializeField] private string achievementConditionTextValue;
    [SerializeField] private string achievementConditionProgressCompleteText;
                     private int achievementCurrentProgressValue;
    [SerializeField] public int achievementEndingProgressValue;

    [Header("Drag&Drop in Inspector")]
    [SerializeField] private TextMeshProUGUI rewardValueText;
    [SerializeField] private Image rewardImage;
    [SerializeField] private TextMeshProUGUI achievementConditionText;
    [SerializeField] private Slider achievementConditionSlider;
    [SerializeField] private TextMeshProUGUI achievementConditionProgressText;
    [SerializeField] private Button claimButton;
    [SerializeField] private TextMeshProUGUI claimText;
    [SerializeField] private Image claimCheckmark;

    [Header("Drag&Drop in Inspector from apropriate skin game object")]
    [SerializeField] private TextMeshProUGUI skinCostValueText;

    public static Action OnGetAchievementReward;

    void Start()
    {
        Subscribe();
        InitializeAchievementsFields();
        InitializeAchievements();
    }
    void Subscribe()
    {
        switch (rewardIndex)
        {
            case 1:
                AchievementConditionManager.Instance.OnAchievementLogin += InitializeAchievementsFields;
                AchievementConditionManager.Instance.OnAchievementLogin += InitializeAchievements;
                break;
            case 2:
                AchievementConditionManager.Instance.OnSwipe += InitializeAchievementsFields;
                AchievementConditionManager.Instance.OnSwipe += InitializeAchievements;
                break;
            case 3:
                AchievementConditionManager.Instance.OnTopScoreChanged += InitializeAchievementsFields;
                AchievementConditionManager.Instance.OnTopScoreChanged += InitializeAchievements;
                break;
            case 4:
                AchievementConditionManager.Instance.OnAchievementVideoWatched += InitializeAchievementsFields;
                AchievementConditionManager.Instance.OnAchievementVideoWatched += InitializeAchievements;
                break;

            default:
                return;
        }
    }
    void Unsubscribe()
    {
        switch (rewardIndex)
        {
            case 1:
                AchievementConditionManager.Instance.OnAchievementLogin -= InitializeAchievementsFields;
                AchievementConditionManager.Instance.OnAchievementLogin -= InitializeAchievements;
                break;
            case 2:
                AchievementConditionManager.Instance.OnSwipe -= InitializeAchievementsFields;
                AchievementConditionManager.Instance.OnSwipe -= InitializeAchievements;
                break;
            case 3:
                AchievementConditionManager.Instance.OnTopScoreChanged -= InitializeAchievementsFields;
                AchievementConditionManager.Instance.OnTopScoreChanged -= InitializeAchievements;
                break;
            case 4:
                AchievementConditionManager.Instance.OnAchievementVideoWatched -= InitializeAchievementsFields;
                AchievementConditionManager.Instance.OnAchievementVideoWatched -= InitializeAchievements;
                break;

            default:
                return;
        }
    }

    void OnDisable()
    {
        Unsubscribe();
    }
    void OnDestroy()
    {
        Unsubscribe();
    }
    public void InitializeAchievementsFields(int index = 0)
    {
        rewardValueText.text = $"+{rewardValue}";
        rewardValueText.color = rewardValueTextColor;
        foreach (var i in rewardImages) { if (i.name == rewardType.ToString()) rewardImage.sprite = i; };

        AchievementConditionManager.Instance.AchievementEndingProgressValue(rewardIndex, achievementEndingProgressValue); 
        achievementCurrentProgressValue = AchievementConditionManager.Instance.AchievementCurrentProgressValue(rewardIndex);

        achievementConditionText.text = achievementConditionTextValue;
        achievementConditionSlider.value = (float)achievementCurrentProgressValue / (float)achievementEndingProgressValue;
        achievementConditionProgressText.text = $"{achievementCurrentProgressValue}/{achievementEndingProgressValue}";

        claimCheckmark.gameObject.SetActive(false);
    }


    private void InitializeAchievements(int index = 0)
    {
        if(AchievementConditionManager.Instance.isAchievementRewardTaken(rewardIndex))
        {
            MakeAchievementCompleted();
        }
        else
        {
            if (AchievementConditionManager.Instance.IsAchievementCompleted(rewardIndex))
            {
                MakeAchievementCompletable();
            }
            else
            {
                MakeAchievementUncompleted();
            }
        }
    }

    public void GetReward()
    {
        MakeAchievementCompleted();
        AddReward();
        AchievementConditionManager.Instance.isAchievementRewardTaken(rewardIndex, true);
        OnGetAchievementReward?.Invoke();
    }

    public void MakeAchievementCompleted()
    {
        claimButton.interactable = false;
        claimCheckmark.gameObject.SetActive(true);
        claimText.gameObject.SetActive(false);

        achievementConditionSlider.value = 1;
        achievementConditionProgressText.text = achievementConditionProgressCompleteText;

        skinCostValueText.text = achievementConditionProgressCompleteText;
        skinCostValueText.color = Color.green;
    }

    public void MakeAchievementUncompleted()
    {        
        claimButton.interactable = false;
        claimCheckmark.gameObject.SetActive(false);
        claimText.gameObject.SetActive(true);
        achievementConditionSlider.value = (float)achievementCurrentProgressValue / (float)achievementEndingProgressValue;
        achievementConditionProgressText.text = $"{achievementCurrentProgressValue}/{achievementEndingProgressValue}";
    }

    public void MakeAchievementCompletable()
    {
        claimButton.interactable = true;
        claimCheckmark.gameObject.SetActive(false);
        claimText.gameObject.SetActive(true);
        achievementConditionSlider.value = (float)achievementCurrentProgressValue / (float)achievementEndingProgressValue;
        achievementConditionProgressText.text = $"{achievementCurrentProgressValue}/{achievementEndingProgressValue}";
    }

    public void AddReward()
    {
        switch (rewardType)
        {
            case eNumSystem.eCurrencyType.Coin:
                CurrencyManager.Instance.AddCoins(rewardValue);
                break;

            case eNumSystem.eCurrencyType.Diamond:
                CurrencyManager.Instance.AddDiamonds(rewardValue);
                break;

            case eNumSystem.eCurrencyType.Skin:              
                break;

            default:
                break;
        }
    }
}
