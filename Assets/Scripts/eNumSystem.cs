using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eNumSystem : MonoBehaviour
{
    public enum ePlayerPrefsNames { LastSaveTime, DailyRewardUsed }
    public enum ePlayerPrefsTaskConditions { LoginCount, PlaysTimeCount, ScoreCount, VideoWatchCount }
    public enum ePlayerPrefsTaskProgress { CurrentProgressValue , EndingProgressValue }
    public enum ePlayerPrefsTaskStatus { isTask1Completed , isTask2Completed , isTask3Completed , isTask4Completed }
    public enum eCurrencyType { Coin, Diamond }
}
