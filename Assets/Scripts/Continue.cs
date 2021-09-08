using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    [SerializeField] private GameOverZone gameOverZone; 
    public void ContinueGame()
    {
        gameOverZone.AlreadyChecked = false;
        Time.timeScale = 1;
    }
}
