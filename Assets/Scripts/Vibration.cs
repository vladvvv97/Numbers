using UnityEngine;
using System.Collections;

public static class Vibration
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif
    public static bool isVibrationOn
    {
        get => PlayerPrefs.GetInt(eNumSystem.eVibrationStatus.VibrationStatus.ToString()) == 0 ? false : true;
        set { PlayerPrefs.SetInt(eNumSystem.eVibrationStatus.VibrationStatus.ToString(), value == false ? 0 : 1); PlayerPrefs.Save(); }
    }
    public static void Vibrate()
    {
        if (isAndroid())
            vibrator.Call("vibrate");
        else if (isAndroid() && !isVibrationOn)
            return;
        else
            Handheld.Vibrate();
    }


    public static void Vibrate(long milliseconds)
    {
        if (isAndroid() && isVibrationOn)
            vibrator.Call("vibrate", milliseconds);
        else if (isAndroid() && !isVibrationOn)
            return;
        else
            Handheld.Vibrate();
    }

    public static void Vibrate(long[] pattern, int repeat = -1)
    {
        if (isAndroid() && isVibrationOn)
            vibrator.Call("vibrate", pattern, repeat);
        else if (isAndroid() && !isVibrationOn)
            return;
        else
            Handheld.Vibrate();
    }

    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
    public static void InitializeVibration()
    {
        if (!PlayerPrefs.HasKey(eNumSystem.eVibrationStatus.VibrationStatus.ToString()))
        {
            isVibrationOn = true;
        }
        else
        {
            isVibrationOn = System.Convert.ToBoolean(PlayerPrefs.GetInt(eNumSystem.eVibrationStatus.VibrationStatus.ToString()));
        }
    }
}

