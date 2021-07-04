using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacklightLines : MonoBehaviour
{
    private SpriteRenderer[] _sr;

    void Awake()
    {
        _sr = this.GetComponentsInChildren<SpriteRenderer>();
    }
    void Start()
    {
        foreach (var sr in _sr)
        {
            sr.enabled = false;
        }
       
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }
    private void OnMouseDrag()
    {
        foreach (var sr in _sr)
        {
            sr.enabled = false;
        }

        switch(GameManager.Instance.DropLine())
        {
            case 10: _sr[0].enabled = true; break;
            case 11: _sr[1].enabled = true; break;
            case 12: _sr[2].enabled = true; break;
            case 13: _sr[3].enabled = true; break;
            case 14: _sr[4].enabled = true; break;
            case 15: _sr[5].enabled = true; break;

            case 20: _sr[0].enabled = true; _sr[1].enabled = true; break;
            case 21: _sr[1].enabled = true; _sr[2].enabled = true; break;
            case 22: _sr[2].enabled = true; _sr[3].enabled = true; break;
            case 23: _sr[3].enabled = true; _sr[4].enabled = true; break;
            case 24: _sr[4].enabled = true; _sr[5].enabled = true; break;

            case 30: _sr[0].enabled = true; _sr[1].enabled = true; _sr[2].enabled = true; break;
            case 31: _sr[1].enabled = true; _sr[2].enabled = true; _sr[3].enabled = true; break;
            case 32: _sr[2].enabled = true; _sr[3].enabled = true; _sr[4].enabled = true; break;
            case 33: _sr[3].enabled = true; _sr[4].enabled = true; _sr[5].enabled = true; break;

            default: return;
        }

        

        GameManager.Instance.CubeControll();
    }
    private void OnMouseUp()
    {
        GameManager.Instance.SetSpeedEqualDropSpeed();

        foreach (var sr in _sr)
        {
            sr.enabled = false;
        }
    }
     
}
