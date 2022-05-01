using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    private Animator _animController;

    private void Awake()
    {
        _animController = GetComponent<Animator>();
    }

    private void Start()
    {
        _animController.runtimeAnimatorController = SkinsManager.Instance.MainMenuBackgrounds[PlayerPrefs.GetInt("MainMenuBackgroundIndex")];
    }
    public void SetBackground()
    {
        _animController.runtimeAnimatorController = SkinsManager.Instance.MainMenuBackgrounds[PlayerPrefs.GetInt("MainMenuBackgroundIndex")];
    }
}
