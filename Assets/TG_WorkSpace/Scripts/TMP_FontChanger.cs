using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // 기본 Unity 텍스트를 위해 필요

#if UNITY_EDITOR
using UnityEditor;
#endif

/*
https://bonnate.tistory.com/

Insert the script into the game object
insert the TMP font in the inspector
and press the button to find and replace all components.

It may work abnormally, so make sure to back up your scene before using it!!
*/

public class TMP_FontChanger : MonoBehaviour
{
    [SerializeField] public TMP_FontAsset FontAsset;
    [SerializeField] public Font UnityFont; // 기본 Unity 텍스트용 폰트
}

#if UNITY_EDITOR
[CustomEditor(typeof(TMP_FontChanger))]
public class TMP_FontChangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Change Font!"))
        {
            TMP_FontAsset fontAsset = ((TMP_FontChanger)target).FontAsset;
            Font unityFont = ((TMP_FontChanger)target).UnityFont;

            // TextMeshPro 3D 텍스트 변경
            foreach (TextMeshPro textMeshPro3D in GameObject.FindObjectsOfType<TextMeshPro>(true))
            {
                textMeshPro3D.font = fontAsset;
            }

            // TextMeshPro UGUI 텍스트 변경
            foreach (TextMeshProUGUI textMeshProUi in GameObject.FindObjectsOfType<TextMeshProUGUI>(true))
            {
                textMeshProUi.font = fontAsset;
            }

            // 기본 Unity 텍스트 변경
            foreach (Text text in GameObject.FindObjectsOfType<Text>(true))
            {
                text.font = unityFont;
            }
        }
    }
}
#endif
