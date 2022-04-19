using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementConditionManager : MonoBehaviour
{
    public static AchievementConditionManager Instance;
    //-------------------------------------------------------------------------------------------------------------------------------------------------------//
    public int AchievementCurrentProgressValue(int index) // GetInt
    {
        return PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementProgress.AchievementCurrentProgressValue.ToString() + index.ToString());
    }
    public void AchievementCurrentProgressValue(int index, int value) // SetInt
    {
        PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementProgress.AchievementCurrentProgressValue.ToString() + index.ToString(), value);
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------//
    public int AchievementEndingProgressValue(int index) // GetInt
    {
        return PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementProgress.AchievementEndingProgressValue.ToString() + index.ToString());      
    }
    public void AchievementEndingProgressValue(int index, int value) // SetInt
    {
        PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementProgress.AchievementEndingProgressValue.ToString() + index.ToString(), value);
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------//
    public bool isAchievementRewardTaken(int index) // GetInt
    {
        return PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievementRewardTaken.ToString() + index.ToString()) == 0 ? false : true;
    }
    public void isAchievementRewardTaken(int index, bool value) // SetInt
    {
        PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievementRewardTaken.ToString() + index.ToString(), value == false ? 0 : 1);
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------//
    public bool isAchievement1Completed // Login 7 Days
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievement1Completed.ToString()) == 0 ? false : true;
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievement1Completed.ToString(), value == false ? 0 : 1);
    }
    public bool isAchievement2Completed // Deal 500 swipes
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievement2Completed.ToString()) == 0 ? false : true;
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievement2Completed.ToString(), value == false ? 0 : 1);
    }
    public bool isAchievement3Completed // Reach 1000 Top Score
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievement3Completed.ToString()) == 0 ? false : true;
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievement3Completed.ToString(), value == false ? 0 : 1);
    }
    public bool isAchievement4Completed // Watch 50 ad videos
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievement4Completed.ToString()) == 0 ? false : true;
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementStatus.isAchievement4Completed.ToString(), value == false ? 0 : 1);
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------//
    public Action<int> OnAchievementLogin;          // Achievement 1 progress update
    public Action<int> OnSwipe;                     // Achievement 2 progress update
    public Action<int> OnTopScoreChanged;           // Achievement 3 progress update
    public Action<int> OnAchievementVideoWatched;   // Achievement 4 progress update
    //-------------------------------------------------------------------------------------------------------------------------------------------------------//
    public int AchievementLoginCount          // Achievement 1 condition count
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementConditions.AchievementLoginCount.ToString());
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementConditions.AchievementLoginCount.ToString(), value);
    }
    public int SwipeCount      // Achievement 2 condition count
    { 
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementConditions.SwipeCount.ToString()); 
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementConditions.SwipeCount.ToString(), value); 
    }
    public int TopScoreCount          // Achievement 3 condition count
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementConditions.TopScoreCount.ToString());
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementConditions.TopScoreCount.ToString(), value);
    }
    public int AchievementVideoWatchCount     // Achievement 4 condition count
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsAchievementConditions.AchievementVideoWatchCount.ToString());
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsAchievementConditions.AchievementVideoWatchCount.ToString(), value);
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject);

        OnAchievementLogin += CheckAchievementIncreaseProgress;
        OnAchievementLogin += CheckAchievementCompleteCondition;

        OnSwipe += CheckAchievementIncreaseProgress;
        OnSwipe += CheckAchievementCompleteCondition;

        OnTopScoreChanged += CheckAchievementIncreaseProgress;
        OnTopScoreChanged += CheckAchievementCompleteCondition;

        OnAchievementVideoWatched += CheckAchievementIncreaseProgress;
        OnAchievementVideoWatched += CheckAchievementCompleteCondition;

    }
    public void CheckAchievementIncreaseProgress(int index)
    {
        switch (index)
        {
            case 1:
                AchievementLoginCount += Application.isPlaying? 1:0;
                AchievementCurrentProgressValue(index, AchievementLoginCount);
                break;

            case 2:
                SwipeCount++;
                AchievementCurrentProgressValue(index, SwipeCount);
                break;

            case 3:
                TopScoreCount = PlayerPrefs.GetInt("BestScore");
                AchievementCurrentProgressValue(index, TopScoreCount);
                break;

            case 4:
                AchievementVideoWatchCount++;
                AchievementCurrentProgressValue(index, AchievementVideoWatchCount);
                break;

            default:
                return;
        }
    }
    public void CheckAchievementCompleteCondition(int index)
    {
        switch (index)
        {
            case 1:
                if (AchievementLoginCount >= AchievementEndingProgressValue(index) /*Achievement1ScoreConditionValue*/ )
                {
                    isAchievement1Completed = true;
                }
                break;

            case 2:
                if (SwipeCount >= AchievementEndingProgressValue(index))
                {
                    isAchievement2Completed = true;
                }
                break;

            case 3:
                if (TopScoreCount >= AchievementEndingProgressValue(index))
                {
                    isAchievement3Completed = true;
                }
                break;

            case 4:
                if (AchievementVideoWatchCount >= AchievementEndingProgressValue(index))
                {
                    isAchievement4Completed = true;
                }
                break;

            default:
                return;
        }
    }

    public bool IsAchievementCompleted(int index)
    {
        switch (index)
        {
            case 1:
                return isAchievement1Completed;

            case 2:
                return isAchievement2Completed;

            case 3:
                return isAchievement3Completed;

            case 4:
                return isAchievement4Completed;

            default:
                return false;
        }
    }

    public int NumberOfCompletedAchievements() // NEED TO UPDATE WHEN INCREASE NUMBER OF TASKS
    {
        int result =
            (isAchievement1Completed && !isAchievementRewardTaken(1) ? 1 : 0) + 
            (isAchievement2Completed && !isAchievementRewardTaken(2) ? 1 : 0) + 
            (isAchievement3Completed && !isAchievementRewardTaken(3) ? 1 : 0) + 
            (isAchievement4Completed && !isAchievementRewardTaken(4) ? 1 : 0);
        return result;
    }

    public void InvokeOnAchievementLoginAction()
    {
        AchievementConditionManager.Instance.OnAchievementLogin?.Invoke(1);
    }
    public void InvokeOnSwipeAction()
    {
        AchievementConditionManager.Instance.OnSwipe?.Invoke(2);
    }
    public void InvokeOnTopScoreChangedAction()
    {
        AchievementConditionManager.Instance.OnTopScoreChanged?.Invoke(3);
    }
    public void InvokeOnAchievementVideoWatchedAction()
    {
        AchievementConditionManager.Instance.OnAchievementVideoWatched?.Invoke(4);
    }
}
