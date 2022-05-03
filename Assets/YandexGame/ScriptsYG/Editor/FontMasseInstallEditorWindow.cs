using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace YG
{
    public class FontMasseInstallEditorWindow : EditorWindow
    {
        [MenuItem("YG/Localization/Font Masse Install")]
        public static void ShowWindow()
        {
            GetWindow<FontMasseInstallEditorWindow>("FontMasseInstall");
        }

        List<GameObject> objectsTranlate = new List<GameObject>();

        private void OnGUI()
        {
            GUILayout.Space(10);

            if (GUILayout.Button("����� ���� �������� �� ����� �� ���� LanguageYG", GUILayout.Height(30)))
            {
                objectsTranlate.Clear();

                foreach (LanguageYG obj in SceneAsset.FindObjectsOfType<LanguageYG>())
                {
                    objectsTranlate.Add(obj.gameObject);
                }
            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("�������� ����������"))
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    if (obj.GetComponent<LanguageYG>())
                    {
                        bool check = false;
                        for (int i = 0; i < objectsTranlate.Count; i++)
                            if (obj == objectsTranlate[i])
                                check = true;

                        if (!check)
                            objectsTranlate.Add(obj);
                    }
                }
            }

            if (GUILayout.Button("������ ����������"))
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    objectsTranlate.Remove(obj);
                }
            }

            GUILayout.EndHorizontal();

            if (objectsTranlate.Count > 0)
            {
                if (GUILayout.Button("��������"))
                {
                    objectsTranlate.Clear();
                }
            }

            if (objectsTranlate.Count > 0)
            {
                GUILayout.Space(10);

                if (GUILayout.Button("��������� ����������� ����� �� ��� ��������� ������", GUILayout.Height(30)))
                {
                    bool complete = true;

                    foreach (GameObject obj in objectsTranlate)
                    {
                        LanguageYG scr = obj.GetComponent<LanguageYG>();

                        if (scr.infoYG.fonts.defaultFont)
                            scr.ChangeFont(scr.infoYG.fonts.defaultFont);
                        else
                        {
                            complete = false;
                            Debug.LogError("The standard font is not specified! Specify it in the InfoYG plugin settings.  (ru) �� ������ ����������� �����! ������� ��� � ���������� ������� InfoYG");
                            break;
                        }
                    }
                    if (complete)
                        Debug.Log("The font was replaced successfully!  (ru) ������ ������ ����������� �������!");
                }
            }

            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            GUILayout.Label($"({objectsTranlate.Count} �������� � ������)", style, GUILayout.ExpandWidth(true));

            for (int i = 0; i < objectsTranlate.Count; i++)
            {
                objectsTranlate[i] = (GameObject)EditorGUILayout.ObjectField($"{i + 1}. { objectsTranlate[i].name}", objectsTranlate[i], typeof(GameObject), false);
            }
        }
    }
}
