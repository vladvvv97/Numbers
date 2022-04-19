using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private ButtonType buttonType;
    private Button _button;
    enum ButtonType
    {
        Default, Back
    }
    void Awake()
    {
        _button = GetComponent<Button>();
    }
    void OnEnable() => _button.onClick.AddListener(PlayButtonSound);
    void OnDisable() => _button.onClick.RemoveListener(PlayButtonSound);
    void OnDestroy() => _button.onClick.RemoveListener(PlayButtonSound);
    private void PlayButtonSound()
    {
        if (buttonType == ButtonType.Default)
        {
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Button);
        }
        else if (buttonType == ButtonType.Back)
        {
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Back);
        }
        else { return; }
    }
}
