using UnityEngine;
using UnityEditor;

namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    public class WindowHeaderGUI : WindowGUI
    {
        public override void OnGUI()
        {
            GUILayout.Label(new GUIContent(BundleInfo.BundleImage));

            GUILayout.Label($"Thank you for buying our {BundleInfo.BundleTitle}! Visit each package listed below, to add them to your assets and download them.", LabelStyle);
        }

        GUIStyle LabelStyle => new GUIStyle(EditorStyles.label)
        {
            wordWrap = true
        };
    }
}