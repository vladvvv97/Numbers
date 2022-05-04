using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacklightLines : MonoBehaviour
{
    [SerializeField] private GameObject[] lines;
    private SpriteRenderer[] _sr;
    
    void Awake()
    {
        _sr = this.GetComponentsInChildren<SpriteRenderer>();

        foreach (var sr in _sr)
        {
            sr.sprite = SkinsManager.Instance.Backlights[PlayerPrefs.GetInt("BacklightIndex")];
            sr.enabled = true;
        }
    }
    void Start()
    {
        foreach (var item in lines)
        {
            item.SetActive(false);
        }
    }

    public void EnableBacklights()
    {
            switch (GameManager.Instance.DropLine())
            {
                case 10: lines[0].SetActive(true); break;
                case 11: lines[1].SetActive(true); break;
                case 12: lines[2].SetActive(true); break;
                case 13: lines[3].SetActive(true); break;
                case 14: lines[4].SetActive(true); break;
                case 15: lines[5].SetActive(true); break;

                case 20: lines[0].SetActive(true); lines[1].SetActive(true); break;
                case 21: lines[1].SetActive(true); lines[2].SetActive(true); break;
                case 22: lines[2].SetActive(true); lines[3].SetActive(true); break;
                case 23: lines[3].SetActive(true); lines[4].SetActive(true); break;
                case 24: lines[4].SetActive(true); lines[5].SetActive(true); break;

                case 30: lines[0].SetActive(true); lines[1].SetActive(true); lines[2].SetActive(true); break;
                case 31: lines[1].SetActive(true); lines[2].SetActive(true); lines[3].SetActive(true); break;
                case 32: lines[2].SetActive(true); lines[3].SetActive(true); lines[4].SetActive(true); break;
                case 33: lines[3].SetActive(true); lines[4].SetActive(true); lines[5].SetActive(true); break;

                default: return;
            }

            GameManager.Instance.CubeControll();
            GameManager.Instance.SetSpeedEqualDropSpeed();
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Drop);
        Vibration.Vibrate(20);
        Invoke(nameof(DisableBacklights), GameManager.Instance._visibleTimeOfBacklights);              
    }
    private void DisableBacklights()
    {
        foreach (var item in lines)
        {
            item.SetActive(false);
        }
    }
}
