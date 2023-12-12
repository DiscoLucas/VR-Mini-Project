using UnityEngine;
using UnityEditor;

namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    public class WindowSupportGUI : WindowGUI
    {
        public override void OnGUI()
        {
            GUILayout.Space(14);

            DrawHeader("Support");
            GUILayout.Label($"If you have any question, feel free to get in touch with us via support@ilumisoft.de", LabelStyle);
        }

        GUIStyle LabelStyle => new GUIStyle(EditorStyles.label)
        {
            wordWrap = true
        };
    }
}