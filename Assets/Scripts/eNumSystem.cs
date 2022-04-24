using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eNumSystem : MonoBehaviour
{
    public enum ePlayerPrefsNames { LastSaveTime, DailyRewardUsed }
    public enum ePlayerPrefsTaskConditions { LoginCount, PlaysTimeCount, ScoreCount, VideoWatchCount }
    public enum ePlayerPrefsTaskProgress { CurrentProgressValue , EndingProgressValue }
    public enum ePlayerPrefsTaskStatus { isTask1Completed , isTask2Completed , isTask3Completed , isTask4Completed, isTaskRewardTaken }
    public enum ePlayerPrefsAchievementConditions { AchievementLoginCount, SwipeCount, TopScoreCount, AchievementVideoWatchCount }
    public enum ePlayerPrefsAchievementProgress { AchievementCurrentProgressValue , AchievementEndingProgressValue }
    public enum ePlayerPrefsAchievementStatus { isAchievement1Completed, isAchievement2Completed, isAchievement3Completed, isAchievement4Completed, isAchievementRewardTaken }
    public enum eCurrencyType { Coin, Diamond, Skin, None }
    public enum eVibrationStatus {VibrationStatus}
    public enum eUserStatus { FirstTime }
    public enum eTagName { OneNumberCube, TwoNumberCube, ThreeNumberCube }
}