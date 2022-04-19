using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    private Toggle toggle;
    void Awake() => toggle = GetComponent<Toggle>();
    void OnEnable() => toggle.onValueChanged.AddListener(value => AudioManager.Instance.Music.MuteMusic());
    private void OnDisable() => toggle.onValueChanged.RemoveAllListeners();
    private void OnDestroy() => toggle.onValueChanged.RemoveAllListeners();
}
