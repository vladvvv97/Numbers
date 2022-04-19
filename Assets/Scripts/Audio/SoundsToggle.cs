using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsToggle : MonoBehaviour
{
    private Toggle toggle;
    void Awake() => toggle = GetComponent<Toggle>();
    void OnEnable() => toggle.onValueChanged.AddListener(value => AudioManager.Instance.Sounds.MuteSounds());
    private void OnDisable() => toggle.onValueChanged.RemoveAllListeners();
    private void OnDestroy() => toggle.onValueChanged.RemoveAllListeners();
}
