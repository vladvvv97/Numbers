using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverZone : MonoBehaviour
{
    public UnityEvent OnStayingInGameOverZone;
    private bool _alreadyChecked = false;

    public bool AlreadyChecked { get => _alreadyChecked; set => _alreadyChecked = value; }


    void OnTriggerStay2D(Collider2D collision)
    {
        NumberCube EnteredCube = collision.gameObject.GetComponent<NumberCube>();
            if 
            (
            EnteredCube 
            && EnteredCube.ReadyToMerge                                  == true 
            && AlreadyChecked                                            == false 
            && GameManager.Instance.AllCubesReadyToMerge                 == true
            && GameManager.Instance.CubesToDestroy.Contains(EnteredCube) == false // 19.04.2022 - Game Over even if Entered cube destroys
            && GameManager.Instance._timer > GameManager.Instance.WaitForReadyToMerge
            )
            {
                OnStayingInGameOverZone?.Invoke();
                AlreadyChecked = true;
                AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.GameOver);
            }
    }

}
