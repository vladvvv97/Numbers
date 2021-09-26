using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskConditionManager : MonoBehaviour
{
    public static TaskConditionManager Instance;
    public int CurrentProgressValue(int index) // GetInt
    {
        return PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskProgress.CurrentProgressValue.ToString() + index.ToString());
    }
    public void CurrentProgressValue(int index, int value) // SetInt
    {
        PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskProgress.CurrentProgressValue.ToString() + index.ToString(), value);
    }
    public int EndingProgressValue(int index) // GetInt
    {
        return PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskProgress.EndingProgressValue.ToString() + index.ToString());      
    }
    public void EndingProgressValue(int index, int value) // SetInt
    {
        PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskProgress.EndingProgressValue.ToString() + index.ToString(), value);
    }

    public bool isTask1Completed // Login 
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskStatus.isTask1Completed.ToString()) == 0 ? false : true;
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskStatus.isTask1Completed.ToString(), value == false ? 0 : 1);
    }
    public bool isTask2Completed // Play N times
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskStatus.isTask2Completed.ToString()) == 0 ? false : true;
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskStatus.isTask2Completed.ToString(), value == false ? 0 : 1);
    }
    public bool isTask3Completed // Reach N score
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskStatus.isTask3Completed.ToString()) == 0 ? false : true;
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskStatus.isTask3Completed.ToString(), value == false ? 0 : 1);
    }
    public bool isTask4Completed // Watch N videos
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskStatus.isTask4Completed.ToString()) == 0 ? false : true;
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskStatus.isTask4Completed.ToString(), value == false ? 0 : 1);
    }

    [SerializeField] private int Task3ScoreConditionValue;

    public Action<int> OnLogin;         // Task 1 progress update
    public Action<int> OnGameFinished;  // Task 2 progress update
    public Action<int> OnScoreChanged;  // Task 3 progress update
    public Action<int> OnVideoWatched;  // Task 4 progress update

    public int LoginCount          // Task 1 condition count
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskConditions.LoginCount.ToString());
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskConditions.LoginCount.ToString(), value);
    }
    public int PlaysTimeCount      // Task 2 condition count
    { 
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskConditions.PlaysTimeCount.ToString()); 
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskConditions.PlaysTimeCount.ToString(), value); 
    }
    public int ScoreCount          // Task 3 condition count
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskConditions.ScoreCount.ToString());
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskConditions.ScoreCount.ToString(), value);
    }
    public int VideoWatchCount     // Task 4 condition count
    {
        get => PlayerPrefs.GetInt(eNumSystem.ePlayerPrefsTaskConditions.VideoWatchCount.ToString());
        set => PlayerPrefs.SetInt(eNumSystem.ePlayerPrefsTaskConditions.VideoWatchCount.ToString(), value);
    }
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject);

        OnLogin += CheckTaskIncreaseProgress;
        OnLogin += CheckTaskCompleteCondition;

        OnGameFinished += CheckTaskIncreaseProgress;
        OnGameFinished += CheckTaskCompleteCondition;

        OnScoreChanged += CheckTaskIncreaseProgress;
        OnScoreChanged += CheckTaskCompleteCondition;

        OnVideoWatched += CheckTaskIncreaseProgress;
        OnVideoWatched += CheckTaskCompleteCondition;
    }

    void Start()
    {
        InvokeOnLoginAction();
    }

    public void CheckTaskIncreaseProgress(int index)
    {
        switch (index)
        {
            case 1:
                LoginCount = Application.isPlaying ? 1 : 0;
                CurrentProgressValue(index, LoginCount);
                break;

            case 2:
                ++PlaysTimeCount;
                CurrentProgressValue(index, PlaysTimeCount);
                break;

            case 3:
                ScoreCount += GameManager.Instance.Score >= 1000 ? 1 : 0;
                CurrentProgressValue(index, ScoreCount);
                break;

            case 4:
                ++VideoWatchCount;
                CurrentProgressValue(index, VideoWatchCount);
                break;

            default:
                return;
        }
    }
    public void CheckTaskCompleteCondition(int index)
    {
        switch (index)
        {
            case 1:
                if (LoginCount >= EndingProgressValue(index))
                {
                    isTask1Completed = true;
                }
                break;

            case 2:
                if (PlaysTimeCount >= EndingProgressValue(index))
                {
                    isTask2Completed = true;
                }
                break;

            case 3:
                if (ScoreCount >= EndingProgressValue(index))
                {
                    isTask3Completed = true;
                }
                break;

            case 4:
                if (VideoWatchCount >= EndingProgressValue(index))
                {
                    isTask4Completed = true;
                }
                break;

            default:
                return;
        }
    }

    public void ResetCompleteProgress(int index)
    {
        switch (index)
        {
            case 1:
                isTask1Completed = false;
                CurrentProgressValue(index, 0);
                break;

            case 2:
                isTask2Completed = false;
                CurrentProgressValue(index, 0);
                break;

            case 3:
                isTask3Completed = false;
                CurrentProgressValue(index, 0);
                break;

            case 4:
                isTask4Completed = false;
                CurrentProgressValue(index, 0);
                break;

            default:
                return;
        }
    }
    public void ResetAllCompleteProgress()
    {
                isTask1Completed = false;
                CurrentProgressValue(1, 0);

                isTask2Completed = false;
                CurrentProgressValue(2, 0);

                isTask3Completed = false;
                CurrentProgressValue(3, 0);

                isTask4Completed = false;
                CurrentProgressValue(4, 0);
    }

    public bool IsTaskCompleted(int index)
    {
        switch (index)
        {
            case 1:
                return isTask1Completed;

            case 2:
                return isTask2Completed;

            case 3:
                return isTask3Completed;

            case 4:
                return isTask4Completed;

            default:
                return false;
        }
    }

    public int NumberOfCompletedTasks() // NEED TO UPDATE WHEN INCREASE NUMBER OF TASKS
    {
        int result = (isTask1Completed ? 1 : 0) + (isTask2Completed ? 1 : 0) + (isTask3Completed ? 1 : 0) + (isTask4Completed ? 1 : 0);
        return result;
    }

    public void InvokeOnLoginAction()
    {
        TaskConditionManager.Instance.OnLogin?.Invoke(1);
    }
    public void InvokeOnGameFinishedAction()
    {
        TaskConditionManager.Instance.OnGameFinished?.Invoke(2);
    }
    public void InvokeOnScoreChangedAction()
    {
        TaskConditionManager.Instance.OnScoreChanged?.Invoke(3);
    }
    public void InvokeOnVideoWatchedAction()
    {
        TaskConditionManager.Instance.OnVideoWatched?.Invoke(4);
    }
}
