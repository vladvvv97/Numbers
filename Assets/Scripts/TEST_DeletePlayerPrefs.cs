using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TEST_DeletePlayerPrefs : EditorWindow
{
    [MenuItem("Player Prefs/Delete All")]
    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Player Prefs Deleted!");
    }
}
