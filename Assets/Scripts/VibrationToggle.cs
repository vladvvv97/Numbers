using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationToggle : MonoBehaviour
{
    private Toggle toggle;
    void Awake()
    {
        toggle = GetComponent<Toggle>();
        InitializeVibrationToggle();
    }

    void OnEnable()
    {
        toggle.onValueChanged.AddListener(value => Vibration.isVibrationOn = value);
        toggle.onValueChanged.AddListener(value => HandheldVibrate(value));
    }
    private void OnDisable() => toggle.onValueChanged.RemoveAllListeners();
    private void OnDestroy() => toggle.onValueChanged.RemoveAllListeners();

    private void HandheldVibrate(bool value)
    {
        if (value)
        {
            Handheld.Vibrate();
        }
        else
        {
            return;
        }
    }

    private void InitializeVibrationToggle()
    {
        if (PlayerPrefs.HasKey(eNumSystem.eVibrationStatus.VibrationStatus.ToString()))
        {
            toggle.SetIsOnWithoutNotify(Vibration.isVibrationOn);
        }
        else
        {
            Vibration.isVibrationOn = true;
            toggle.SetIsOnWithoutNotify(true);
        }
    }
}
